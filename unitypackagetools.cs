using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using System.Text;

namespace uprospector
{
    public class unity_package_tools
    {
        private static readonly object @lock = new object();
        private static readonly string log_file_path = "uprospector.log";

        public static void log_message(string message)
        {
            lock (@lock)
            {
                try
                {
                    // Append text to the existing file or create a new file if it doesn't exist
                    using StreamWriter file = new StreamWriter(log_file_path, append: true);
                    file.WriteLine($"{DateTime.Now}: {message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error writing to log file: {ex.Message}");
                }
            }
        }
        
        static List<string> find_all_packages(string directory_path)
        {
            var unity_package_files = new List<string>();
            try
            {
                if (Directory.Exists(directory_path))
                {
                    // Search the directory and all subdirectories for .unitypackage files
                    unity_package_files.AddRange(Directory.EnumerateFiles(directory_path, "*.unitypackage", SearchOption.AllDirectories));
                }
                else
                {
                    Console.WriteLine("The specified directory does not exist.");
                }
            }
            catch (Exception ex)
            {
                log_message($"Error finding packages: {ex.Message}");
            }

            return unity_package_files;
        }
        
        private static void extract_tgz(string tgz, string output_dir)
        {
            using FileStream stream = File.OpenRead(tgz);
            Stream zip_stream = new GZipInputStream(stream);
            TarArchive tar_archive = TarArchive.CreateInputTarArchive(zip_stream, Encoding.ASCII);

            tar_archive.ExtractContents(output_dir);
            tar_archive.Close();

            zip_stream.Close();
            stream.Close();
        }
        
        private static string extract_file(string output_directory, string folder)
        {
            byte[]? file_data = null;
            string file_path = "";
            byte[]? file_preview_data = null;

            IEnumerable<string> files = Directory.EnumerateFiles(folder);
            foreach (string file in files)
            {
                switch (Path.GetFileName(file))
                {
                    case "asset":
                        file_data = File.ReadAllBytes(file);
                        break;
                    case "pathname":
                        file_path = File.ReadAllText(file).Split('\n')[0].Trim('\r', '\n', '\0');
                        break;
                    case "preview.png":
                        file_preview_data = File.ReadAllBytes(file);
                        break;
                    case "asset.meta":
                    default: break;
                }
            }

            if (string.IsNullOrWhiteSpace(file_path))
            {
                log_message($"File path missing in {folder}");
            }

            file_path = Path.Combine(output_directory, file_path);
            
            var asset_folder = Path.GetDirectoryName(file_path);
            
            try
            {
                if(asset_folder == null)
                    throw new Exception($"Error getting directory name for {file_path}");
                
                if (!Directory.Exists(asset_folder))
                    Directory.CreateDirectory(asset_folder);
            }
            catch (Exception ex)
            {
                log_message($"Error creating directory {asset_folder}: {ex.Message}");
            }

            if (file_data != null && file_data.Length > 0)
            {
                File.WriteAllBytes(file_path, file_data);
            }
            else
            {
                bool is_folder = Path.GetExtension(file_path).Length == 0;
                if (!is_folder)
                {
                    log_message($"File data missing in {folder} : {file_path} : is_folder = {is_folder}");
                }
            }

            if (file_preview_data is { Length: > 0 })
            {
                var preview_folder = Path.GetDirectoryName(file_path);
                if (preview_folder != null)
                {
                    try
                    {
                        File.WriteAllBytes(Path.Combine(preview_folder, Path.GetFileNameWithoutExtension(file_path) + "_preview.png"), file_preview_data);
                    }
                    catch (Exception e)
                    {
                        log_message("Error writing preview file: " + e.Message);
                    }
                }
            }
            
            return file_path;
        }

        public static event Action<string>? package_extracted = null;
        public static event Action<int>? extraction_complete = null;
        
        public static async Task extract_all_packages(string src, string dst)
        {
            var response = await extract_packages(src, dst);
            extraction_complete?.Invoke(response);      
        }
        
        private static async Task<int> extract_packages(string src, string dst)
        {
            int total_packages = 0;
            dst = Path.Combine(dst, "extract_packages");
            try
            {
                var file_ops = new ParallelOptions { MaxDegreeOfParallelism = 16 };
                total_packages = await Task.Run(() =>
                    {
                        var packages = find_all_packages(src);

                        // Use a parallel loop to process each package
                        var package_result = Parallel.ForEach(packages, new ParallelOptions { MaxDegreeOfParallelism = 8 }, (package) =>
                        {
                            log_message($"Extracting {package}");
                            
                            var package_name = Path.GetFileNameWithoutExtension(package);
                            var package_dir = Path.Combine(dst, package_name);
                            var package_dir_temp = Path.Combine(package_dir, "t." + Guid.NewGuid());

                            if (!Directory.Exists(package_dir_temp))
                            {
                                Directory.CreateDirectory(package_dir_temp);
                            }

                            try
                            {
                                extract_tgz(package, package_dir_temp);

                                // Process each folder in parallel
                                var temp_folder_paths = Directory.GetDirectories(package_dir_temp);

                                var file_result = Parallel.ForEach(temp_folder_paths, file_ops, (next_folder) => { extract_file(package_dir, next_folder); });

                                if (!file_result.IsCompleted)
                                {
                                    log_message($"Error extracting package {package_name}");
                                }
                                
                                Directory.Delete(package_dir_temp, true);
                                
                                lock (@lock)
                                {
                                    package_extracted?.Invoke($"{package_name} : {temp_folder_paths.Length} files");
                                }
                            }
                            catch (Exception e)
                            {
                                log_message($"Error extracting package {package_name} : {e.Message}");
                            }
                        });
                        
                        if (!package_result.IsCompleted)
                        {
                            log_message($"Error extracting packages");
                        }
                        
                        return packages.Count;
                    });
            }
            catch (Exception ex)
            {
                log_message($"Error extracting package from {src} to {dst}. err = {ex.Message}");
            }

            return total_packages;
        }
    }
}

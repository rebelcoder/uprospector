namespace uprospector
{
    public class directory_cleaner
    {
        //given: packagename/assets/assets/textures/texture.png
        //result: packagename/textures/texture.png
        public static void remove_unused(string root_path)
        {
            foreach (var directory in Directory.GetDirectories(root_path))
            {
                remove_unused(directory); // Recursively process subdirectories

                // If the directory contains only other directories (and no files), move them up
                if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length > 0)
                {
                    try
                    {
                        move_sub_up_one(directory);

                        // Delete the now-empty directory
                        Directory.Delete(directory);
                    }
                    catch (Exception e)
                    {
                        unity_package_tools.log_message($"Error removing unused directory: {e.Message}");
                    }
                }
            }
        }
        private static void move_sub_up_one(string directory)
        {
            var parent_directory = Directory.GetParent(directory)?.FullName;

            if (parent_directory == null)
            {
                return;
            }

            foreach (var sub_dir in Directory.GetDirectories(directory))
            {
                var dest_dir = Path.Combine(parent_directory, new DirectoryInfo(sub_dir).Name);

                while (Directory.Exists(dest_dir))
                {
                    dest_dir = Path.Combine(parent_directory, $"{new DirectoryInfo(sub_dir).Name}_{DateTime.Now.Ticks}");
                }
                
                Directory.Move(sub_dir, dest_dir);
            }
        }
    }
}

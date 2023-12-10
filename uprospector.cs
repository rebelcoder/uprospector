// See https://aka.ms/new-console-template for more information

using Terminal.Gui;

namespace uprospector;

static class uprospector_main
{
    static void Main()
    {
        unity_package_tools.exclude_unity_files = true;
        unity_package_tools.remove_unused_directories = true;
        unity_package_tools.log_message($"uprospector started {DateTime.Now}");
        
        Application.Init();
        try
        {
            Application.Run(new uprospector_window());
        }
        finally
        {
            Application.Shutdown();
        }
    }
}

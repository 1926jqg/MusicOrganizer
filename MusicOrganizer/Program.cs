using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicOrganizer
{
    class Program
    {
        public const string BASE_DIRECTORY = @"D:\Music";
        public const string GOOGLE_DIRECTORY = @"GooglePlayExport";

        static void Main()
        {
            var library = new MusicLibrary();

            var files = GetAllFilesInDirectory($@"{BASE_DIRECTORY}\{GOOGLE_DIRECTORY}", "*.mp3");
            Console.WriteLine($"Processing {files.Count()} .mp3 files");

            foreach (var file in files)
            {
                library.Add(new Mp3File(file));
            }
            foreach (var album in library.Albums)
            {
                var path = album.GetPath(BASE_DIRECTORY);
                Directory.CreateDirectory(path);
                foreach(var file in album)
                {
                    File.Copy(file.Path, $@"{path}\{file.Title}.mp3", true);
                }
            }
            foreach(var artist in library.Artists)
            {
                var artistPath = artist.GetPath(BASE_DIRECTORY);
                Directory.CreateDirectory(artistPath);
                foreach(var album in artist)
                {
                    var albumPath = album.GetPath(BASE_DIRECTORY);
                    CreateShortcut(artistPath, album.Title, albumPath);
                }
            }
            
        }

        static void CreateShortcut(string basePath, string name, string targetPath)
        {
            try
            {
                var shell = new IWshRuntimeLibrary.WshShell();
                var shortcutAddress = @$"{basePath}\{name}.lnk";
                var shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutAddress);
                shortcut.Description = $"Shortcut to {name}";
                shortcut.TargetPath = targetPath;
                shortcut.Save();
            }
            catch
            {
                Console.WriteLine($"Failure making shortcut for {name}");
            }
        }


        static IEnumerable<string> GetAllFilesInDirectory(string path, string searchPattern)
        {
            var directories = new Queue<string>();
            directories.Enqueue(path);
            while (directories.Count > 0)
            {
                var currentDirectory = directories.Dequeue();
                foreach(var file in Directory.EnumerateFiles(currentDirectory, searchPattern))
                {
                    yield return file;
                }
                foreach(var directory in Directory.EnumerateDirectories(currentDirectory))
                {
                    directories.Enqueue(directory);
                }
            }
        }
    }
}

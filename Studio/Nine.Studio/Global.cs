﻿namespace Nine.Studio
{

    using System;
    using System.Collections.Generic;

    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Win32;
    using Nine.Studio.Extensibility;

    static class Global
    {
        public static readonly string Title = "Engine Nine";
        public static readonly string TraceFilename = "Nine.log";
        public static readonly string ExtensionDirectory = ".";
        public static readonly string ProjectExtension = ".nine";

        public static readonly Version Version = Assembly.GetExecutingAssembly().GetName().Version;
        public static readonly string VersionString = string.Format("v{0}.{1}", Version.Major, Version.Minor);

        public static readonly string IntermediateDirectory = Path.Combine(Path.GetTempPath(), Strings.Title, VersionString, "Intermediate");
        public static readonly string OutputDirectory = Path.Combine(Path.GetTempPath(), Strings.Title, VersionString, "Bin");

        static Global()
        {
            if (!Directory.Exists(IntermediateDirectory))
                Directory.CreateDirectory(IntermediateDirectory);
            if (!Directory.Exists(OutputDirectory))
                Directory.CreateDirectory(OutputDirectory);
        }

        public static string NextName(IEnumerable<string> existingNames, string name)
        {
            if (existingNames.All(n => !n.Equals(name, StringComparison.OrdinalIgnoreCase)))
                return name;

            int index = 1;
            string next;
            do
            {
                next = string.Format("{0}{1}", name, index++);
            }
            while (existingNames.Any(n => n.Equals(next, StringComparison.OrdinalIgnoreCase)));
            return next;
        }

        public static string NextName(string name, string ext)
        {
            if (string.IsNullOrEmpty(name))
                name = Strings.Untitled;

            if (name == null)
                name = "";  
          
            if (ext == null)
                ext = "";
            else if (!string.IsNullOrEmpty(ext))
                ext = ext.GetNormalizedFileExtension();

            var fileName = "";
            var index = 1;
            do
            {
                fileName = string.Format("{0}{1}{2}", name, index++, ext);
            }
            while (File.Exists(fileName));

            return fileName;
        }

        public static string GetNormalizedFileExtension(this string fileExtension)
        {
            if (string.IsNullOrEmpty(fileExtension))
                return "";

            fileExtension = fileExtension.Trim(' ', '.', '|', '*');
            fileExtension = fileExtension.Insert(0, ".");
            return fileExtension.ToLowerInvariant();
        }

        public static bool MatchFileExtension(this IImporter serializer, string match)
        {
            foreach (var ext in serializer.FileExtensions)
            {
                if (GetNormalizedFileExtension(match) == GetNormalizedFileExtension(ext))
                    return true;
            }
            return false;
        }

        public static bool MatchFileExtension(this IExporter serializer, string match)
        {
            foreach (var ext in serializer.FileExtensions)
            {
                if (GetNormalizedFileExtension(match) == GetNormalizedFileExtension(ext))
                    return true;
            }
            return false;
        }

        public static void SetProperty(string name, string value)
        {
            Registry.SetValue(GetUserRegistry(null), name, value);
        }

        public static string GetProperty(string name)
        {
            return (string)Registry.GetValue(GetUserRegistry(null), name, null);
        }

        public static string GetRegistry(string key)
        {
            string keyPath = string.Format(@"HKEY_LOCAL_MACHINE\Software\{0}\{1}\", Title, VersionString);
            if (!string.IsNullOrEmpty(key))
                keyPath = Path.Combine(keyPath, key);
            return keyPath;
        }

        public static string GetUserRegistry(string key)
        {
            string keyPath = string.Format(@"HKEY_CURRENT_USER\Software\{0}\{1}\", Title, VersionString);
            if (!string.IsNullOrEmpty(key))
                keyPath = Path.Combine(keyPath, key);
            return keyPath;
        }

        public static RegistryKey GetRegistryKey(string key)
        {
            string keyPath = string.Format(@"Software\{0}\{1}\", Title, VersionString);
            if (!string.IsNullOrEmpty(key))
                keyPath = Path.Combine(keyPath, key);
            RegistryKey result = Registry.LocalMachine.OpenSubKey(keyPath);
            if (result == null)
                result = Registry.LocalMachine.CreateSubKey(keyPath);
            return result;
        }

        public static RegistryKey GetUserRegistryKey(string key)
        {
            string keyPath = string.Format(@"Software\{0}\{1}\", Title, VersionString);
            if (!string.IsNullOrEmpty(key))
                keyPath = Path.Combine(keyPath, key);
            RegistryKey result = Registry.CurrentUser.OpenSubKey(keyPath);
            if (result == null)
                result = Registry.CurrentUser.CreateSubKey(keyPath);
            return result;
        }

        public static string NormalizeFilename(string fileName)
        {
            if (fileName == null)
                return null;
            return Path.GetFullPath(new Uri(Path.GetFullPath(fileName)).LocalPath)
                       .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        }

        public static string NormalizePath(string fileName)
        {
            // This isn't correct at all.
            if (fileName == null)
                return null;
            fileName = Path.GetFullPath(new Uri(Path.GetFullPath(fileName)).LocalPath)
                           .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            if (!fileName.EndsWith(Path.DirectorySeparatorChar.ToString()))
                fileName = fileName + Path.DirectorySeparatorChar;
            return fileName;
        }

        public static bool FilenameEquals(string filename1, string filename2)
        {
            return string.Compare(NormalizeFilename(filename1),
                                  NormalizeFilename(filename2),
                                  StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        public static string GetRelativeFilename(string fileName, string path)
        {
            Uri uri = new Uri(NormalizeFilename(fileName));
            Uri dir = new Uri(NormalizePath(path));
            return dir.MakeRelativeUri(uri).OriginalString;
        }
    }
}
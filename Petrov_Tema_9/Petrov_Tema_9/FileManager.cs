using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class FileManager
    {
        public static void CreateAndWriteFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        public static string ReadFile(string path)
        {
            return File.ReadAllText(path);
        }

        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public static void CopyFile(string source, string dest)
        {
            File.Copy(source, dest, overwrite: true);
        }

        public static void MoveFile(string source, string dest)
        {
            File.Move(source, dest);
        }

        public static void RenameFile(string oldPath, string newPath)
        {
            File.Move(oldPath, newPath);
        }

        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        public static void DeleteFilesByPattern(string directory, string pattern)
        {
            foreach (string file in Directory.GetFiles(directory, pattern))
            {
                File.Delete(file);
            }
        }

        public static string[] ListFiles(string directory, string pattern = "*")
        {
            return Directory.GetFiles(directory, pattern);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{

    public class FileInfoProvider
    {
        public static long GetFileSize(string path)
        {
            return new FileInfo(path).Length;
        }

        public static DateTime GetCreationTime(string path)
        {
            return File.GetCreationTime(path);
        }

        public static DateTime GetLastWriteTime(string path)
        {
            return File.GetLastWriteTime(path);
        }

        public static int CompareFilesBySize(string file1, string file2)
        {
            long size1 = GetFileSize(file1);
            long size2 = GetFileSize(file2);
            return size1.CompareTo(size2);
        }

        public static void SetReadOnly(string path, bool readOnly)
        {
            File.SetAttributes(path, readOnly ? FileAttributes.ReadOnly : FileAttributes.Normal);
        }

        public static bool CanRead(string path)
        {
            try
            {
                using (FileStream fs = File.OpenRead(path)) { }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool CanWrite(string path)
        {
            try
            {
                using (FileStream fs = File.OpenWrite(path)) { }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}

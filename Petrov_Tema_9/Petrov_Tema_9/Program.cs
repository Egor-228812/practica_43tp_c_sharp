using System;
using System.IO;
using Task1;

class Program
{
    static void Main()
    {
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string surname = "Petrov";
        string initials = "es";
        string fileName = $"{surname}.{initials}";
        string filePath = Path.Combine(baseDir, fileName);
        string copyPath = Path.Combine(baseDir, $"{surname}_copy.{initials}");
        string movedDir = Path.Combine(baseDir, "Moved");
        string movedPath = Path.Combine(movedDir, fileName);
        string renamedPath = Path.Combine(baseDir, "familiya.io");
        string tempFile = Path.Combine(baseDir, "temp.txt");

        try
        {
            // 1
            FileManager.CreateAndWriteFile(filePath, "яегор и мне 18 лет!%;№ ");
            Console.WriteLine("Файл создан, содержимое: " + FileManager.ReadFile(filePath));

            // 2
            bool exists = FileManager.FileExists(filePath);
            Console.WriteLine($"Файл существует: {exists}");

            // 3
            long size = FileInfoProvider.GetFileSize(filePath);
            DateTime created = FileInfoProvider.GetCreationTime(filePath);
            DateTime modified = FileInfoProvider.GetLastWriteTime(filePath);
            Console.WriteLine($"Размер: {size} байт, Создан: {created}, Изменён: {modified}");

            // 4
            FileManager.CopyFile(filePath, copyPath);
            Console.WriteLine($"Копия существует: {FileManager.FileExists(copyPath)}");

            // 5
            Directory.CreateDirectory(movedDir);
            FileManager.MoveFile(filePath, movedPath);
            Console.WriteLine($"Перемещённый файл существует: {FileManager.FileExists(movedPath)}");

            // 6
            FileManager.RenameFile(movedPath, renamedPath);
            Console.WriteLine($"Переименованный файл существует: {FileManager.FileExists(renamedPath)}");

            // 7
            try
            {
                FileManager.DeleteFile(Path.Combine(baseDir, "nonexistent.file"));
                Console.WriteLine("Попытка удаления выполнена (ошибки не возникло)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении: {ex.Message}");
            }

            // 8
            FileManager.CreateAndWriteFile(tempFile, "Temporary");
            int comparison = FileInfoProvider.CompareFilesBySize(renamedPath, tempFile);
            Console.WriteLine($"Сравнение размеров файлов: {comparison}");

            // 9
            FileManager.DeleteFilesByPattern(baseDir, $"*.{initials}");
            Console.WriteLine($"Файлы с расширением .{initials} удалены.");

            // 10
            Console.WriteLine("Файлы в директории:");
            foreach (string file in FileManager.ListFiles(baseDir))
            {
                Console.WriteLine(file);
            }

            // 11
            FileInfoProvider.SetReadOnly(tempFile, true);
            try
            {
                FileManager.CreateAndWriteFile(tempFile, "Попытка записи в защищённый файл");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ожидаемая ошибка записи: {ex.Message}");
            }
            FileInfoProvider.SetReadOnly(tempFile, false);

            // 12
            Console.WriteLine($"Чтение: {FileInfoProvider.CanRead(tempFile)}");
            Console.WriteLine($"Запись: {FileInfoProvider.CanWrite(tempFile)}");

            // Очистка
            FileManager.DeleteFile(tempFile);
            FileManager.DeleteFile(renamedPath);
            FileManager.DeleteFile(copyPath);
            Directory.Delete(movedDir);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
        }
    }
}
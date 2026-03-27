using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
public class FileWatcher
{
    private FileSystemWatcher watcher;

    public FileWatcher(string path)
    {
        watcher = new FileSystemWatcher(path);
        watcher.Created += OnCreated;
        watcher.Deleted += OnDeleted;
        watcher.Changed += OnChanged;
        watcher.Renamed += OnRenamed;
        watcher.EnableRaisingEvents = true;
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"[СОЗДАН] {e.Name}");
        SendEmailNotification(e.Name);
    }

    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"[УДАЛЁН] {e.Name}");
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"[ИЗМЕНЁН] {e.Name}");
    }

    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        Console.WriteLine($"[ПЕРЕИМЕНОВАН] {e.OldName} -> {e.Name}");
    }

    private void SendEmailNotification(string fileName)
    {
        Console.WriteLine($"EMAIL УВЕДОМЛЕНИЕ: Создан новый файл '{fileName}'");
    }
}

}

using System.IO.MemoryMappedFiles;
using System.Text;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace Petrov_Tema_16.Services
{
    public class NotificationService
    {
        private const string MapName = "HotelNotifications";
        private MemoryMappedFile _mmf;
        private CancellationTokenSource _cts;

        public event Action<string> NotificationReceived;

        public void StartListening()
        {
            _cts = new CancellationTokenSource();
            _mmf = MemoryMappedFile.CreateOrOpen(MapName, 1024);
            var view = _mmf.CreateViewAccessor();
            var token = _cts.Token;
            Task.Run(() =>
            {
                byte[] buffer = new byte[1024];
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        int bytesRead = view.ReadArray(0, buffer, 0, buffer.Length);
                        if (bytesRead > 0)
                        {
                            string msg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            if (!string.IsNullOrWhiteSpace(msg))   // ← проверка на пустоту
                                NotificationReceived?.Invoke(msg);
                            view.WriteArray(0, new byte[1024], 0, 1024);
                        }
                        Thread.Sleep(500);
                    }
                    catch { }
                }
            });
        }

        public void StopListening()
        {
            _cts?.Cancel();
            _mmf?.Dispose();
        }

        public void SendNotification(string message)
        {
            using var mmf = MemoryMappedFile.OpenExisting(MapName);
            using var view = mmf.CreateViewAccessor();
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            view.WriteArray(0, bytes, 0, bytes.Length);
        }
    }
}
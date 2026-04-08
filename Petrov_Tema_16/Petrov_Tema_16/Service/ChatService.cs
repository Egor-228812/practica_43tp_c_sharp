using System;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Petrov_Tema_16.Services
{
    public class ChatService
    {
        private NamedPipeServerStream _server;
        private NamedPipeClientStream _client;
        private CancellationTokenSource _cts;
        public event Action<string> MessageReceived;
        public event Action<string> StatusChanged;

        public async Task<bool> StartServerAsync(string pipeName = "hotelchat", int timeoutMs = 10000)
        {
            try
            {
                _server = new NamedPipeServerStream(pipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
                StatusChanged?.Invoke("Ожидание подключения гостя...");
                var connectTask = _server.WaitForConnectionAsync();
                var timeoutTask = Task.Delay(timeoutMs);
                if (await Task.WhenAny(connectTask, timeoutTask) == timeoutTask)
                {
                    StatusChanged?.Invoke("Таймаут: никто не подключился.");
                    return false;
                }
                StatusChanged?.Invoke("Гость подключился. Чат готов.");
                _ = Task.Run(() => ReadMessagesAsync(_server));
                return true;
            }
            catch (Exception ex)
            {
                StatusChanged?.Invoke($"Ошибка сервера: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ConnectClientAsync(string pipeName = "hotelchat", int timeoutMs = 5000)
        {
            try
            {
                _client = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut, PipeOptions.Asynchronous);
                StatusChanged?.Invoke("Подключение к серверу...");
                var connectTask = _client.ConnectAsync(timeoutMs);
                await connectTask;
                StatusChanged?.Invoke("Подключено к серверу.");
                _ = Task.Run(() => ReadMessagesAsync(_client));
                return true;
            }
            catch (Exception ex)
            {
                StatusChanged?.Invoke($"Ошибка клиента: {ex.Message}. Убедитесь, что сервер запущен.");
                return false;
            }
        }

        private async Task ReadMessagesAsync(PipeStream stream)
        {
            var buffer = new byte[4096];
            while (true)
            {
                try
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    MessageReceived?.Invoke(message);
                }
                catch { break; }
            }
            StatusChanged?.Invoke("Соединение разорвано.");
        }

        public async Task SendMessageAsync(string message, bool isServer)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            if (isServer && _server != null && _server.IsConnected)
                await _server.WriteAsync(bytes, 0, bytes.Length);
            else if (!isServer && _client != null && _client.IsConnected)
                await _client.WriteAsync(bytes, 0, bytes.Length);
        }

        public void Dispose()
        {
            _server?.Dispose();
            _client?.Dispose();
            _cts?.Cancel();
        }
    }
}
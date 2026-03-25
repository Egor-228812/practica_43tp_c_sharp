using System;
using System.IO;
using System.Net.Http;

namespace Task2
{
    internal class RequestHandler
    {
        private ApiClient _apiclient = new ApiClient();

        public void ProcessRequest(string url)
        {
            string logs_file = "Logs.txt";
            if (!File.Exists(logs_file))
            {
                using (File.Create(logs_file)) { }
            }
            try
            {
                _apiclient.SendRequest(url);
            }
            catch (HttpRequestException ex)
            {
                string logEntry = $"[{DateTime.Now}] {ex.Message}{Environment.NewLine}StackTrace:{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}";
                File.AppendAllText(logs_file, logEntry);

                throw new ApiException("Ошибка при отправке API-запроса", ex);
            }
        }
    }
}
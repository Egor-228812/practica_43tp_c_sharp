using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    internal class ApiClient
    {
        public void SendRequest(string url)
        {
            throw new HttpRequestException("Поступление запроса - сервер недоступен");
        }
    }
}

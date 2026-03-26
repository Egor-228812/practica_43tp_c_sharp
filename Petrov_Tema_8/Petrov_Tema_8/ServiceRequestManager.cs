using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    internal class ServiceRequestManager
    {
        private Queue<ServiceRequest> queue;
        public ServiceRequestManager() => queue = new Queue<ServiceRequest>(); 

        public void AddRequest(ServiceRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Заявка не моджет быть пустой");
            queue.Enqueue(request);
            Console.WriteLine($"Заявка {request.Id} добавлена");
        }

        public void RemoveRequest()
        {
            if (queue.Count == 0) throw new InvalidOperationException("Очередь пуста - нам нечего удалять");
            queue.Dequeue();
            Console.WriteLine("Первая заявка была удалена");
        }

        public ServiceRequest FindRequest(int id)
        {
            foreach (var req in queue)
            {
                if (req.Id  == id) return req;
            }
            throw new KeyNotFoundException($"заявка с id: {id} не найдена");
        }
        public void SortRequest()
        {
            List<ServiceRequest> list = new List<ServiceRequest>(queue);
            list.Sort((a,b) => a.Id.CompareTo(b.Id));
            int length = queue.Count;
            foreach(var req in list) 
                queue.Enqueue(req);
            Console.WriteLine("Очередь отсортирована по Id");
            for (int i = 0; i < length; i++) queue.Dequeue();
        }

        public void ShowRequest()
        {
            if (queue.Count == 0)
            {
                Console.WriteLine("очередь пуста");
                return;
            }
            Console.WriteLine("текущая очередь: ");
            foreach (var req in queue)
                Console.WriteLine($"Id: {req.Id}. клиент: {req.CustomerName}. Тип: {req.RequestType}");
        }
    }
}

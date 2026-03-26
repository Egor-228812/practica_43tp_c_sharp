using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class LinkedListManager<T>
    {
        private MyLinkedList<T> list;

        public LinkedListManager()
        {
            list = new MyLinkedList<T>();
        }

        public void AddFirst(T item) => list.AddFirst(item);
        public void AddLast(T item) => list.AddLast(item);
        public bool Remove(T item) => list.Remove(item);
        public bool Contains(T item) => list.Find(item) != null;
        public void Display() => list.Display();
    }

}

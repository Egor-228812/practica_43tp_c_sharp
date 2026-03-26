using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class MyLinkedList<T>
    {
        public class Node
        {
            public T Value;
            public Node Prev;
            public Node Next;
            public Node(T value) => Value = value;
        }

        private Node head;
        private Node tail;

        public void AddFirst(T item)
        {
            Node newNode = new Node(item);
            if (head == null)
            {
                head = tail = newNode;
            }
            else
            {
                newNode.Next = head;
                head.Prev = newNode;
                head = newNode;
            }
        }

        public void AddLast(T item)
        {
            Node newNode = new Node(item);
            if (tail == null)
            {
                head = tail = newNode;
            }
            else
            {
                newNode.Prev = tail;
                tail.Next = newNode;
                tail = newNode;
            }
        }

        public bool Remove(T item)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    if (current.Prev != null)
                        current.Prev.Next = current.Next;
                    else
                        head = current.Next;

                    if (current.Next != null)
                        current.Next.Prev = current.Prev;
                    else
                        tail = current.Prev;

                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public Node Find(T item)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Value.Equals(item))
                    return current;
                current = current.Next;
            }
            return null;
        }

        public void Display()
        {
            Node current = head;
            while (current != null)
            {
                Console.Write(current.Value + " ");
                current = current.Next;
            }
            Console.WriteLine();
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class Smartphone
    {
        public string Name { get; set; }
        public Battery Battery { get; set; }
        public App[] Apps { get; set; }
        private int appCount;

        public Smartphone(string name)
        {
            Name = name;
            Battery = new Battery("Встроенный аккумулятор");
            Apps = new App[10];
            appCount = 0;
        }

        public void AddApp(App app)
        {
            if (appCount < Apps.Length)
            {
                Apps[appCount] = app;
                appCount++;
            }
            else
            {
                Console.WriteLine("Нельзя добавить больше приложений, массив заполнен.");
            }
        }

        public void ShowInstalledApps()
        {
            Console.WriteLine($"Приложения на {Name}:");
            for (int i = 0; i < appCount; i++)
            {
                Console.WriteLine($"- {Apps[i].Name}");
            }
            if (appCount == 0)
                Console.WriteLine("Нет установленных приложений.");
        }
    }

}

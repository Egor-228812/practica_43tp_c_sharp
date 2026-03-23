using Task2;
class Program
{
    static void Main()
    {
        Smartphone phone1 = new Smartphone("iPhone 13");
        Smartphone phone2 = new Smartphone("Samsung Galaxy S21");

        App app1 = new App("Telegram");
        App app2 = new App("WhatsApp");
        App app3 = new App("Instagram");

        phone1.AddApp(app1);
        phone1.AddApp(app2);
        phone2.AddApp(app3);

        User user = new User(new Smartphone[] { phone1, phone2 });

        foreach (Smartphone phone in user.Smartphones)
        {
            phone.ShowInstalledApps();
            Console.WriteLine();
        }
    }
}
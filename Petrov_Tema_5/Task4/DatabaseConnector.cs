using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
public class DatabaseConnector : ISqlDatabase, INoSqlDatabase
{
    void ISqlDatabase.Connect()
    {
        Console.WriteLine("Подключение к SQL базе данных");
    }

    void INoSqlDatabase.Connect()
    {
        Console.WriteLine("Подключение к NoSQL базе данных");
    }
}

}

using Task4;

public interface ISqlDatabase
{
    void Connect();
}

public interface INoSqlDatabase
{
    void Connect();
}


class Program
{
    static void Main()
    {
        DatabaseConnector connector = new DatabaseConnector();

        ISqlDatabase sql = connector;
        sql.Connect();

        INoSqlDatabase noSql = connector;
        noSql.Connect();
    }
}
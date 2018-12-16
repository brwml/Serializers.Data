using System;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;

namespace Serializers.Data.TestClient
{
    public static class Program
    {
        private static SQLiteConnectionStringBuilder ConnectionString => new SQLiteConnectionStringBuilder { DataSource = "users.db", Version = 3 };

        public static void Main()
        {
            SQLiteConnection.CreateFile(ConnectionString.DataSource);

            using (var connection = new SQLiteConnection(ConnectionString.ConnectionString))
            {
                connection.Open();

                CreateTable(connection);

                while (true)
                {
                    InsertRow(connection);
                    DisplayRows(connection);
                }
            }
        }

        private static void CreateTable(IDbConnection connection)
        {
            var sql = "create table users(name varchar(50), age int)";
            var command = connection.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }

        private static void DisplayRows(IDbConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "select * from users order by age desc";

            var users = command.ExecuteReader().DeserializeAll<User>();

            foreach (var user in users)
            {
                Console.WriteLine($"\tUser: {user.Name} ({user.Age} years old)");
            }

            Console.WriteLine();
        }

        private static void InsertRow(IDbConnection connection)
        {
            Console.Write("Name (or \"exit\" or \"quit\"): ");
            var name = Console.ReadLine();

            if (name.Equals("exit", StringComparison.OrdinalIgnoreCase) || name.Equals("quit", StringComparison.OrdinalIgnoreCase))
            {
                Process.GetCurrentProcess().Kill();
            }

            Console.Write("Age: ");
            var age = Console.ReadLine();

            var command = connection.CreateCommand();
            command.CommandText = $"insert into users(name, age) values ('{name}', {age})";
            command.ExecuteNonQuery();
        }
    }

    public class User
    {
        public int Age
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }
    }
}
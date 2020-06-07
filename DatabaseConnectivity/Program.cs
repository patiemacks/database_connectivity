using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnectivity
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string provider = ConfigurationManager.AppSettings["provider"];

            string connetionString = ConfigurationManager.AppSettings["connectionString"];

            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);

            using (DbConnection connection = factory.CreateConnection())
            {
                if (connection == null)
                {
                    Console.WriteLine("Connection Error");
                    Console.ReadLine();
                    return;
                }

                connection.ConnectionString = connetionString;
                connection.Open();

                DbCommand command = factory.CreateCommand();

                if (command == null)
                {
                    Console.WriteLine("Command Error");
                    Console.ReadLine();
                    return;
                }
                command.Connection = connection;

                command.CommandText = "Select * From Student";

                using (DbDataReader dataReader=command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Console.WriteLine($"Student Name is :{dataReader["FirstName"]}"+" "+ $"{dataReader["LastName"]}"+" "+ $", his/her Guardian is :{dataReader["Guardian"]}"+" "+ $"and resides at :{dataReader["Address"]}");
                    }
                }

                Console.ReadLine();
            }
        }
    }
}

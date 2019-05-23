using ConsoleAppAdo.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAdo
{


    public class Program
    {
        private const string ConnectionString = 
            @"Data Source=.;User ID=sa;Password=P@$$w0rd;Initial Catalog=SchoolDB;";

        static async Task GetData()
        {
            List<Student> Results = new List<Student>();

            string id = "10";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT [Id], [FirstName], [LastName], [DateOfBirth], [ClassId] " +
                                      "FROM [Student] " +
                                      "WHERE [Id] = @id";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new SqlParameter("id", id));

                Console.WriteLine("Opening Connection");
                await connection.OpenAsync();
                Console.WriteLine("Executing Query");
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    Console.WriteLine("Fetching results");
                    while (await reader.ReadAsync())
                    {
                        Results.Add(new Student
                        {
                            Id = reader.GetInt32(0), //reader["Id"]
                            FirstName = reader.GetString(1), //reader["FirstName"]
                            LastName = reader.GetString(2), //reader["LastName"]
                            DateOfBirth = reader.GetDateTime(3),//reader["DateOfBirth"]
                            ClassId = reader.GetInt32(4) //reader["ClassId"]
                        });
                    }
                }
            }

            foreach (var student in Results)
            {
                Console.WriteLine("Student Details:" +
                                  $"Id: {student.Id}" +
                                  $"Name: {student.LastName}, {student.FirstName}",
                                  $"Date of Birth: {student.DateOfBirth}");
            }

        }

        public static void Main(string[] args)
        {
            var clienteHandler = new ClienteHandler();
            var cliente = clienteHandler.Adicionar("Y666", "Fulano de Tal", DateTime.Now.AddMonths(-15));

            


            var pedidoHandler = new PedidoHandler();
            pedidoHandler.Adicionar();



            Console.ReadLine();
            Task.Run(async () => await GetData());
            Console.WriteLine("Ending Execution");
            Console.ReadLine();
        }
    }
}

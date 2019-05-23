using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace ConsoleAppAdo.Domain
{
    public class ClienteHandler
    {

        private readonly string connectionString;


        public ClienteHandler()
        {

            connectionString = ConfigurationManager.ConnectionStrings["MeuSistema"].ToString();

        }

        public void Adicionar(string codigoCliente, string nomeCliente, DateTime dataCadastro)
        {
            var cliente = new Cliente(codigoCliente, nomeCliente, dataCadastro);
            if (!cliente.EhValido())
            {
                Console.WriteLine($"Corrija as seguinte validacoes");
                foreach (var item in cliente.Validacoes)
                {
                   Console.WriteLine($"   {item.Key} {item.Value}");
                }
                return;
            }



            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT 
                                        [Id], [FirstName], [LastName], [DateOfBirth], [ClassId] " +
                                      "FROM [Student] " +
                                      "WHERE [Id] = @id";

                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("id", id));


                Console.WriteLine("Opening Connection");
                connection.OpenAsync();
                Console.WriteLine("Executing Query");

                using (SqlDataReader reader = command.ExecuteReader())
                {

                    Console.WriteLine("Fetching results");
                    while (reader.Read())
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



        }
    }
}

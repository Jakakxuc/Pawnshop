using Microsoft.Data.SqlClient;
using PawnshopApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.Repository
{
    public class DocumentNumberRepository : IDocumentNumberRepository
    {

        private readonly string _connectionString;

        public DocumentNumberRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public long GetCurrentNumber()
        {

            string query = "SELECT MAX(LastNumber) FROM DocumentNumber";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    long lastNumber = Convert.ToInt64(result);
                    return lastNumber + 1;
                }

                return 1; // If no previous document found, start from 1
            }

        }

        public void UpdateLastDocumentNumber(long lastNumber)
        {
            string query = "UPDATE DocumentNumber SET LastNumber = @LastNumber";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LastNumber", lastNumber);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}

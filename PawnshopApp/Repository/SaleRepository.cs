using Microsoft.Data.SqlClient;
using PawnshopApp.Entities;
using PawnshopApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly string _connectionString;

        public SaleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Sale> AddAsync(Sale entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "INSERT INTO Sales (UUID, PawnedItemUUID, SaleAmount, SaleDate) " +
                            "VALUES (@UUID, @PawnedItemUUID, @SaleAmount, @SaleDate)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UUID", entity.UUID);
                    command.Parameters.AddWithValue("@PawnedItemUUID", entity.PawnedItemUUID);
                    command.Parameters.AddWithValue("@SaleAmount", entity.SaleAmount);
                    command.Parameters.AddWithValue("@SaleDate", entity.SaleDate);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return entity;
        }

        public async Task DeleteAsync(Guid uuid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "DELETE FROM Sales WHERE UUID = @UUID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UUID", uuid);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public IEnumerable<Sale> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            var sales = new List<Sale>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT UUID, PawnedItemUUID, SaleAmount, SaleDate FROM Sales";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var sale = new Sale
                        {
                            UUID = (Guid)reader["UUID"],
                            PawnedItemUUID = (Guid)reader["PawnedItemUUID"],
                            SaleAmount = (decimal)reader["SaleAmount"],
                            SaleDate = (DateTime)reader["SaleDate"]
                        };

                        sales.Add(sale);
                    }
                }
            }

            return sales;
        }

        public async Task<Sale> GetByUUIDAsync(Guid uuid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT UUID, PawnedItemUUID, SaleAmount, SaleDate FROM Sales WHERE UUID = @UUID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UUID", uuid);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var sale = new Sale
                            {
                                UUID = (Guid)reader["UUID"],
                                PawnedItemUUID = (Guid)reader["PawnedItemUUID"],
                                SaleAmount = (decimal)reader["SaleAmount"],
                                SaleDate = (DateTime)reader["SaleDate"]
                            };

                            return sale;
                        }
                    }
                }
            }

            return null;
        }

        public async Task<Sale> UpdateAsync(Sale entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "UPDATE Sales SET PawnedItemUUID = @PawnedItemUUID, SaleAmount = @SaleAmount, " +
                            "SaleDate = @SaleDate WHERE UUID = @UUID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PawnedItemUUID", entity.PawnedItemUUID);
                    command.Parameters.AddWithValue("@SaleAmount", entity.SaleAmount);
                    command.Parameters.AddWithValue("@SaleDate", entity.SaleDate);
                    command.Parameters.AddWithValue("@UUID", entity.UUID);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return entity;
        }
    }
}

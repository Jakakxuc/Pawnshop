using Microsoft.Data.SqlClient;
using PawnshopApp.Entities;
using PawnshopApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PawnshopApp.Repository
{
    public class PawnedItemRepository : IPawnedItemRepository
    {
        private readonly string _connectionString;

        public PawnedItemRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<PawnedItem> AddAsync(PawnedItem entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "INSERT INTO PawnedItems (UUID, LoanUUID, Description, EstimatedValue, PawnDate, ExpiryDate, IsSold) " +
                            "VALUES (@UUID, @LoanUUID, @Description, @EstimatedValue, @PawnDate, @ExpiryDate, @IsSold)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UUID", entity.UUID);
                    command.Parameters.AddWithValue("@LoanUUID", entity.LoanUUID);
                    command.Parameters.AddWithValue("@Description", entity.Description);
                    command.Parameters.AddWithValue("@EstimatedValue", entity.EstimatedValue);
                    command.Parameters.AddWithValue("@PawnDate", entity.PawnDate);
                    command.Parameters.AddWithValue("@ExpiryDate", entity.ExpiryDate);
                    command.Parameters.AddWithValue("@IsSold", entity.IsSold);

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

                var query = "DELETE FROM PawnedItems WHERE UUID = @UUID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UUID", uuid);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public IEnumerable<PawnedItem> GetAll()
        {
            var pawnedItems = new List<PawnedItem>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT UUID, LoanUUID, Description, EstimatedValue, PawnDate, ExpiryDate, IsSold FROM PawnedItems";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pawnedItem = new PawnedItem
                        {
                            UUID = (Guid)reader["UUID"],
                            LoanUUID = (Guid)reader["LoanUUID"],
                            Description = reader["Description"].ToString(),
                            EstimatedValue = (decimal)reader["EstimatedValue"],
                            PawnDate = (DateTime)reader["PawnDate"],
                            ExpiryDate = (DateTime)reader["ExpiryDate"],
                            IsSold = (bool)reader["IsSold"]
                        };

                        pawnedItems.Add(pawnedItem);
                    }
                }
            }

            return pawnedItems;
        }

        public async Task<IEnumerable<PawnedItem>> GetAllAsync()
        {
            var pawnedItems = new List<PawnedItem>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT UUID, LoanUUID, Description, EstimatedValue, PawnDate, ExpiryDate, IsSold FROM PawnedItems";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var pawnedItem = new PawnedItem
                        {
                            UUID = (Guid)reader["UUID"],
                            LoanUUID = (Guid)reader["LoanUUID"],
                            Description = reader["Description"].ToString(),
                            EstimatedValue = (decimal)reader["EstimatedValue"],
                            PawnDate = (DateTime)reader["PawnDate"],
                            ExpiryDate = (DateTime)reader["ExpiryDate"],
                            IsSold = (bool)reader["IsSold"]
                        };

                        pawnedItems.Add(pawnedItem);
                    }
                }
            }

            return pawnedItems;
        }

        public async Task<PawnedItem> GetByUUIDAsync(Guid uuid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT UUID, LoanUUID, Description, EstimatedValue, PawnDate, ExpiryDate, IsSold FROM PawnedItems WHERE UUID = @UUID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UUID", uuid);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var pawnedItem = new PawnedItem
                            {
                                UUID = (Guid)reader["UUID"],
                                LoanUUID = (Guid)reader["LoanUUID"],
                                Description = reader["Description"].ToString(),
                                EstimatedValue = (decimal)reader["EstimatedValue"],
                                PawnDate = (DateTime)reader["PawnDate"],
                                ExpiryDate = (DateTime)reader["ExpiryDate"],
                                IsSold = (bool)reader["IsSold"]
                            };

                            return pawnedItem;
                        }
                    }
                }
            }

            return null;
        }

        public async Task<PawnedItem> UpdateAsync(PawnedItem entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "UPDATE PawnedItems SET LoanUUID = @LoanUUID, Description = @Description, EstimatedValue = @EstimatedValue, " +
                            "PawnDate = @PawnDate, ExpiryDate = @ExpiryDate, IsSold = @IsSold WHERE UUID = @UUID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LoanUUID", entity.LoanUUID);
                    command.Parameters.AddWithValue("@Description", entity.Description);
                    command.Parameters.AddWithValue("@EstimatedValue", entity.EstimatedValue);
                    command.Parameters.AddWithValue("@PawnDate", entity.PawnDate);
                    command.Parameters.AddWithValue("@ExpiryDate", entity.ExpiryDate);
                    command.Parameters.AddWithValue("@IsSold", entity.IsSold);
                    command.Parameters.AddWithValue("@UUID", entity.UUID);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return entity;
        }
    }
}

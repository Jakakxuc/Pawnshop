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
    public class InsuranceRepository : IInsuranceRepository
    {
        private readonly string _connectionString;

        public InsuranceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Insurance> AddAsync(Insurance entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "INSERT INTO Insurances (UUID, PawnedItemUUID, InsuranceAmount, InsuranceDate) " +
                            "VALUES (@UUID, @PawnedItemUUID, @InsuranceAmount, @InsuranceDate)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UUID", entity.UUID);
                    command.Parameters.AddWithValue("@PawnedItemUUID", entity.PawnedItemUUID);
                    command.Parameters.AddWithValue("@InsuranceAmount", entity.InsuranceAmount);
                    command.Parameters.AddWithValue("@InsuranceDate", entity.InsuranceDate);

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

                var query = "DELETE FROM Insurances WHERE UUID = @UUID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UUID", uuid);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public IEnumerable<Insurance> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Insurance>> GetAllAsync()
        {
            var insurances = new List<Insurance>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT UUID, PawnedItemUUID, InsuranceAmount, InsuranceDate FROM Insurances";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var insurance = new Insurance
                        {
                            UUID = (Guid)reader["UUID"],
                            PawnedItemUUID = (Guid)reader["PawnedItemUUID"],
                            InsuranceAmount = (decimal)reader["InsuranceAmount"],
                            InsuranceDate = (DateTime)reader["InsuranceDate"]
                        };

                        insurances.Add(insurance);
                    }
                }
            }

            return insurances;
        }

        public async Task<Insurance> GetByUUIDAsync(Guid uuid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT UUID, PawnedItemUUID, InsuranceAmount, InsuranceDate FROM Insurances WHERE UUID = @UUID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UUID", uuid);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var insurance = new Insurance
                            {
                                UUID = (Guid)reader["UUID"],
                                PawnedItemUUID = (Guid)reader["PawnedItemUUID"],
                                InsuranceAmount = (decimal)reader["InsuranceAmount"],
                                InsuranceDate = (DateTime)reader["InsuranceDate"]
                            };

                            return insurance;
                        }
                    }
                }
            }

            return null;
        }

        public async Task<Insurance> UpdateAsync(Insurance entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "UPDATE Insurances SET PawnedItemUUID = @PawnedItemUUID, InsuranceAmount = @InsuranceAmount, " +
                            "InsuranceDate = @InsuranceDate WHERE UUID = @UUID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PawnedItemUUID", entity.PawnedItemUUID);
                    command.Parameters.AddWithValue("@InsuranceAmount", entity.InsuranceAmount);
                    command.Parameters.AddWithValue("@InsuranceDate", entity.InsuranceDate);
                    command.Parameters.AddWithValue("@UUID", entity.UUID);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return entity;
        }
    }
}

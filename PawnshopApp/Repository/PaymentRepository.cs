using Microsoft.Data.SqlClient;
using PawnshopApp.Entities;
using PawnshopApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PawnshopApp.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly string _connectionString;

        public PaymentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Payment> AddAsync(Payment entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "INSERT INTO Payments (UUID, LoanUUID, PaymentAmount, PaymentDate) VALUES (@UUID, @LoanUUID, @PaymentAmount, @PaymentDate)";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UUID", entity.UUID);
                    command.Parameters.AddWithValue("@LoanUUID", entity.LoanUUID);
                    command.Parameters.AddWithValue("@PaymentAmount", entity.PaymentAmount);
                    command.Parameters.AddWithValue("@PaymentDate", entity.PaymentDate);
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
                var sql = "DELETE FROM Payments WHERE UUID = @UUID";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UUID", uuid);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public IEnumerable<Payment> GetAll()
        {
            var payments = new List<Payment>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM Payments";
                using (var command = new SqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        payments.Add(new Payment
                        {
                            UUID = (Guid)reader["UUID"],
                            LoanUUID = (Guid)reader["LoanUUID"],
                            PaymentAmount = (decimal)reader["PaymentAmount"],
                            PaymentDate = (DateTime)reader["PaymentDate"]
                        });
                    }
                }
            }
            return payments;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            var payments = new List<Payment>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT * FROM Payments";
                using (var command = new SqlCommand(sql, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        payments.Add(new Payment
                        {
                            UUID = (Guid)reader["UUID"],
                            LoanUUID = (Guid)reader["LoanUUID"],
                            PaymentAmount = (decimal)reader["PaymentAmount"],
                            PaymentDate = (DateTime)reader["PaymentDate"]
                        });
                    }
                }
            }
            return payments;
        }

        public async Task<Payment> GetByUUIDAsync(Guid uuid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT * FROM Payments WHERE UUID = @UUID";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UUID", uuid);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Payment
                            {
                                UUID = (Guid)reader["UUID"],
                                LoanUUID = (Guid)reader["LoanUUID"],
                                PaymentAmount = (decimal)reader["PaymentAmount"],
                                PaymentDate = (DateTime)reader["PaymentDate"]
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<Payment> UpdateAsync(Payment entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "UPDATE Payments SET LoanUUID = @LoanUUID, PaymentAmount = @PaymentAmount, PaymentDate = @PaymentDate WHERE UUID = @UUID";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UUID", entity.UUID);
                    command.Parameters.AddWithValue("@LoanUUID", entity.LoanUUID);
                    command.Parameters.AddWithValue("@PaymentAmount", entity.PaymentAmount);
                    command.Parameters.AddWithValue("@PaymentDate", entity.PaymentDate);
                    await command.ExecuteNonQueryAsync();
                }
            }
            return entity;
        }
    }
}

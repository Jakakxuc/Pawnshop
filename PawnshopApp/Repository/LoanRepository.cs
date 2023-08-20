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
    public class LoanRepository : ILoanRepository
    {
        private readonly string _connectionString;

        public LoanRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Loan> AddAsync(Loan entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "INSERT INTO Loans (UUID, LoanAmount, InterestRate, LoanDate, Status, DocumentUUID) VALUES (@UUID, @LoanAmount, @InterestRate, @LoanDate, @Status, @DocumentUUID)";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UUID", entity.UUID);
                    command.Parameters.AddWithValue("@DocumentUUID", entity.DocumentUUID);
                    command.Parameters.AddWithValue("@LoanAmount", entity.LoanAmount);
                    command.Parameters.AddWithValue("@InterestRate", entity.InterestRate);
                    command.Parameters.AddWithValue("@LoanDate", entity.LoanDate);
                    command.Parameters.AddWithValue("@Status", (int)entity.Status);
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
                var sql = "DELETE FROM Loans WHERE UUID = @UUID";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UUID", uuid);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public IEnumerable<Loan> GetAll()
        {
            var loans = new List<Loan>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM Loans";
                using (var command = new SqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        loans.Add(new Loan
                        {
                            UUID = (Guid)reader["UUID"],
                            DocumentUUID = (Guid)reader["DocumentUUID"],
                            LoanAmount = (decimal)reader["LoanAmount"],
                            InterestRate = (decimal)reader["InterestRate"],
                            LoanDate = (DateTime)reader["LoanDate"],
                            Status = (LoanStatus)(int)reader["Status"]
                        });
                    }
                }
            }
            return loans;
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            var loans = new List<Loan>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT * FROM Loans";
                using (var command = new SqlCommand(sql, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        loans.Add(new Loan
                        {
                            UUID = (Guid)reader["UUID"],
                            DocumentUUID = (Guid)reader["DocumentUUID"],
                            LoanAmount = (decimal)reader["LoanAmount"],
                            InterestRate = (decimal)reader["InterestRate"],
                            LoanDate = (DateTime)reader["LoanDate"],
                            Status = (LoanStatus)(int)reader["Status"]
                        });
                    }
                }
            }
            return loans;
        }

        public Loan GetByUUID(Guid uuid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                 connection.Open();
                var sql = "SELECT * FROM Loans WHERE UUID = @UUID";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UUID", uuid);
                    using (var reader =  command.ExecuteReader())
                    {
                        if ( reader.Read())
                        {
                            return new Loan
                            {
                                UUID = (Guid)reader["UUID"],
                                DocumentUUID = (Guid)reader["DocumentUUID"],
                                LoanAmount = (decimal)reader["LoanAmount"],
                                InterestRate = (decimal)reader["InterestRate"],
                                LoanDate = (DateTime)reader["LoanDate"],
                                Status = (LoanStatus)(int)reader["Status"]
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<Loan> GetByUUIDAsync(Guid uuid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT * FROM Loans WHERE UUID = @UUID";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UUID", uuid);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Loan
                            {
                                UUID = (Guid)reader["UUID"],
                                DocumentUUID = (Guid)reader["DocumentUUID"],
                                LoanAmount = (decimal)reader["LoanAmount"],
                                InterestRate = (decimal)reader["InterestRate"],
                                LoanDate = (DateTime)reader["LoanDate"],
                                Status = (LoanStatus)(int)reader["Status"]
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<Loan> UpdateAsync(Loan entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "UPDATE Loans SET LoanAmount = @LoanAmount, InterestRate = @InterestRate, LoanDate = @LoanDate, Status = @Status WHERE UUID = @UUID";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UUID", entity.UUID);
                    command.Parameters.AddWithValue("@DocumentUUID", entity.DocumentUUID);
                    command.Parameters.AddWithValue("@LoanAmount", entity.LoanAmount);
                    command.Parameters.AddWithValue("@InterestRate", entity.InterestRate);
                    command.Parameters.AddWithValue("@LoanDate", entity.LoanDate);
                    command.Parameters.AddWithValue("@Status", (int)entity.Status);
                    await command.ExecuteNonQueryAsync();
                }
            }
            return entity;
        }
    }
}

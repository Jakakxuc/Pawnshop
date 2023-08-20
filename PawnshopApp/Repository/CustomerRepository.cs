using PawnshopApp.Entities;
using PawnshopApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace PawnshopApp.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Customer> AddAsync(Customer entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "INSERT INTO Customers (UUID, Name, Surname, PhoneNumber) VALUES (@UUID, @Name, @Surname, @PhoneNumber)";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UUID", entity.UUID);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@Surname", entity.Surname);
                    command.Parameters.AddWithValue("@PhoneNumber", entity.PhoneNumber);
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
                var sql = "DELETE FROM Customers WHERE UUID = @UUID";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UUID", uuid);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            var customers = new List<Customer>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM Customers";
                using (var command = new SqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer
                        {
                            UUID = (Guid)reader["UUID"],
                            Name = (string)reader["Name"],
                            Surname = (string)reader["Surname"],
                            PhoneNumber = (string)reader["PhoneNumber"]
                        });
                    }
                }
            }
            return customers;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var customers = new List<Customer>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT * FROM Customers";
                using (var command = new SqlCommand(sql, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        customers.Add(new Customer
                        {
                            UUID = (Guid)reader["UUID"],
                            Name = (string)reader["Name"],
                            Surname = (string)reader["Surname"],
                            PhoneNumber = (string)reader["PhoneNumber"]
                        });
                    }
                }
            }
            return customers;
        }

        public Customer GetByUUID(Guid uuid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM Customers WHERE UUID = @UUID";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UUID", uuid);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Customer
                            {
                                UUID = (Guid)reader["UUID"],
                                Name = (string)reader["Name"],
                                Surname = (string)reader["Surname"],
                                PhoneNumber = (string)reader["PhoneNumber"]
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<Customer> GetByUUIDAsync(Guid uuid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT * FROM Customers WHERE UUID = @UUID";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UUID", uuid);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Customer
                            {
                                UUID = (Guid)reader["UUID"],
                                Name = (string)reader["Name"],
                                Surname = (string)reader["Surname"],
                                PhoneNumber = (string)reader["PhoneNumber"]
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<Customer> UpdateAsync(Customer entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "UPDATE Customers SET Name = @Name, Surname = @Surname, PhoneNumber = @PhoneNumber WHERE UUID = @UUID";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UUID", entity.UUID);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@Surname", entity.Surname);
                    command.Parameters.AddWithValue("@PhoneNumber", entity.PhoneNumber);
                    await command.ExecuteNonQueryAsync();
                }
            }
            return entity;
        }
    }
}

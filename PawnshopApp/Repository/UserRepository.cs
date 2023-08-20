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
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<User> AddAsync(User entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "INSERT INTO Users (UUID, Username, PasswordHash) " +
                            "VALUES (@UUID, @Username, @PasswordHash)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UUID", entity.UUID);
                    command.Parameters.AddWithValue("@Username", entity.Username);
                    command.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);

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

                var query = "DELETE FROM Users WHERE UUID = @UUID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UUID", uuid);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = new List<User>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT UUID, Username, PasswordHash FROM Users";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var user = new User
                        {
                            UUID = (Guid)reader["UUID"],
                            Username = reader["Username"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString()
                        };

                        users.Add(user);
                    }
                }
            }

            return users;
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT UUID, Username, PasswordHash FROM Users WHERE Username = @Username";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", login);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var user = new User
                            {
                                UUID = (Guid)reader["UUID"],
                                Username = reader["Username"].ToString(),
                                PasswordHash = reader["PasswordHash"].ToString()
                            };

                            return user;
                        }
                    }
                }
            }

            return null;
        }

        public async Task<User> GetByUUIDAsync(Guid uuid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT UUID, Username, PasswordHash FROM Users WHERE UUID = @UUID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UUID", uuid);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var user = new User
                            {
                                UUID = (Guid)reader["UUID"],
                                Username = reader["Username"].ToString(),
                                PasswordHash = reader["PasswordHash"].ToString()
                            };

                            return user;
                        }
                    }
                }
            }

            return null;
        }

        public async Task<User> UpdateAsync(User entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "UPDATE Users SET Username = @Username, PasswordHash = @PasswordHash WHERE UUID = @UUID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", entity.Username);
                    command.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
                    command.Parameters.AddWithValue("@UUID", entity.UUID);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return entity;
        }
    }
}

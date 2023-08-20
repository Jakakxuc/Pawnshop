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
    public class DocumentRepository : IDocumentRepository
    {
        private readonly string _connectionString;

        public DocumentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Document> AddAsync(Document entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Documents (UUID, Name, Number, CreationDate, CustomerUUID, Comment) VALUES (@UUID, @Name, @Number, @CreationDate, @CustomerUUID, @Comment)";
                    command.Parameters.AddWithValue("@UUID", entity.UUID);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@Number", entity.Number);
                    command.Parameters.AddWithValue("@CreationDate", entity.CreationDate);
                    command.Parameters.AddWithValue("@CustomerUUID", entity.CustomerUUID);
                    command.Parameters.AddWithValue("@Comment", entity.Comment);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return entity;
        }

        public async Task DeleteAsync(Guid uuid)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Documents WHERE UUID = @UUID";
                    command.Parameters.AddWithValue("@UUID", uuid);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public IEnumerable<Document> GetAll()
        {
            List<Document> documents = new List<Document>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT UUID, Name, Number, CreationDate, CustomerUUID, Comment FROM Documents";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            documents.Add(new Document
                            {
                                UUID = new Guid(reader["UUID"].ToString()),
                                Name = reader["Name"].ToString(),
                                Number = (long)reader["Number"],
                                CreationDate = (DateTime)reader["CreationDate"],
                                CustomerUUID = new Guid(reader["CustomerUUID"].ToString()),
                                Comment = reader["Comment"].ToString()
                            });
                        }
                    }
                }
            }

            return documents;
        }

        public async Task<IEnumerable<Document>> GetAllAsync()
        {
            List<Document> documents = new List<Document>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT UUID, Name, Number, CreationDate, CustomerUUID, Comment FROM Documents";

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            documents.Add(new Document
                            {
                                UUID = new Guid(reader["UUID"].ToString()),
                                Name = reader["Name"].ToString(),
                                Number = (long)reader["Number"],
                                CreationDate = (DateTime)reader["CreationDate"],
                                CustomerUUID = new Guid(reader["CustomerUUID"].ToString()),
                                Comment = reader["Comment"].ToString()
                            });
                        }
                    }
                }
            }

            return documents;
        }

        public Document GetByUUID(Guid uuid)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT UUID, Name, Number, CreationDate, CustomerUUID, Comment FROM Documents WHERE UUID = @UUID";
                    command.Parameters.AddWithValue("@UUID", uuid);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Document
                            {
                                UUID = new Guid(reader["UUID"].ToString()),
                                Name = reader["Name"].ToString(),
                                Number = (long)reader["Number"],
                                CreationDate = (DateTime)reader["CreationDate"],
                                CustomerUUID = new Guid(reader["CustomerUUID"].ToString()),
                                Comment = reader["Comment"].ToString()
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public async Task<Document> GetByUUIDAsync(Guid uuid)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT UUID, Name, Number, CreationDate, CustomerUUID, Comment FROM Documents WHERE UUID = @UUID";
                    command.Parameters.AddWithValue("@UUID", uuid);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Document
                            {
                                UUID = new Guid(reader["UUID"].ToString()),
                                Name = reader["Name"].ToString(),
                                Number = (long)reader["Number"],
                                CreationDate = (DateTime)reader["CreationDate"],
                                CustomerUUID = new Guid(reader["CustomerUUID"].ToString()),
                                Comment = reader["Comment"].ToString()
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public async Task<Document> UpdateAsync(Document entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Documents SET Name = @Name, Number = @Number, CreationDate = @CreationDate, CustomerUUID = @CustomerUUID, Comment = @Comment WHERE UUID = @UUID";
                    command.Parameters.AddWithValue("@UUID", entity.UUID);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@Number", entity.Number);
                    command.Parameters.AddWithValue("@CreationDate", entity.CreationDate);
                    command.Parameters.AddWithValue("@CustomerUUID", entity.CustomerUUID);
                    command.Parameters.AddWithValue("@Comment", entity.Comment);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return entity;
        }
    }
}

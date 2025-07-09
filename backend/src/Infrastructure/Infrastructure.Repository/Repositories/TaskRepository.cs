using Core.Domain.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly string _connectionString;
        public TaskRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task CreateAsync(TaskItem task)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = @"INSERT INTO TaskItem (Id, Title, Description, IsCompleted, CreatedAt)
                      VALUES (@Id, @Title, @Description, @IsCompleted, @CreatedAt)";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", task.Id);
            command.Parameters.AddWithValue("@Title", task.Title);
            command.Parameters.AddWithValue("@Description", (object?)task.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);
            command.Parameters.AddWithValue("@CreatedAt", task.CreatedAt);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(string id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = "DELETE FROM TaskItem WHERE Id = @Id";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            var tasks = new List<TaskItem>();
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = "SELECT * FROM TaskItem";
            using var command = new SqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                tasks.Add(new TaskItem
                {
                    Id = reader.GetString(0),
                    Title = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                    IsCompleted = reader.GetBoolean(3),
                    CreatedAt = reader.GetDateTime(4),
                    UpdatedAt = reader.IsDBNull(5) ? null : reader.GetDateTime(5)
                });
            }

            return tasks;
        }

        public async Task<TaskItem?> GetByIdAsync(string id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = "SELECT * FROM TaskItem WHERE Id = @Id";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new TaskItem
                {
                    Id = reader.GetString(0),
                    Title = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                    IsCompleted = reader.GetBoolean(3),
                    CreatedAt = reader.GetDateTime(4),
                    UpdatedAt = reader.IsDBNull(5) ? null : reader.GetDateTime(5)
                };
            }

            return null;
        }

        public async Task UpdateAsync(TaskItem task)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = @"UPDATE TaskItem 
                  SET Title = @Title,
                      Description = @Description,
                      IsCompleted = @IsCompleted,
                      UpdatedAt = @UpdatedAt
                  WHERE Id = @Id";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", task.Id);
            command.Parameters.AddWithValue("@Title", task.Title);
            command.Parameters.AddWithValue("@Description", (object?)task.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);
            command.Parameters.AddWithValue("@UpdatedAt", (object?)task.UpdatedAt ?? DBNull.Value);

            await command.ExecuteNonQueryAsync();
        }
    }
}

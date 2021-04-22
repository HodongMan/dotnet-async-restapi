using System;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

using MySqlConnector;
using Dapper;

using TodoApplication.Core;


namespace TodoApplication.Data
{
    public class TaskRepository : IDisposable
    {
        private readonly IDbConnection _db;

        public TaskRepository(IOptions<ConnectionStrings> connectionStrings)
        {
            _db = new MySqlConnection(connectionStrings.Value.TodoDatabase);
        }

        public void Dispose()
        {
            _db.Close();
        }

        public Task<IEnumerable<Models.Task>> GetAllTasksAsync()
        {
            return _db.QueryAsync<Models.Task>("SELECT * FROM tasks");
        }

        public Task<int> SaveAsync(Models.Task task)
        {
            if (task.Id.HasValue)
            {
                string update = @"UPDATE tasks SET description = @Description, completed = @Completed WHERE id = @Id";
                return _db.ExecuteAsync(update, new { task.Description, task.IsCompleted, task.Id });
            }
            else
            {
                string insert = @"INSERT INTO tasks (description) VALUES (@Description)";
                return _db.ExecuteAsync(insert, new { task.Description });
            }
        }

        public Task<int> DeleteAsync(int taskId)
        {
            string delete = @"DELETE FROM tasks WHERE id = @Id";
            return _db.ExecuteAsync(delete, new { Id = taskId });
        }
    }
}

using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Data
{
    public class TodoDatabase : ITodoDatabase
    {
        private SQLiteAsyncConnection database;

        public TodoDatabase()
        {
        }

        public void InitializeDataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<TodoItem>().Wait();
        }

        public Task<List<TodoItem>> GetItemsAsync()
        {
            return database.Table<TodoItem>().ToListAsync();
        }

        public Task<int> SaveItemAsync(TodoItem task)
        {
            if (task.ID != 0)
            {
                return database.UpdateAsync(task);
            }
            else
            {
                return database.InsertAsync(task);
            }
        }

        public Task<int> DeleteItemAsync(TodoItem task)
        {
            return database.DeleteAsync(task);
        }
    }
}

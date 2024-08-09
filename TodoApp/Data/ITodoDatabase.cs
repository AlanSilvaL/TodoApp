using TodoApp.Models;

namespace TodoApp.Data
{
    public interface ITodoDatabase
    {
        Task<int> DeleteItemAsync(TodoItem task);
        Task<List<TodoItem>> GetItemsAsync();
        void InitializeDataBase(string dbPath);
        Task<int> SaveItemAsync(TodoItem task);
    }
}
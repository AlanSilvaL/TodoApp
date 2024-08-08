using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoApp.Models;
using TodoApp.Data;

namespace TodoApp.ViewModels
{
    public class TodoViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TodoItem> _tasks;
        private ObservableCollection<TodoItem> _allTasks;
        private string _taskName;
        private string _searchText;
        private TodoDatabase _database;

        public ObservableCollection<TodoItem> Tasks
        {
            get => _tasks;
            set
            {
                if (_tasks != value)
                {
                    _tasks = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TaskName
        {
            get => _taskName;
            set
            {
                if (_taskName != value)
                {
                    _taskName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    FilterTasks();
                }
            }
        }

        public ICommand AddTaskCommand { get; }
        public ICommand RemoveTaskCommand { get; }
        public ICommand EditTaskCommand { get; }

        public TodoViewModel()
        {
            _allTasks = new ObservableCollection<TodoItem>();
            Tasks = new ObservableCollection<TodoItem>();
            AddTaskCommand = new Command(async () => await AddTask());
            RemoveTaskCommand = new Command<TodoItem>(async (task) => await RemoveTask(task));
            EditTaskCommand = new Command<TodoItem>(async (task) => await EditTask(task));
        }

        public void Initialize(TodoDatabase database)
        {
            _database = database;
            LoadTasks();
        }

        private async void LoadTasks()
        {
            var tasks = await _database.GetItemsAsync();
            foreach (var task in tasks)
            {
                _allTasks.Add(task);

            }
            Tasks = new ObservableCollection<TodoItem>(_allTasks);
        }

        private async Task AddTask()
        {
            if (!string.IsNullOrWhiteSpace(TaskName))
            {
                var newTask = new TodoItem
                {
                    Name = TaskName,
                    CreatedAt = DateTime.Now,
                    Status = "Pending"
                };
                await _database.SaveItemAsync(newTask);
                _allTasks.Add(newTask);
                FilterTasks();
                TaskName = string.Empty;
            }
        }

        private async Task RemoveTask(TodoItem task)
        {
            await _database.DeleteItemAsync(task);
            _allTasks.Remove(task);
            FilterTasks();
        }

        private async Task EditTask(TodoItem task)
        {
            task.Status = task.Status == "Pending" ? "Completed" : "Pending";
            await _database.SaveItemAsync(task);
            FilterTasks();
        }

        private void FilterTasks()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                Tasks = new ObservableCollection<TodoItem>(_allTasks);
            }
            else
            {
                var filteredTasks = _allTasks.Where(t => t.Name.ToLower().Contains(SearchText.ToLower())).ToList();
                Tasks = new ObservableCollection<TodoItem>(filteredTasks);
            }
            OnPropertyChanged(nameof(Tasks));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

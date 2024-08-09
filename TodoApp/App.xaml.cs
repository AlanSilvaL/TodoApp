using Microsoft.Maui.Controls;
using System.IO;
using TodoApp.Data;
using TodoApp.ViewModels;

namespace TodoApp
{
    public partial class App : Application
    {
        private readonly ITodoDatabase database;

        public App()
        {
            InitializeComponent();
            database = IPlatformApplication.Current?.Services.GetService<ITodoDatabase>();
            database.InitializeDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3"));
            MainPage = new MainPage();
        }
    }
}

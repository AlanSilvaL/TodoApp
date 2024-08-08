using Microsoft.Maui.Controls;
using System.IO;
using TodoApp.Data;
using TodoApp.ViewModels;

namespace TodoApp
{
    public partial class App : Application
    {
        static TodoDatabase database;

        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }

        public static TodoDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new TodoDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3"));
                }
                return database;
            }
        }
    }
}

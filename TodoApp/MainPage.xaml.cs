using TodoApp.ViewModels;
namespace TodoApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var viewModel = new TodoViewModel();
            //viewModel.Initialize(App.Database);
            BindingContext = viewModel;
        }
    }
}

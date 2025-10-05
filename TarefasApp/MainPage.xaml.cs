using System.Linq;
using Microsoft.Maui.Controls;
using TarefasApp.Models;
using TarefasApp.Services;

namespace TarefasApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadTasks(); // carregamento inicial
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadTasks(); // recarrega toda vez que a página volta
        }

        private void LoadTasks()
        {
            var tasks = TasksService.Instance.GetAll();
            TasksCollectionView.ItemsSource = null; // força atualização visual
            TasksCollectionView.ItemsSource = tasks;
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var task = e.CurrentSelection.FirstOrDefault() as TaskItem;
            if (task == null) return;

            await Shell.Current.GoToAsync($"{nameof(TaskDetailsPage)}?taskId={task.Id}");
            ((CollectionView)sender).SelectedItem = null;
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddEditTaskPage), true);
        }
    }
}

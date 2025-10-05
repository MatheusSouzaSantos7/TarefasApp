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
            BindingContext = TasksService.Instance;
        }


        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var task = e.CurrentSelection.FirstOrDefault() as TaskItem;
            if (task == null) return;


            // Navega para a página de detalhes passando o Id como query parameter
            await Shell.Current.GoToAsync($"{nameof(TaskDetailsPage)}?taskId={task.Id}");


            // limpa seleção
            ((CollectionView)sender).SelectedItem = null;
        }


        private async void OnAddClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddEditTaskPage), true);
        }
    }
}
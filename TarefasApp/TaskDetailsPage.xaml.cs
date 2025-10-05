using System;
using Microsoft.Maui.Controls;
using TarefasApp.Models;
using TarefasApp.Services;


namespace TarefasApp
{
    [QueryProperty(nameof(TaskId), "taskId")]
    public partial class TaskDetailsPage : ContentPage
    {
        public string TaskId { set { LoadTask(value); } }

        public TaskDetailsPage()
        {
            InitializeComponent();
        }


        void LoadTask(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                var task = TasksService.Instance.GetById(guid);
                BindingContext = task;
            }
        }


        private async void OnEditClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Editar", "A edição será implementada na Etapa 3.", "OK");
        }


        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Confirmar", "Deseja excluir esta tarefa?", "Sim", "Não");
            if (!confirm) return;


            var task = BindingContext as TaskItem;
            if (task != null)
            {
                TasksService.Instance.Remove(task);
                // voltar para a página anterior
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
using System;
using Microsoft.Maui.Controls;
using TarefasApp.Models;
using TarefasApp.Services;

namespace TarefasApp
{
    public partial class AddEditTaskPage : ContentPage
    {
        public AddEditTaskPage()
        {
            InitializeComponent();
            DatePicker.Date = DateTime.Now;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleEntry.Text))
            {
                await DisplayAlert("Erro", "O título é obrigatório.", "OK");
                return;
            }

            var priority = Priority.Medium;

            switch (PriorityPicker.SelectedIndex)
            {
                case 0: priority = Priority.Low; break;
                case 1: priority = Priority.Medium; break;
                case 2: priority = Priority.High; break;
            }

            var newTask = new TaskItem
            {
                Title = TitleEntry.Text,
                Description = DescriptionEditor.Text,
                CreatedDate = DatePicker.Date,
                Priority = priority
            };

            TasksService.Instance.Add(newTask);

            await DisplayAlert("Sucesso", "Tarefa adicionada com sucesso!", "OK");
            await Shell.Current.GoToAsync(".."); // Fecha o modal
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(".."); // Fecha o modal sem salvar
        }
    }
}

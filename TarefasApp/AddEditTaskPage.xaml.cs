using System;
using Microsoft.Maui.Controls;
using TarefasApp.Models;
using TarefasApp.Services;

namespace TarefasApp
{
    [QueryProperty(nameof(TaskId), "taskId")]
    public partial class AddEditTaskPage : ContentPage
    {
        private bool _isEditMode = false;
        private TaskItem _taskToEdit;

        public string TaskId
        {
            set
            {
                if (Guid.TryParse(value, out var guid))
                {
                    _isEditMode = true;
                    _taskToEdit = TasksService.Instance.GetById(guid);
                    LoadTaskData();
                }
            }
        }

        public AddEditTaskPage()
        {
            InitializeComponent();
            DatePicker.Date = DateTime.Now;
        }

        private void LoadTaskData()
        {
            if (_taskToEdit != null)
            {
                TitleEntry.Text = _taskToEdit.Title;
                DescriptionEditor.Text = _taskToEdit.Description;
                DatePicker.Date = _taskToEdit.CreatedDate;

                switch (_taskToEdit.Priority)
                {
                    case Priority.Low: PriorityPicker.SelectedIndex = 0; break;
                    case Priority.Medium: PriorityPicker.SelectedIndex = 1; break;
                    case Priority.High: PriorityPicker.SelectedIndex = 2; break;
                }

                Title = "Editar Tarefa";
            }
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

            if (_isEditMode && _taskToEdit != null)
            {
                // Atualiza os dados existentes
                _taskToEdit.Title = TitleEntry.Text;
                _taskToEdit.Description = DescriptionEditor.Text;
                _taskToEdit.CreatedDate = DatePicker.Date;
                _taskToEdit.Priority = priority;

                await DisplayAlert("Sucesso", "Tarefa atualizada!", "OK");
            }
            else
            {
                // Cria nova tarefa
                var newTask = new TaskItem
                {
                    Title = TitleEntry.Text,
                    Description = DescriptionEditor.Text,
                    CreatedDate = DatePicker.Date,
                    Priority = priority
                };

                TasksService.Instance.Add(newTask);
                await DisplayAlert("Sucesso", "Tarefa adicionada!", "OK");
            }

            await Shell.Current.GoToAsync(".."); // Fecha o modal
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}

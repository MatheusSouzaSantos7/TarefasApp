using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Models;

namespace TarefasApp.Services
{
    internal class TasksService
    {
        private static TasksService _instance;
        public static TasksService Instance => _instance ??= new TasksService();

        public ObservableCollection<TaskItem> Tasks { get; } = new ObservableCollection<TaskItem>();

        private TasksService()
        {
            // seed de exemplo
            Tasks.Add(new TaskItem { Title = "Comprar leite", Description = "Leite, pão e café", Priority = Priority.Medium });
            Tasks.Add(new TaskItem { Title = "Enviar relatório", Description = "Relatório mensal para o gerente", Priority = Priority.High });
            Tasks.Add(new TaskItem { Title = "Treinar C#", Description = "Praticar MAUI por 30 minutos", Priority = Priority.Low });

        }
        public IEnumerable<TaskItem> GetAll() => Tasks;

        public TaskItem GetById(Guid id) => Tasks.FirstOrDefault(t => t.Id == id);
        public void Add(TaskItem item) => Tasks.Add(item);
        public void Remove(TaskItem item) => Tasks.Remove(item);
    }
}

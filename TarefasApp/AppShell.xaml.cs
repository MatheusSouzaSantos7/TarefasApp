namespace TarefasApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(TaskDetailsPage), typeof(TaskDetailsPage));
            Routing.RegisterRoute(nameof(AddEditTaskPage), typeof(AddEditTaskPage));

            // mais rotas (Add/Edit) virão nas próximas etapas
        }
    }
}

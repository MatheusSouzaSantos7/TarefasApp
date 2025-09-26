using TarefasApp.Models;

namespace TarefasApp;

public partial class MainPage : ContentPage
{
    private List<Tarefa> tarefas;

    public MainPage()
    {
        InitializeComponent();

        tarefas = new List<Tarefa>
        {
            new Tarefa { Titulo = "Comprar pão", Descricao = "Ir na padaria às 8h", DataCriacao = DateTime.Now, Prioridade = "Alta" },
            new Tarefa { Titulo = "Estudar MAUI", Descricao = "Focar em CollectionView", DataCriacao = DateTime.Now, Prioridade = "Média" }
        };

        TarefasListView.ItemsSource = tarefas;
    }

    private async void TarefasListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Tarefa tarefaSelecionada)
        {
            var paginaDetalhes = new PaginaDetalhes(tarefaSelecionada, RemoverTarefa);
            await Navigation.PushAsync(paginaDetalhes);

            // limpa seleção para não manter item marcado
            TarefasListView.SelectedItem = null;
        }
    }

    private void RemoverTarefa(Tarefa tarefa)
    {
        tarefas.Remove(tarefa);
        TarefasListView.ItemsSource = null;
        TarefasListView.ItemsSource = tarefas;
    }

    private async void OnAdicionarClicked(object sender, EventArgs e)
    {
        var novaTarefaPage = new NovaTarefaPage();
        novaTarefaPage.TarefaAdicionada += (tarefa) =>
        {
            tarefas.Add(tarefa);
            TarefasListView.ItemsSource = null;
            TarefasListView.ItemsSource = tarefas;
        };

        await Navigation.PushModalAsync(novaTarefaPage);
    }
}

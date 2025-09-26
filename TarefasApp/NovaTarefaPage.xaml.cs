using TarefasApp.Models;

namespace TarefasApp;

public partial class NovaTarefaPage : ContentPage
{
    public event Action<Tarefa> TarefaAdicionada;

    public NovaTarefaPage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TituloEntry.Text))
        {
            await DisplayAlert("Erro", "O título é obrigatório.", "OK");
            return;
        }

        var novaTarefa = new Tarefa
        {
            Titulo = TituloEntry.Text,
            Descricao = DescricaoEntry.Text,
            DataCriacao = DateTime.Now,
            Prioridade = PrioridadePicker.SelectedItem?.ToString() ?? "Média"
        };

        TarefaAdicionada?.Invoke(novaTarefa);

        await Navigation.PopModalAsync();
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}

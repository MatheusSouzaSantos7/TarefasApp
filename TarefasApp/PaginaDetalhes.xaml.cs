using TarefasApp.Models;

namespace TarefasApp;

public partial class PaginaDetalhes : ContentPage
{
    private Tarefa tarefa;

    private Action<Tarefa> removerCallback;

    public PaginaDetalhes(Tarefa tarefaSelecionada, Action<Tarefa> removerCallback)
    {
        InitializeComponent();
        tarefa = tarefaSelecionada;
        this.removerCallback = removerCallback;
        MostrarDetalhes();
    }


    private void MostrarDetalhes()
    {
        TituloLabel.Text = tarefa.Titulo;
        DescricaoLabel.Text = tarefa.Descricao;
        DataCriacaoLabel.Text = tarefa.DataCriacao.ToString("dd/MM/yyyy HH:mm");
        PrioridadeLabel.Text = tarefa.Prioridade;
    }

    private async void OnEditarClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new EditarTarefaPage(tarefa, AtualizarExibicao));
    }

    private async void OnExcluirClicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert("Confirmação", "Deseja realmente excluir esta tarefa?", "Sim", "Cancelar");

        if (confirmar)
        {
            removerCallback?.Invoke(tarefa);
            await Navigation.PopAsync(); // Voltar para a lista
        }
    }


    private void AtualizarExibicao()
    {
        MostrarDetalhes(); // Atualiza os dados na tela depois de editar
    }
}

using TarefasApp.Models;

namespace TarefasApp;

public partial class EditarTarefaPage : ContentPage
{
    private Tarefa tarefa;
    private Action atualizarCallback;

    public EditarTarefaPage(Tarefa tarefa, Action atualizarCallback)
    {
        InitializeComponent();
        this.tarefa = tarefa;
        this.atualizarCallback = atualizarCallback;
        PreencherCampos();
    }

    private void PreencherCampos()
    {
        TituloEntry.Text = tarefa.Titulo;
        DescricaoEditor.Text = tarefa.Descricao;
        DataCriacaoPicker.Date = tarefa.DataCriacao;
        PrioridadePicker.SelectedItem = tarefa.Prioridade;
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        tarefa.Titulo = TituloEntry.Text;
        tarefa.Descricao = DescricaoEditor.Text;
        tarefa.DataCriacao = DataCriacaoPicker.Date;
        tarefa.Prioridade = PrioridadePicker.SelectedItem?.ToString() ?? "Média";

        atualizarCallback?.Invoke();

        await Navigation.PopModalAsync();
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}

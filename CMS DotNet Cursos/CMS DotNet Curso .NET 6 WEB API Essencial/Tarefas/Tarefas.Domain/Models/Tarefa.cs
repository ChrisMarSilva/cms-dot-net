namespace Tarefas.Domain.Models;

public class Tarefa : BaseEntity
{
    public string Atividade { get; set; }
    public string Status { get; set; }

    public Tarefa() : base()
    {

    }

    public Tarefa(string atividade, string status) : this()
    {
        Atividade = atividade;
        Status = status;
    }

    public void Update(string atividade, string status)
    {
        base.Update();

        Atividade = atividade;
        Status = status;
    }
}

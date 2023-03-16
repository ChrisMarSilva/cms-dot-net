using Catalogo.Service.Interfaces;

namespace Catalogo.Service;

public class MeuServico : IMeuServico
{
    public string Saudacao(string nome) => $"Bem-Vindo, {nome} - {DateTime.Now}\n\n";
}

using Catalogo.Application.Interfaces;

namespace Catalogo.Application.Services;

public class MeuServico : IMeuServico
{
    public string Saudacao(string nome) => $"Bem-Vindo, {nome} - {DateTime.Now}\n\n";
}

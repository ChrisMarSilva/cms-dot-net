﻿using prmToolkit.NotificationPattern;
using XGame.Domain.Arguments.Base;
using XGame.Domain.Arguments.Jogador;
using XGame.Domain.Entities;
using XGame.Domain.Interfaces.Repositories;
using XGame.Domain.Interfaces.Services;
using XGame.Domain.ValueObjects;

namespace XGame.Domain.Services;

public class ServiceJogador : Notifiable, IServiceJogador
{
    private readonly IRepositoryJogador _repositoryJogador;

    public ServiceJogador() { }

    public ServiceJogador(IRepositoryJogador repositoryJogador)
    {
        _repositoryJogador = repositoryJogador;
    }

    public AdicionarJogadorResponse AdicionarJogador(AdicionarJogadorRequest request)
    {
        var nome = new Nome(request.PrimeiroNome, request.UltimoNome);
        var email = new Email(request.Email);

        var jogador = new Jogador(nome, email, request.Senha);

        AddNotifications(nome, email);

        if (_repositoryJogador.Existe(x => x.Email.Endereco == request.Email))
        {
            // AddNotification("E-mail", Message.JA_EXISTE_UM_X0_CHAMADO_X1.ToFormat("e-mail", request.Email));
        }

        if (this.IsInvalid())
            return null;

        jogador = _repositoryJogador.Adicionar(jogador);

        return new AdicionarJogadorResponse(){ Id = jogador.Id, Message = "" };
        //return (AdicionarJogadorResponse)jogador;
    }

    private void AddNotifications(Nome nome, Email email)
    {
        throw new NotImplementedException();
    }

    public AlterarJogadorResponse AlterarJogador(AlterarJogadorRequest request)
    {
        if (request == null)
        {
            // AddNotification("AlterarJogadorRequest", Message.X0_E_OBRIGATORIO.ToFormat("AlterarJogadorRequest"));
        }

        Jogador jogador = _repositoryJogador.ObterPorId(request.Id);

        if (jogador == null)
        {
            // AddNotification("Id", Message.DADOS_NAO_ENCONTRADOS);
            return null;
        }

        var nome = new Nome(request.PrimeiroNome, request.UltimoNome);
        var email = new Email(request.Email);

        jogador.AlterarJogador(nome, email, jogador.Status);

        AddNotifications(jogador);

        if (IsInvalid())
            return null;

        _repositoryJogador.Editar(jogador);

        return (AlterarJogadorResponse)jogador;
    }

    public AutenticarJogadorResponse AutenticarJogador(AutenticarJogadorRequest request)
    {
        if (request == null)
        {
           // AddNotification("AutenticarJogadorRequest", Message.X0_E_OBRIGATORIO.ToFormat("AutenticarJogadorRequest"));
        }

        var email = new Email(request.Email);
        var jogador = new Jogador(email, request.Senha);

        AddNotifications(jogador, email);

        if (jogador.IsInvalid())
            return null;

        jogador = _repositoryJogador
            .ObterPor(x => x.Email.Endereco == jogador.Email.Endereco && x.Senha == jogador.Senha);

        return new AutenticarJogadorResponse() { Id = jogador.Id, PrimeiroNome = jogador.Nome.PrimeiroNome, Email = jogador.Email.Endereco, Status = (int)jogador.Status };
    }

    public IEnumerable<JogadorResponse> ListarJogador()
    {
        return _repositoryJogador
            .Listar()
            .ToList()
            .Select(jogador => (JogadorResponse)jogador)
            .ToList();
    }

    public ResponseBase ExcluirJogador(Guid id)
    {
        Jogador jogador = _repositoryJogador.ObterPorId(id);

        if (jogador == null)
        {
            // AddNotification("Id", Message.DADOS_NAO_ENCONTRADOS);
            return null;
        }

        _repositoryJogador.Remover(jogador);

        return new ResponseBase();
    }
}

﻿using prmToolkit.NotificationPattern;

namespace XGame.Domain.ValueObjects;

public class Nome : Notifiable
{
    protected Nome() { }

    public Nome(string primeiroNome, string ultimoNome)
    {
        PrimeiroNome = primeiroNome;
        UltimoNome = ultimoNome;

        //new AddNotifications<Nome>(this)
        //        .IfNullOrInvalidLength(x => x.PrimeiroNome, 3, 50, Message.X0_OBRIGATORIO_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Primeiro nome", "3", "50"))
        //        .IfNullOrInvalidLength(x => x.UltimoNome, 3, 50, Message.X0_OBRIGATORIO_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Primeiro nome", "3", "50"));
    }

    public string PrimeiroNome { get; set; } = string.Empty;
    public string UltimoNome { get; set; } = string.Empty;
}

using System.ComponentModel;

namespace CMS.API.Enum
{
    public enum SituacaoEnum
    {
        [Description("Informação")]
        Informacao = 0,

        [Description("Recebida")]
        Recebida = 1,
    }

    public enum TipoMensagem
    {
        Desconhecido,
        TipoUm
    }
}
using System.ComponentModel;

namespace CMS_DotNet_Teste_Call_APIs.Dtos.Enums;

public enum TipoOwner
{
    [Description("Pessoa Física")]
    NATURAL_PERSON,
    [Description("Pessoa Jurídica")]
    LEGAL_PERSON
}
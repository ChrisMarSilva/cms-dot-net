using FileHelpers;
using FileHelpers.Events;

namespace CMS.EF.Performance.Console.CNAB240.Entity
{

    //[FixedLengthRecord()]
    [FixedLengthRecord(FixedMode.AllowMoreChars)]
    public class RegistroHeaderLote : INotifyRead
    {
        [FieldHidden]
        public string Linha;

        [FieldFixedLength(3)]
        [FieldTrim(TrimMode.Both)]
        public string CodigoBanco { get; set; }

        [FieldFixedLength(4)]
        [FieldTrim(TrimMode.Both)]
        public string LoteServico { get; set; }

        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Both)]
        public string TipoRegistro { get; set; }

        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Both)]
        public string TipoOperacao { get; set; }

        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Both)]
        public string TipoServico { get; set; }

        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Both)]
        public string FormaLancamento { get; set; }

        [FieldTrim(TrimMode.Both)]
        [FieldFixedLength(3)]
        public string NumeroVersaoLote { get; set; }

        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Both)]
        public string Filler1 { get; set; }

        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Both)]
        public string TipoInscricaoEmpresa { get; set; }

        [FieldFixedLength(14)]
        [FieldTrim(TrimMode.Both)]
        public string NumeroInscricaoEmpresa { get; set; }

        [FieldFixedLength(20)]
        [FieldTrim(TrimMode.Both)]
        public string CodigoConvenioBanco { get; set; }

        [FieldFixedLength(5)]
        [FieldTrim(TrimMode.Both)]
        public string AgenciaMantenedoraConta { get; set; }

        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Both)]
        public string DigitoVerificadorAgencia { get; set; }

        [FieldFixedLength(12)]
        [FieldTrim(TrimMode.Both)]
        public string NumeroContaCorrente { get; set; }

        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Both)]
        public string DigitoVerificadorConta { get; set; }

        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Both)]
        public string DigitoVerificadorAgenciaConta { get; set; }

        [FieldFixedLength(30)]
        [FieldTrim(TrimMode.Both)]
        public string NomeEmpresa { get; set; }

        [FieldFixedLength(40)]
        [FieldTrim(TrimMode.Both)]
        public string Informacao1Mensagem { get; set; }

        [FieldFixedLength(30)]
        [FieldTrim(TrimMode.Both)]
        public string Endereco { get; set; }

        [FieldFixedLength(5)]
        [FieldTrim(TrimMode.Both)]
        public string Numero { get; set; }

        [FieldFixedLength(15)]
        [FieldTrim(TrimMode.Both)]
        public string ComplementoEndereco { get; set; }

        [FieldFixedLength(20)]
        [FieldTrim(TrimMode.Both)]
        public string Cidade { get; set; }

        [FieldFixedLength(5)]
        [FieldTrim(TrimMode.Both)]
        public string CEP { get; set; }

        [FieldFixedLength(3)]
        [FieldTrim(TrimMode.Both)]
        public string ComplementoCEP { get; set; }

        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Both)]
        public string UF { get; set; }

        [FieldFixedLength(8)]
        [FieldTrim(TrimMode.Both)]
        public string Filler2 { get; set; }

        [FieldFixedLength(10)]
        [FieldTrim(TrimMode.Both)]
        public string OcorrenciasRetorno { get; set; }

        public void BeforeRead(BeforeReadEventArgs e)
        {
            this.Linha = e.RecordLine;
            //e.SkipThisRecord = true;
        }

        public void AfterRead(AfterReadEventArgs e)
        {
        }

    }
}

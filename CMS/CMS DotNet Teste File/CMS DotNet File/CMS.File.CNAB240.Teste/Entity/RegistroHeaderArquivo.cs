using FileHelpers;
using FileHelpers.Events;

namespace CMS.EF.Performance.Console.CNAB240.Entity
{

    //[FixedLengthRecord()]
    [FixedLengthRecord(FixedMode.AllowMoreChars)]
    public class RegistroHeaderArquivo : INotifyRead
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

        [FieldFixedLength(9)]
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
        [FieldAlign(AlignMode.Left, ' ')]
        public string NomeEmpresa { get; set; }

        [FieldFixedLength(30)]
        [FieldTrim(TrimMode.Both)]
        public string NomeBanco { get; set; }

        [FieldFixedLength(10)]
        [FieldTrim(TrimMode.Both)]
        public string Filler2 { get; set; }

        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Both)]
        public string CodigoRemessaRetorno { get; set; }

        [FieldFixedLength(8)]
        [FieldTrim(TrimMode.Both)]
        public string DataGeracaoArquivo { get; set; }

        [FieldFixedLength(6)]
        [FieldTrim(TrimMode.Both)]
        public string HoraGeracaoArquivo { get; set; }

        [FieldFixedLength(6)]
        [FieldTrim(TrimMode.Both)]
        public string NumeroSequencialArquivo { get; set; }

        [FieldFixedLength(3)]
        [FieldTrim(TrimMode.Both)]
        public string NumeroVersaoLayout { get; set; }

        [FieldFixedLength(5)]
        [FieldTrim(TrimMode.Both)]
        public string DensidadeGravacaoArquivo { get; set; }

        [FieldFixedLength(20)]
        [FieldTrim(TrimMode.Both)]
        public string UsoReservadoBanco { get; set; }

        [FieldFixedLength(20)]
        [FieldTrim(TrimMode.Both)]
        public string UsoReservadoEmpresa { get; set; }

        [FieldFixedLength(19)]
        [FieldTrim(TrimMode.Both)]
        public string Filler3 { get; set; }

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

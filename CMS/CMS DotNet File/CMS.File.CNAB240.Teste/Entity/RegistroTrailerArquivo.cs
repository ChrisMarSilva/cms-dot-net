using FileHelpers;
using FileHelpers.Events;

namespace CMS.EF.Performance.Console.CNAB240.Entity
{

    //[FixedLengthRecord()]
    [FixedLengthRecord(FixedMode.AllowMoreChars)]
    public class RegistroTrailerArquivo : INotifyRead
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

        [FieldFixedLength(6)]
        [FieldTrim(TrimMode.Both)]
        public string QuantidadeLotesArquisvo { get; set; }

        [FieldFixedLength(6)]
        [FieldTrim(TrimMode.Both)]
        public string QuantidadeRegistrosArquivo { get; set; }

        [FieldFixedLength(211)]
        [FieldTrim(TrimMode.Both)]
        public string Filler2 { get; set; }
        
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

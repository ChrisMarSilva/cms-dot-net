using FileHelpers;
using FileHelpers.Events;
using System;

namespace CMS.EF.Performance.Console.CNAB240.Entity
{

    //[FixedLengthRecord()]
    [FixedLengthRecord(FixedMode.AllowMoreChars)]
    public class RegistroDetalheSegmentoB : INotifyRead
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

        [FieldFixedLength(5)]
        [FieldTrim(TrimMode.Both)]
        public string NumeroSequencialRegistroLote { get; set; }

        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Both)]
        public string CodigoSegmentoRegistroDetalhe { get; set; }

        [FieldFixedLength(3)]
        [FieldTrim(TrimMode.Both)]
        public string Filler1 { get; set; }

        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Both)]
        public string TipoInscricaoFavorecido { get; set; }

        [FieldFixedLength(14)]
        [FieldTrim(TrimMode.Both)]
        public string CNPJCPFFavorecido { get; set; }

        [FieldFixedLength(30)]
        [FieldTrim(TrimMode.Both)]
        public string LogradouroFavorecido { get; set; }

        [FieldFixedLength(5)]
        [FieldTrim(TrimMode.Both)]
        public string NúmeroLocalFavorecido { get; set; }

        [FieldFixedLength(15)]
        [FieldTrim(TrimMode.Both)]
        public string ComplementoLocalFavorecido { get; set; }

        [FieldFixedLength(15)]
        [FieldTrim(TrimMode.Both)]
        public string BairroFavorecido { get; set; }

        [FieldFixedLength(20)]
        [FieldTrim(TrimMode.Both)]
        public string CidadeFavorecido { get; set; }

        [FieldFixedLength(8)]
        [FieldTrim(TrimMode.Both)]
        public string CEPFavorecido { get; set; }

        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Both)]
        public string EstadoFavorecido { get; set; }

        [FieldFixedLength(8)]
        [FieldTrim(TrimMode.Both)]
        public string DataVencimento { get; set; }

        [FieldFixedLength(15)]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(TwoDecimalConverter))]
        [FieldNullValue(typeof(decimal), "0")]
        public decimal ValorDocumento { get; set; }

        [FieldFixedLength(15)]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(TwoDecimalConverter))]
        [FieldNullValue(typeof(decimal), "0")]
        public decimal ValorAbatimento { get; set; } = 0;

        [FieldFixedLength(15)]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(TwoDecimalConverter))]
        [FieldNullValue(typeof(decimal), "0")]
        public decimal ValorDesconto { get; set; }

        [FieldFixedLength(15)]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(TwoDecimalConverter))]
        [FieldNullValue(typeof(decimal), "0")]
        public decimal ValorMora { get; set; }

        [FieldFixedLength(15)]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(TwoDecimalConverter))]
        [FieldNullValue(typeof(decimal), "0")]
        public decimal ValorMulta { get; set; }
        
        [FieldFixedLength(4)]
        [FieldTrim(TrimMode.Both)]
        public string HorarioEnvioTED { get; set; }

        [FieldFixedLength(11)]
        [FieldTrim(TrimMode.Both)]
        public string Filler2 { get; set; }

        [FieldFixedLength(4)]
        [FieldTrim(TrimMode.Both)]
        public string CodigoHistoricoCredito { get; set; }

        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Both)]
        public string EmissaoAvisoFavorecido { get; set; }

        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Both)]
        public string Filler3 { get; set; }

        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Both)]
        public string TEDInstituiçãoFinanceira { get; set; }

        [FieldFixedLength(8)]
        [FieldTrim(TrimMode.Both)]
        public string IdentificaçãoIFSPB { get; set; }

        public void BeforeRead(BeforeReadEventArgs e)
        {
            this.Linha = e.RecordLine;
            //e.SkipThisRecord = true;
        }

        public void AfterRead(AfterReadEventArgs e)
        {
        }

        internal class TwoDecimalConverter : ConverterBase
        {
            public override object StringToField(string from)
            {
                decimal res = Convert.ToDecimal(from);
                return res / 100;
            }

            public override string FieldToString(object from)
            {
                decimal d = (decimal)from;
                return Math.Round(d * 100).ToString();
            }
        }

    }
}

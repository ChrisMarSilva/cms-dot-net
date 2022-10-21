using FileHelpers;
using FileHelpers.Events;
using System;

namespace CMS.EF.Performance.Console.CNAB240.Entity
{

    //[FixedLengthRecord()]
    [FixedLengthRecord(FixedMode.AllowMoreChars)]
    public class RegistroTrailerLote : INotifyRead
    {

        [FieldHidden]
        public string Linha;

        [FieldFixedLength(3)]
        public string CodigoBanco { get; set; }

        [FieldFixedLength(4)]
        public string LoteServico { get; set; }

        [FieldFixedLength(1)]
        public string TipoRegistro { get; set; }

        [FieldFixedLength(9)]
        public string Filler1 { get; set; }

        [FieldFixedLength(6)]
        public string QuantidadeRegistrosLote { get; set; }

        [FieldFixedLength(18)]
        [FieldConverter(typeof(TwoDecimalConverter))]
        //[FieldConverter(ConverterKind.Decimal, ".")] "6|41.34"
        [FieldNullValue(typeof(decimal), "0")]

        public decimal SomatoriaValores { get; set; }

        [FieldFixedLength(18)]
        [FieldConverter(typeof(TwoDecimalConverter))]
        [FieldNullValue(typeof(decimal), "0")]
        public decimal SomatoriaQuantidadeMoedas { get; set; }
        
        [FieldFixedLength(6)]
        public string NumeroAvisoDebito { get; set; }

        [FieldFixedLength(165)]
        public string Filler2 { get; set; }

        [FieldFixedLength(10)]
        public string OcorrenciasRetorno { get; set; }

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

        public void BeforeRead(BeforeReadEventArgs e)
        {
            this.Linha = e.RecordLine;
            //e.SkipThisRecord = true;
        }

        public void AfterRead(AfterReadEventArgs e)
        {
        }

    }

    //[FieldConverter(typeof(MoneyConverter))]

    public class MoneyConverter : ConverterBase
    {
        public override object StringToField(string from)
        {
            return Convert.ToDecimal(Decimal.Parse(from) / 100);
        }

        public override string FieldToString(object fieldValue)
        {
            return ((decimal)fieldValue).ToString("#.##").Replace(".", "");
        }

    }
}

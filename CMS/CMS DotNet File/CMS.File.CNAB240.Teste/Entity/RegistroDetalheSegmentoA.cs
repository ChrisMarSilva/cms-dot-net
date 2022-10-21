using FileHelpers;
using FileHelpers.Events;
using System;

namespace CMS.EF.Performance.Console.CNAB240.Entity
{

    //[FixedLengthRecord()]
    [FixedLengthRecord(FixedMode.AllowMoreChars)]
    public class RegistroDetalheSegmentoA : INotifyRead
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

        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Both)]
        public string TipoMovimento { get; set; }

        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Both)]
        public string CodigoInstrucaoMovimento { get; set; }

        [FieldFixedLength(3)]
        [FieldTrim(TrimMode.Both)]
        public string CodigoCamaraCompensacao { get; set; }

        [FieldFixedLength(3)]
        [FieldTrim(TrimMode.Both)]
        public string CodigoBancoFavorecido { get; set; }

        [FieldFixedLength(5)]
        [FieldTrim(TrimMode.Both)]
        public string CodigoAgenciaFavorecido { get; set; }

        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Both)]
        public string DigitoVerificadorAgencia { get; set; }

        [FieldFixedLength(12)]
        [FieldTrim(TrimMode.Both)]
        public string ContaCorrenteFavorecido { get; set; }

        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Both)]
        public string DigitoVerificadorConta { get; set; }

        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Both)]
        public string DigitoVerificadorAgenciaConta { get; set; }

        [FieldFixedLength(30)]
        [FieldTrim(TrimMode.Both)]
        public string NomeFavorecido { get; set; }

        [FieldFixedLength(20)]
        [FieldTrim(TrimMode.Both)]
        public string NroDocumentoCliente { get; set; }

        [FieldFixedLength(8)]
        [FieldTrim(TrimMode.Both)]
        public string DataPagamento { get; set; }

        [FieldFixedLength(3)]
        [FieldTrim(TrimMode.Both)]
        public string TipoMoeda { get; set; }

        [FieldFixedLength(15)]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(TwoDecimalConverter))]
        [FieldNullValue(typeof(decimal), "0")]
        public decimal QuantidadeMoeda { get; set; }

        [FieldFixedLength(15)]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(TwoDecimalConverter))]
        [FieldNullValue(typeof(decimal), "0")]
        public decimal ValorPagamento { get; set; }

        [FieldFixedLength(20)]
        [FieldTrim(TrimMode.Both)]
        public string NroDocumentoBanco { get; set; }

        [FieldFixedLength(8)]
        [FieldTrim(TrimMode.Both)]
        public string DataRealPagamentoRetorno { get; set; }

        [FieldFixedLength(15)]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(TwoDecimalConverter))]
        [FieldNullValue(typeof(decimal), "0")]
        public decimal ValorRealPagamento { get; set; }

        [FieldFixedLength(40)]
        [FieldTrim(TrimMode.Both)]
        public string Informacao2Mensagem { get; set; }

        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Both)]
        public string FinalidadeDOC { get; set; }

        [FieldFixedLength(5)]
        [FieldTrim(TrimMode.Both)]
        public string FinalidadeTED { get; set; }

        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Both)]
        public string CodigoFinalidadeComplementar { get; set; }

        [FieldFixedLength(3)]
        [FieldTrim(TrimMode.Both)]
        public string Filler1 { get; set; }

        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Both)]
        public string EmissaoAvisoFavorecido { get; set; }

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

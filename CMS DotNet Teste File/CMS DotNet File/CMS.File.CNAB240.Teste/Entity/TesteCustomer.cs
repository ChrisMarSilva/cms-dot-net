﻿using FileHelpers;
using System;

namespace CMS.EF.Performance.Console.CNAB240.Entity
{
    [FixedLengthRecord()]
    public class TesteCustomer
    {
		[FieldFixedLength(5)]
		[FieldTrim(TrimMode.Both)]
		public int CustId;

		[FieldFixedLength(20)]
		[FieldTrim(TrimMode.Right)]
		public string Name;

		[FieldFixedLength(8)]
		[FieldConverter(typeof(TwoDecimalConverter))]
		public decimal Balance;

		[FieldFixedLength(8)]
		[FieldConverter(ConverterKind.Date, "ddMMyyyy")]
		public DateTime AddedDate;

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

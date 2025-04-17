using CsvHelper.Configuration;

namespace CMS_Web_ByBit_Misc_Console;

public sealed class RegistroMap : ClassMap<Registro>
{
    public RegistroMap()
    {
        Map(m => m.Uid).Name("Uid");
        Map(m => m.Currency).Name("Currency");
        Map(m => m.Contract).Name("Contract");
        Map(m => m.Type).Name("Type");
        Map(m => m.Direction).Name("Direction");
        Map(m => m.Quantity).Name("Quantity");
        Map(m => m.Position).Name("Position");
        Map(m => m.FilledPrice).Name("Filled Price");
        Map(m => m.Funding).Name("Funding");
        Map(m => m.FeePaid).Name("Fee Paid");
        Map(m => m.CashFlow).Name("Cash Flow");
        Map(m => m.Change).Name("Change");
        Map(m => m.WalletBalance).Name("Wallet Balance");
        Map(m => m.Action).Name("Action");
        Map(m => m.TimeUTC).Name("Time(UTC)");

        //Map(m => m.Uid).Index(0);
        //Map(m => m.Currency).Index(1);
        //Map(m => m.Contract).Index(2);
        //Map(m => m.Type).Index(3);
        //Map(m => m.Direction).Index(4);
        //Map(m => m.Quantity).Index(5).Convert(row => Math.Abs(row.Row.GetField<decimal>(5)));
        //Map(m => m.Position).Index(6);
        //Map(m => m.FilledPrice).Index(7);
        //Map(m => m.Funding).Index(8);
        //Map(m => m.FeePaid).Index(9).Convert(row => Math.Abs(row.Row.GetField<decimal>(9)));
        //Map(m => m.CashFlow).Index(10).Convert(row => Math.Abs(row.Row.GetField<decimal>(10)));
        //Map(m => m.Change).Index(11);
        //Map(m => m.WalletBalance).Index(12);
        //Map(m => m.Action).Index(13);
        //Map(m => m.TimeUTC).Index(14).TypeConverterOption.Format("yyyy-MM-dd HH:mm:ss");
    }
}

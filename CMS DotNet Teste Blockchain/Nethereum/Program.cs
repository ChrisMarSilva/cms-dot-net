using System;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Web3.Accounts.Managed;
using Nethereum.Hex.HexTypes;

// https://github.com/Nethereum/Testchains

Console.WriteLine("INI"); 
try
{
    // Infura 

    //GetAccountBalance().Wait();
    //SendingTransactio().Wait();
    ConvertingCryptoCurrency().Wait();



}
catch (Exception ex)
{
    Console.WriteLine($"ERRO: {ex.Message}");
}
finally
{
    Console.WriteLine("FIM");
    Console.ReadKey();
}

async Task GetAccountBalance()
{
    var projectId = "ddea04e9a4e14dbc9dc53d4178b0c9b0"; // 7238211010344719ad14a89db874158c
    var url = $"https://mainnet.infura.io/v3/{projectId}";
    var web3 = new Web3(url);

    var address = "0xde0b295669a9fd93d5f28d9ec85e40f4cb697bae";
    var balance = await web3.Eth.GetBalance.SendRequestAsync(address);
    Console.WriteLine($"Balance in Wei: {balance.Value}"); // 318273306589375457545410

    var etherAmount = Web3.Convert.FromWei(balance.Value);
    Console.WriteLine($"Balance in Ether: {etherAmount}"); // 318273,30658937545754541
}

async Task SendingTransactio()
{
    var privateKey = "0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7";
    var account = new Account(privateKey);

    var web3 = new Web3(account);
    var toAddress = "0x13f022d72158410433cbd66f5dd8bf6d2d129924";

    var transaction = await web3.Eth
        .GetEtherTransferService()
        .TransferEtherAndWaitForReceiptAsync(toAddress, 1.11m);
    //  .TransferEtherAndWaitForReceiptAsync(toAddress, 1.11m, 2);
    //  .TransferEtherAndWaitForReceiptAsync(toAddress, 1.11m, 2, new BigInteger(25000));

    // transferência de Ether usando uma carteira HD 
    //string Words = "ripple scissors kick mammal hire column oak again sun offer wealth tomorrow wagon turn fatal";
    //string Password = "password";
    //var wallet = new Wallet(Words, Password);
    //var account = wallet.GetAccount(0);
    //var toAddress = "0x13f022d72158410433cbd66f5dd8bf6d2d129924";
    //var web3 = new Web3(account);
    //var transaction = await web3.Eth
    //    .GetEtherTransferService()
    //    .TransferEtherAndWaitForReceiptAsync(toAddress, 1.11m, 2);

}

async Task ConvertingCryptoCurrency()
{
    var web3 = new Web3();

    var address = "0x12890d2cce102216644c59daE5baed380d84830c";
    var balance = await web3.Eth.GetBalance.SendRequestAsync(address);

    var balanceInWei = balance.Value;
    var balanceInEther = Web3.Convert.FromWei(balance.Value);

    var BackToWei = Web3.Convert.ToWei(balanceInEther);
    Console.WriteLine($"Balance in Ether: {BackToWei}");
}

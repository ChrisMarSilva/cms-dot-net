using ConsoleApp1;

try
{

    var sChave = "os6rFLqLnGaUlwYEetw8zvcHQ9A=";

    //sChave = JdCripto.DescriptoAes256("JDNPC_WS_CHAVECRIPTO", sChave);
    // sChave = await JdCripto.DecryptAsync("JDNPC_WS_CHAVECRIPTO", Convert.FromBase64String(sChave));
    sChave = "JDNPC_WS_CHAVECRIPTO";
    Console.WriteLine($"sChave: {sChave}");

    //var sSenhaCripto = JdCripto.CriptoAes256("JDNPC_WS_CHAVECRIPTO", "WS");
    // Console.WriteLine($"sCripto256_Chave: {sSenhaCripto}");
    var sSenhaCripto = await JdCripto.EncryptAsync(sChave, "WS");
    Console.WriteLine($"sCripto256_Chave: {BitConverter.ToString(sSenhaCripto)}");
    Console.WriteLine($"sCripto256_Chave: {Convert.ToBase64String(sSenhaCripto)}");




    //var hashSenhaBD = "DBC9C4A7E48F437DE8301A70954970C36BF78453F91359F25C18027628FA2DDA";
    //var hashSenhaInformada = "WS";
    // var sCripto256_Chave = "os6rFLqLnGaUlwYEetw8zvcHQ9A=";

    //var teste1 = JdCripto.Hash256(hashSenhaInformada);
    //Console.WriteLine($"teste1: {teste1}");


    //var aes = new Aes256("JDNPC_WS_CHAVECRIPTO");
    //var sCripto256_Chave = aes.Decrypt("os6rFLqLnGaUlwYEetw8zvcHQ9A=");
    //Console.WriteLine($"sCripto256_Chave: {sCripto256_Chave}");





    //const string chave = "Sup3rS3curePass!";
    //Console.WriteLine($"1-passphrase: {chave}");

    //var textClear = "We use encryption to obscure a piece of information.";
    //Console.WriteLine($"2-clearText: {textClear}");

    //Console.WriteLine($"");
    //var textEncrypted = await JdCripto.EncryptAsync(chave, textClear);
    //var textEncryptedTexto = BitConverter.ToString(textEncrypted);
    //Console.WriteLine($"3-encrypted: x{textEncryptedTexto}");
    //Console.WriteLine($"");

    //var textDecrypted = await JdCripto.DecryptAsync(chave, textEncrypted);
    //Console.WriteLine($"4-decrypted: {textDecrypted}");
    //Console.WriteLine($"5-Result: {textClear.Equals(textDecrypted, StringComparison.OrdinalIgnoreCase)}");

    ////var textDecrypted2 = JdCripto.DescriptoAes256(chave, textEncryptedTexto);
    //var textDecrypted2 = JdCripto.DescriptoAes256(chave, Convert.ToBase64String(textEncrypted));
    //Console.WriteLine($"6-decrypted: {textDecrypted2}");
    //Console.WriteLine($"7-Result: {textClear.Equals(textDecrypted2, StringComparison.OrdinalIgnoreCase)}");

    //var parametroChave = "JDNPC_WS_CHAVECRIPTO";
    //Console.WriteLine($"parametro: {parametroChave}");

    //var chave = "BibOL+E01+K2w4VMiHcYJJ3CPzhEJD/PhwkbY4+b0uE=";
    //Console.WriteLine($"chave-1: {chave}");

    //chave = JdCripto.DescriptoAes256(parametroChave, chave);
    //Console.WriteLine($"chave2-: {chave}");

    // Specified key is not a valid size for this algorithm.
    // The input is not a valid Base-64 string as it contains a non-base 64 character, more than two padding characters, or an illegal character among the padding characters.




    // var legadoSenha = "WS";
    //var legadoSenha = JdCripto.DescriptoAes256(chave, legadoSenha);
    //Console.WriteLine($"senha: {senhaBD}");

    //var clientSecret = "WS";
    //var hashSenhaInformada = JdCripto.Hash256(clientSecret!);
    //Console.WriteLine($"hashSenhaInformada: {hashSenhaInformada}");

    //if (!legadoSenha.Equals(hashSenhaInformada, StringComparison.Ordinal))
    //{
    //    Console.WriteLine($"Erro: Senhas Divergentes");
    //    return;
    //}

}
catch (Exception e)
{
    Console.WriteLine($"Erro: {e.Message}");
}
finally
{ 
    Console.ReadKey();
}
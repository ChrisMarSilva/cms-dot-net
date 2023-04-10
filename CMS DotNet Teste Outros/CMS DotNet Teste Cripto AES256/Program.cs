using ConsoleApp1;
using System;

try
{

    const string chave = "Sup3rS3curePass!";
    Console.WriteLine($"1-passphrase: {chave}");

    var textClear = "We use encryption to obscure a piece of information.";
    Console.WriteLine($"2-clearText: {textClear}");

    Console.WriteLine($"");
    var textEncrypted = await JdCripto.EncryptAsync(chave, textClear);
    var textEncryptedTexto = BitConverter.ToString(textEncrypted);
    Console.WriteLine($"3-encrypted: x{textEncryptedTexto}");
    Console.WriteLine($"");

    var textDecrypted = await JdCripto.DecryptAsync(chave, textEncrypted);
    Console.WriteLine($"4-decrypted: {textDecrypted}");
    Console.WriteLine($"5-Result: {textClear.Equals(textDecrypted, StringComparison.OrdinalIgnoreCase)}");

    //var textDecrypted2 = JdCripto.DescriptoAes256(chave, textEncryptedTexto);
    var textDecrypted2 = JdCripto.DescriptoAes256(chave, Convert.ToBase64String(textEncrypted));
    Console.WriteLine($"6-decrypted: {textDecrypted2}");
    Console.WriteLine($"7-Result: {textClear.Equals(textDecrypted2, StringComparison.OrdinalIgnoreCase)}");
    
    

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
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1;

public static class JdCripto
{
    private const string CriptAlfb1 = "CTBGLPJFNQMDKHSR";
    private const string CriptAlfb2 = "PCJGQMNBRTHKFSLD";
    private const string QuebraRand = "QHCJGLNBMTRPFSDK";

    private static readonly Random _randomico = new();

    private static string ValorLetra(char letra)
    {
        var quebraAlfabeto = _randomico.Next(16);
        var posQuebra = 0;

        var alfabeto1 = CriptAlfb1 + CriptAlfb1;
        if (quebraAlfabeto > 0)
            posQuebra = alfabeto1.IndexOf(QuebraRand[quebraAlfabeto - 1]);
        alfabeto1 = alfabeto1.Substring(posQuebra, 16);

        var alfabeto2 = CriptAlfb2 + CriptAlfb2;
        if (quebraAlfabeto > 0)
            posQuebra = alfabeto2.IndexOf(QuebraRand[quebraAlfabeto - 1]);
        alfabeto2 = alfabeto2.Substring(posQuebra, 16);

        var valorLetra = (int)letra;
        var segCasa = valorLetra / 16;
        var primCasa = valorLetra % 16;

        char[] letraCripto = { QuebraRand[quebraAlfabeto], alfabeto2[segCasa], alfabeto1[primCasa] };

        return new string(letraCripto);
    }

    private static string ValorCripto(string grupoletra)
    {
        var quebraAlfabeto = QuebraRand.IndexOf(grupoletra[0]);
        var posQuebra = 0;

        var alfabeto1 = CriptAlfb1 + CriptAlfb1;
        if (quebraAlfabeto > 0)
            posQuebra = alfabeto1.IndexOf(QuebraRand[quebraAlfabeto - 1]);
        alfabeto1 = alfabeto1.Substring(posQuebra, 16);

        var alfabeto2 = CriptAlfb2 + CriptAlfb2;
        if (quebraAlfabeto > 0)
            posQuebra = alfabeto2.IndexOf(QuebraRand[quebraAlfabeto - 1]);
        alfabeto2 = alfabeto2.Substring(posQuebra, 16);

        var valorLetra =
            alfabeto2.IndexOf(grupoletra[1]) * 16 +
            alfabeto1.IndexOf(grupoletra[2]);

        return ((char)valorLetra).ToString();
    }

    public static string Cripto(string chave, string texto)
    {
        var textoAberto = texto + chave;

        if (textoAberto.Length <= 0)
            return "";

        var textoCripto = textoAberto.Aggregate("", (current, letra) => current + ValorLetra(letra));

        textoCripto = textoCripto.Replace("QQ", "V");
        textoCripto = textoCripto.Replace("HH", "W");
        textoCripto = textoCripto.Replace("BB", "X");
        textoCripto = textoCripto.Replace("CC", "Y");
        textoCripto = textoCripto.Replace("DD", "Z");
        textoCripto = textoCripto.Replace("FF", "A");
        textoCripto = textoCripto.Replace("LL", "E");
        textoCripto = textoCripto.Replace("GG", "I");
        textoCripto = textoCripto.Replace("PP", "O");
        textoCripto = textoCripto.Replace("KK", "U");
        textoCripto = textoCripto.Replace("CP", "0");
        textoCripto = textoCripto.Replace("HR", "1");
        textoCripto = textoCripto.Replace("PL", "2");
        textoCripto = textoCripto.Replace("NM", "3");
        textoCripto = textoCripto.Replace("DC", "4");
        textoCripto = textoCripto.Replace("QB", "5");
        textoCripto = textoCripto.Replace("KM", "6");
        textoCripto = textoCripto.Replace("SJ", "7");
        textoCripto = textoCripto.Replace("JB", "8");
        textoCripto = textoCripto.Replace("GR", "9");

        if (chave != "")
        {
            var textoResultado = "";

            for (var iPosTexto = 0; iPosTexto < textoCripto.Length / 2; iPosTexto++)
            {
                textoResultado +=
                    textoCripto[iPosTexto] +
                    textoCripto[textoCripto.Length - iPosTexto - 1].ToString();
            }

            if (textoCripto.Length % 2 > 0)
            {
                textoResultado +=
                    textoCripto[textoCripto.Length / 2];
            }

            return textoResultado;
        };

        return textoCripto;
    }

    public static string Descripto(string chave, string texto, bool geraException = true)
    {
        if (texto.Length <= 0)
            return "";

        try
        {
            var textoTrocado = texto;

            if (chave != "")
            {
                textoTrocado = "";

                if (texto.Length % 2 > 0)
                    textoTrocado = texto[^1].ToString();

                for (var iPosTexto = texto.Length / 2; iPosTexto > 0; iPosTexto--)
                {
                    textoTrocado =
                        texto[iPosTexto * 2 - 2] +
                        textoTrocado +
                        texto[iPosTexto * 2 - 1];
                }
            }

            textoTrocado = textoTrocado.Replace("9", "GR");
            textoTrocado = textoTrocado.Replace("8", "JB");
            textoTrocado = textoTrocado.Replace("7", "SJ");
            textoTrocado = textoTrocado.Replace("6", "KM");
            textoTrocado = textoTrocado.Replace("5", "QB");
            textoTrocado = textoTrocado.Replace("4", "DC");
            textoTrocado = textoTrocado.Replace("3", "NM");
            textoTrocado = textoTrocado.Replace("2", "PL");
            textoTrocado = textoTrocado.Replace("1", "HR");
            textoTrocado = textoTrocado.Replace("0", "CP");
            textoTrocado = textoTrocado.Replace("U", "KK");
            textoTrocado = textoTrocado.Replace("O", "PP");
            textoTrocado = textoTrocado.Replace("I", "GG");
            textoTrocado = textoTrocado.Replace("E", "LL");
            textoTrocado = textoTrocado.Replace("A", "FF");
            textoTrocado = textoTrocado.Replace("Z", "DD");
            textoTrocado = textoTrocado.Replace("Y", "CC");
            textoTrocado = textoTrocado.Replace("X", "BB");
            textoTrocado = textoTrocado.Replace("W", "HH");
            textoTrocado = textoTrocado.Replace("V", "QQ");

            if (textoTrocado.Length % 3 != 0)
                throw new Exception("Falha na descriptografia");

            var textoDescripto = "";

            // Troca Letras
            for (var grupoLetra = 1; grupoLetra <= textoTrocado.Length / 3; grupoLetra++)
            {
                textoDescripto += ValorCripto(textoTrocado.Substring(grupoLetra * 3 - 3, 3));
            }

            if (chave == "") return textoDescripto;

            if (!textoDescripto.Substring(textoDescripto.Length - chave.Length, chave.Length).Equals(chave))
            {
                throw new Exception("Chave inválida");
            }

            return textoDescripto[..^chave.Length];
        }
        catch
        {
            if (geraException)
                throw;

            return "";
        };
    }

    private static byte[] IV = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
    // private static byte[] IV = new byte[16];

    private static byte[] DeriveKeyFromPassword(string password)
    {
        var emptySalt = Array.Empty<byte>();
        var iterations = 1000;
        var desiredKeyLength = 16; // 16 bytes equal 128 bits.
        var hashMethod = HashAlgorithmName.SHA384;
        return Rfc2898DeriveBytes.Pbkdf2(Encoding.Unicode.GetBytes(password), emptySalt, iterations, hashMethod, desiredKeyLength);
    }

    private static byte[] ConvertPasswordToByte(string password)
    {
        byte[] key = new byte[32];

        for (int i = 0; i < password.Length; i++)
        {
            key[i] = Convert.ToByte(password[i]);
        }

        return key;
    }

    public static string DescriptoAes256(string chave, string texto)
    {
        byte[] iv = new byte[16];
        //byte[] buffer = Encoding.UTF8.GetBytes(texto);
        byte[] buffer = Convert.FromBase64String(texto);
        //byte[] buffer = DeriveKeyFromPassword(texto);
        // texto = Convert.ToBase64String(buffer);

        using var aes = Aes.Create();

        //aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7; // None // Zeros // PKCS7;
        //aes.BlockSize = 128;
        //aes.KeySize = 128;

        // aes.Key = Convert.FromBase64String(chave);
        // aes.Key =  Encoding.UTF8.GetBytes(chave);
        // aes.Key = Convert.FromBase64String(Hash256(chave));
        aes.Key = DeriveKeyFromPassword(chave);
        //aes.Key = Hash256InBytes(chave);

        //aes.IV = null;
        //aes.IV = iv; 
        aes.IV = IV;

        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream(buffer);
        using var cs = new CryptoStream((Stream)ms, decryptor, CryptoStreamMode.Read);

        //using var sr = new StreamReader(cs);
        //return sr.ReadToEnd();

        using MemoryStream sr = new();
        cs.CopyTo(sr);
        return Encoding.Unicode.GetString(sr.ToArray()); // Unicode // UTF8 // ASCII

        // aes.GenerateKey(); // Console.WriteLine($"Aes Key : {Convert.ToBase64String(aes.Key)}");
        // aes.GenerateIV(); // Console.WriteLine($"Aes IV : {Convert.ToBase64String(aes.IV)}");

        //using var aes = Aes.Create();
        //aes.Key = Encoding.UTF8.GetBytes(chave);
        //using var decryptor = aes.CreateDecryptor(aes.Key, null);
        //using var msDecrypt = new MemoryStream(Encoding.UTF8.GetBytes(texto));
        //using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        //using var srDecrypt = new StreamReader(csDecrypt);
        //return srDecrypt.ReadToEnd();
    }

    public static async Task<byte[]> EncryptAsync(string chave, string texto)
    {
        using Aes aes = Aes.Create();
        aes.Key = DeriveKeyFromPassword(chave);
        aes.IV = IV;
        using MemoryStream output = new();
        using CryptoStream cryptoStream = new(output, aes.CreateEncryptor(), CryptoStreamMode.Write);
        await cryptoStream.WriteAsync(Encoding.Unicode.GetBytes(texto));
        await cryptoStream.FlushFinalBlockAsync();
        return output.ToArray();
    }

    public static async Task<string> DecryptAsync(string chave, byte[] texto)
    {
        using Aes aes = Aes.Create();

        //aes.Padding = PaddingMode.PKCS7; // None // Zeros // PKCS7
        aes.Key = DeriveKeyFromPassword(chave);

        aes.IV = IV;

        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream(texto);
        using var cs = new CryptoStream((Stream)ms, decryptor, CryptoStreamMode.Read);

        //using var sr = new StreamReader(cs);
        //return sr.ReadToEnd();

        using MemoryStream sr = new();
        await cs.CopyToAsync(sr);
        return Encoding.Unicode.GetString(sr.ToArray());
    }

    public static byte[] Hash256InBytes(string clientSecret)
    {
        using var hash = SHA256.Create();

        var bytes = hash.ComputeHash(Encoding.ASCII.GetBytes(clientSecret)); // Unicode // UTF8 // ASCII

        return bytes;
    }

    public static string Hash256(string clientSecret)
    {
        //using var hash = SHA256.Create();

        //var bytes = hash.ComputeHash(Encoding.ASCII.GetBytes(clientSecret));
        var bytes = Hash256InBytes(clientSecret);

        return string.Concat(bytes.Select(item => item.ToString("X2")));
    }

    public enum HashAlgorithm
    {
        MD5,
        SHA1,
        SHA256,
        SHA384,
        SHA512
    }

    public static byte[] ToRawHash(byte[] data, HashAlgorithm algorithm)
    {
        byte[] hash;
        switch (algorithm)
        {
            case HashAlgorithm.MD5:
                MD5 md5 = MD5.Create();
                hash = md5.ComputeHash(data, 0, data.Length);
                return hash;
            case HashAlgorithm.SHA1:
                SHA1Managed sha1 = new SHA1Managed();
                hash = sha1.ComputeHash(data);
                return hash;
            case HashAlgorithm.SHA256:
                SHA256Managed sha256 = new SHA256Managed();
                hash = sha256.ComputeHash(data);
                return hash;
            case HashAlgorithm.SHA384:
                SHA384Managed sha384 = new SHA384Managed();
                hash = sha384.ComputeHash(data);
                return hash;
            case HashAlgorithm.SHA512:
                SHA512Managed sha512 = new SHA512Managed();
                hash = sha512.ComputeHash(data, 0, data.Length);
                return hash;
            default:
                throw new ArgumentException("Invalid Algorithm");
        }
    }

}


using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit.Shared;

public static class Uteis
{
    public static string ToHmacSHA256(string message, string secret)
    {
        using var hmac = new HMACSHA256(key: Encoding.Default.GetBytes(secret)); // Encoding.UTF8 // Encoding.Default;
        var hmBytes = hmac.ComputeHash(buffer: Encoding.Default.GetBytes(message));
        return ToHexString(hmBytes); // Convert.ToBase64String(hmBytes); // string.Join("", hashmessage.ToList().Select(b => b.ToString("x2")).ToArray());
    }

    public static string ToHexString(byte[] array)
    {
        StringBuilder hex = new StringBuilder(array.Length * 2);
        foreach (byte b in array)
            hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }

    public static string ToJsonFormat(string json)
    {
        //var options = JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
        //{
        //    Formatting = Formatting.Indented,
        //    NullValueHandling = NullValueHandling.Ignore,
        //    ContractResolver = new CamelCasePropertyNamesContractResolver()
        //};
        //dynamic parsedJson = JsonConvert.DeserializeObject(json);
        //return JsonConvert.SerializeObject(parsedJson, options);
        //return JsonConvert.SerializeObject(json, options);

        var options = new JsonSerializerOptions()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.WriteAsString,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        return JsonSerializer.Serialize(JsonDocument.Parse(json), options);
        //return JsonSerializer.Serialize(json, options);
    }

    public async static Task<string> GetRequestWithoutAuth(string requestUri, bool removeData = true)
    {
        try
        {
            using var client = new HttpClient() { Timeout = Timeout.InfiniteTimeSpan };

            client.BaseAddress = new Uri("https://api.foxbit.com.br");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

            using var response = await client.GetAsync(requestUri: requestUri);

            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (removeData && !string.IsNullOrEmpty(responseBody))
            {
                responseBody = responseBody.Substring(8, responseBody.Length - 8);
                responseBody = responseBody.Remove(responseBody.Length - 1);
            }

            return responseBody;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERRO: {ex.Message}");
            throw;
        }
    }


    public async static Task<string> GetRequestWithAuth(string requestUri, string signature, long timestamp)
    {
        try
        {
            using var client = new HttpClient() { Timeout = Timeout.InfiniteTimeSpan };

            client.BaseAddress = new Uri("https://api.foxbit.com.br");
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("Content-Type", "application/json;charset=utf-8");
            //client.DefaultRequestHeaders.Add("Content-Language", "pt-BR");
            //client.DefaultRequestHeaders.Add("Accept", "application/json");
            //client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            //client.DefaultRequestHeaders.Add("pragma", "no-cache");
            //client.DefaultRequestHeaders.Add("cache-control", "no-cache");
            //client.DefaultRequestHeaders.Add("accept", "*/*");
            //client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.183 Safari/537.36");
            //client.DefaultRequestHeaders.Add("ccess-Control-Allow-Origi", "*");
            //client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            //client.DefaultRequestHeaders.Add("Access-Control-Allow-Methods", "GET, POST, PATCH, PUT, DELETE, OPTIONS");
            //client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Origin, Content-Type, Accept, Authorization, X-Auth-Token");
            //client.DefaultRequestHeaders.Add("X-Version","1");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-FB-ACCESS-KEY", "Ksh9WoRMI3xVdAklxI6mbfwZiWyGeIN6zSSGOmr7"); // Chave de acesso
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-FB-ACCESS-TIMESTAMP", timestamp.ToString());
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-FB-ACCESS-SIGNATURE", signature.ToString());
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-Idempotent", Guid.NewGuid().ToString());

            using var response = await client.GetAsync(requestUri: requestUri);

            //Console.WriteLine(response.Headers.ToString());
            //Console.WriteLine(response.RequestMessage);

            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            //Console.WriteLine(response);
            //Console.WriteLine(Uteis.ToJsonFormat(response));

            if (!string.IsNullOrEmpty(responseBody))
            {
                responseBody = responseBody.Substring(8, responseBody.Length - 8); // responseBody.Remove(0, 1);
                responseBody = responseBody.Remove(responseBody.Length - 1);
            }

            return responseBody;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERRO: {ex.Message}");
            throw;
        }
    }
}


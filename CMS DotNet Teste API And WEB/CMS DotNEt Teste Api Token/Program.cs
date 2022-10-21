using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        // static HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {

            //Console.WriteLine($"Request Login");
            //HttpResponseMessage responseLogin = await client.GetAsync("http://localhost:8001/login?user=john&pass=doe");
            //Console.WriteLine($"Response Login: {responseLogin.IsSuccessStatusCode}");
            //string result = await responseLogin.Content.ReadAsStringAsync();
            //Token token = JsonConvert.DeserializeObject<Token>(result);
            //Console.WriteLine($"token: {token.token}");

            //Console.WriteLine($"Request Restrito com Token");
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.token);
            //HttpResponseMessage responseRestrito = await client.GetAsync("http://localhost:8001/restricted2");
            //Console.WriteLine($"Response Restrito: {responseRestrito.IsSuccessStatusCode}");

            var client = new RestClient("http://localhost:8001/");

            Console.WriteLine($"");

            Console.WriteLine($"Request Login");
            var requestLogin = new RestRequest("login?user=john&pass=doe", Method.Get);
            RestResponse responseLogin = await client.ExecuteAsync(requestLogin);
            //Console.WriteLine("Response Login: " + responseLogin.Content);
            var obj = JObject.Parse(responseLogin.Content);
            var token = obj["token"];
            Console.WriteLine("token: " + token);

            Console.WriteLine($"");

            Console.WriteLine($"Request Restrito com Token");
            var requestRestrito = new RestRequest("restricted2", Method.Get);
            requestRestrito.AddHeader("cache-control", "no-cache");
            requestRestrito.AddHeader("Content-Type", "application/json");
            requestRestrito.AddHeader("Authorization", "Bearer " + token);
            RestResponse responseRestrito = await client.ExecuteAsync(requestRestrito);
            Console.WriteLine("Response Restrito: " + responseRestrito.Content);

            Console.WriteLine($"");

            Console.WriteLine("Fim...");
            Console.ReadKey();
        }

    }

    public class Token
    {
        public decimal exp { get; set; }
        public string token { get; set; }
    }

}

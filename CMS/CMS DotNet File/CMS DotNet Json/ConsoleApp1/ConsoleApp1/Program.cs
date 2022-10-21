using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

           //Serialize JSON
            Product product = new Product();
            product.Name = "Apple";
            product.Expiry = new DateTime(2008, 12, 28);
            product.Sizes = new string[] { "Small" };
            string jsonSerialize = JsonConvert.SerializeObject(product);
            Console.WriteLine(jsonSerialize);

            //Deserialize JSON
            string jsonDeserialize = @"{'Name': 'Bad Boys','ReleaseDate': '1995-4-7T00:00:00','Genres': ['Action', 'Comedy' ] }";
            Movie m = JsonConvert.DeserializeObject<Movie>(jsonDeserialize);
            string name = m.Name;
            Console.WriteLine(name);

            //LINQ to JSON
            JArray array = new JArray();
            array.Add("Manual text");
            array.Add(new DateTime(2000, 5, 23));
            JObject o = new JObject();
            o["MyArray"] = array;
            string jsonLINQ = o.ToString();
            Console.WriteLine(jsonLINQ);

            Console.ReadKey();
        }
    }

    internal class Movie
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string[] Genres { get; set; }
    }

    internal class Product
    {
        public string Name { get; set; }
        public DateTime Expiry { get; set; }
        public string[] Sizes { get; set; }
    }
}

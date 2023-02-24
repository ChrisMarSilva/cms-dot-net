using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {

        private readonly ILogger<ProductService> _logger;
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/product";

        public ProductService(ILogger<ProductService> logger, HttpClient client)
        {
            _logger = logger;
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<ProductModel>> FindAllProducts(string token)
        {
            _logger.LogInformation("Web.ProductService.FindAllProducts()");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(BasePath);
            var resultado = await response.ReadContentAs<List<ProductModel>>();
            return resultado;
        }

        public async Task<ProductModel> FindProductById(long id, string token)
        {
            _logger.LogInformation("Web.ProductService.FindProductById()");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{BasePath}/{id}");
            var resultado = await response.ReadContentAs<ProductModel>();
            return resultado;
        }

        public async Task<ProductModel> CreateProduct(ProductModel model, string token)
        {
            _logger.LogInformation("Web.ProductService.CreateProduct()");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJson(BasePath, model);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong when calling API");
            var resultado = await response.ReadContentAs<ProductModel>();
            return resultado;
        }

        public async Task<ProductModel> UpdateProduct(ProductModel model, string token)
        {
            _logger.LogInformation("Web.ProductService.UpdateProduct()");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJson(BasePath, model);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong when calling API");
            var resultado = await response.ReadContentAs<ProductModel>();
            return resultado;
        }

        public async Task<bool> DeleteProductById(long id, string token)
        {
            _logger.LogInformation("Web.ProductService.DeleteProductById()");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync($"{BasePath}/{id}");
            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong when calling API");
            var resultado = await response.ReadContentAs<bool>();
            return resultado;
        }
    }
}

using HttpClientFactoryProject.Configuration;
using HttpClientFactoryProject.Models;
using HttpClientFactoryProject.Abstractions;
using RestSharp;
using System.Text.Json;

namespace HttpClientFactoryProject.Services;

public class TodoService : ITodoService
{
    private readonly IApiConfig _config;
    private readonly IHttpClientFactory _httpClient; // HttpClient // IHttpClientFactory
    private readonly ITodoApi _todoApi;
    private readonly JsonSerializerOptions _options;

    public TodoService(
        IApiConfig config,
        IHttpClientFactory httpClient,
        ITodoApi todoApi)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _todoApi = todoApi ?? throw new ArgumentNullException(nameof(todoApi));
    }

    public async Task<TodoModel?> GetByIdAsync(int id)
    {
        var model = default(TodoModel);

        //var response = await _httpClient.GetFromJsonAsync<TodoModel>($"{_config.BaseUrl}/todos/{id}");
        //return response;

        var client = _httpClient.CreateClient("TodoApi");
        using var response = await client.GetAsync($"{_config.BaseUrl}/todos/{id}");

        if (response.IsSuccessStatusCode) // status code entre 200-299
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            model = await JsonSerializer.DeserializeAsync<TodoModel>(apiResponse, _options); // JsonConvert.DeserializeObject<TodoModel>(apiResponse);
        }

        return model; // null
    }

    public async Task<IEnumerable<TodoModel>?> GetAllAsync()
    {
        var models = default(IEnumerable<TodoModel>);

        //var options = new RestClientOptions(_config.BaseUrl)
        //{
        //    Encoding = Encoding.UTF8
        //};

        //var DefaultSettings = new JsonSerializerSettings
        //{
        //    ContractResolver = new CamelCasePropertyNamesContractResolver(),
        //    DefaultValueHandling = DefaultValueHandling.Include,
        //    TypeNameHandling = TypeNameHandling.None,
        //    NullValueHandling = NullValueHandling.Ignore,
        //    Formatting = Formatting.None,
        //    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
        //};

        var client = new RestClient(_config.BaseUrl); // new RestClient(options, configureSerialization: cfg => cfg.UseSystemTextJson()); // UseSystemTextJson // UseNewtonsoftJson 

        var request = new RestRequest("/todos", Method.Get);
        request.AddHeader("Accept", "application/json");

        var response = await client.ExecuteAsync<IEnumerable<TodoModel>>(request); // await client.GetJsonAsync<IEnumerable<TodoModel>>("/todos");

        if (response.IsSuccessful)
            models = response.Data; //  Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TodoModel>>(response.Content!); // JsonConvert.DeserializeObject<ICollection<TodoModel>>(response.Content!);

        return models;
    }

    public async Task<IEnumerable<TodoModel>?> GetTodosAsync()
    {
        return await _todoApi.ReturnTodo();
    }
}

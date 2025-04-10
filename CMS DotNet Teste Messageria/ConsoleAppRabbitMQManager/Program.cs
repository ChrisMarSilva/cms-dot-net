using ConsoleAppRabbitMQManager;
using RabbitMQ.Stream.Client;
using RabbitMQ.Stream.Client.Reliable;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

Console.WriteLine("INI");
try
{
    var rabbitUrl = "http://10.10.20.36:15672/api/queues/chris_pix_automatico";
    var virtualHost = "chris_pix_automatico";
    var username = "jdpi";
    var password = "jdpi";

    //await ListAllQueues(rabbitUrl, virtualHost, username, password);
    await ListQueuesWithMessages(rabbitUrl, virtualHost, username, password);
    await ListErrorSkippedQueues(rabbitUrl, virtualHost, username, password);
    //await ClearAllQueues(rabbitUrl, virtualHost, username, password);
}
catch (Exception ex)
{
    Console.WriteLine($"\nERRO: {ex.Message}");
}
finally
{
    Console.WriteLine("\nFIM");
}

static async Task<List<QueueInfo>> GetQueues(string rabbitUrl, string virtualHost, string username, string password)
{
    using var client = new HttpClient();

    var byteArray = System.Text.Encoding.ASCII.GetBytes($"{username}:{password}");
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

    var response = await client.GetAsync(rabbitUrl);
    response.EnsureSuccessStatusCode();

    var json = await response.Content.ReadAsStringAsync();
    var filas = JsonSerializer.Deserialize<List<QueueInfo>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    return filas;
}

static async Task ListAllQueues(string rabbitUrl, string virtualHost, string username, string password)
{
    var queues = await GetQueues(rabbitUrl, virtualHost, username, password);

    if (queues.Count == 0)
    {
        Console.WriteLine("Nenhuma fila encontrada.");
        return;
    }

    Console.WriteLine($"\n=== Todas as Filas ({queues.Count.ToString("D3")}) ===");
    foreach (var queue in queues)
    {
        //if (!queue.Name.Equals("jd.pi.auditoria.command.queue", StringComparison.OrdinalIgnoreCase))
        //    continue;

        Console.WriteLine($"Mensagens: {queue.Messages.ToString("D3")} - Tipo: {queue.Type} - Fila: {queue.Name}");
    }
}

static async Task ListQueuesWithMessages(string rabbitUrl, string virtualHost, string username, string password)
{
    var queues = await GetQueues(rabbitUrl, virtualHost, username, password);

    if (queues.Count == 0)
    {
        Console.WriteLine("Nenhuma fila encontrada.");
        return;
    }

    Console.WriteLine($"\n=== Filas com registros pendente ===");

    bool found = false;
    foreach (var queue in queues)
    {
    //    if (!queue.Name.Equals("jd.pi.auditoria.command.queue", StringComparison.OrdinalIgnoreCase))
    //        continue;

        if (queue.Messages > 0)
        {
            Console.WriteLine($"Mensagens: {queue.Messages.ToString("D3")} - Tipo: {queue.Type} - Fila: {queue.Name}");
            found = true;
        }
    }

    if (!found)
    {
        Console.WriteLine("Nenhuma fila com mensagens pendentes encontrada.");
    }
}

static async Task ListErrorSkippedQueues(string rabbitUrl, string virtualHost, string username, string password)
{
    var queues = await GetQueues(rabbitUrl, virtualHost, username, password);

    if (queues.Count == 0)
    {
        Console.WriteLine("Nenhuma fila encontrada.");
        return;
    }

    Console.WriteLine($"\n=== Filas com '_error' ou '_skipped' ===");

    bool found = false;
    foreach (var queue in queues)
    {
        if (queue.Name.EndsWith("_error") || queue.Name.EndsWith("_skipped"))
        {
            Console.WriteLine($"Mensagens: {queue.Messages.ToString("D3")} - Tipo: {queue.Type} - Fila: {queue.Name}");
            found = true;
        }
    }

    if (!found)
    {
        Console.WriteLine("Nenhuma fila com sufixo '_error' ou '_skipped' encontrada.");
    }
}

static async Task ClearAllQueues(string rabbitUrl, string virtualHost, string username, string password)
{
    var queues = await GetQueues(rabbitUrl, virtualHost, username, password);

    using var client = new HttpClient();
    var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

    foreach (var queue in queues)
    {
        // jd.pi.auditoria.command.queue
        // notifica-autorizacao-pagador-callback
        //if (!queue.Name.Equals("notifica-autorizacao-pagador-callback", StringComparison.OrdinalIgnoreCase))
        //    continue;

        if (queue.Type.Equals("classic", StringComparison.OrdinalIgnoreCase))
        {
            // Para filas do tipo 'classic', utilize o método DELETE na API REST

            if (queue.Messages <= 0)
                continue;

            // A URL para purge: /api/queues/{vhost}/{queueName}/contents. Se estiver usando o vhost padrão "/", ele deve ser codificado como %2F.
            var uri = $"http://10.10.20.36:15672/api/queues/chris_pix_automatico/{queue.Name}/contents";
            var response = await client.DeleteAsync(uri);

            if (queue.Name.Equals("secondary-queue", StringComparison.OrdinalIgnoreCase))
            {
                //var manager = new RabbitMqManager("http://localhost:15672", "guest", "guest");
                //await manager.DeleteClassicQueueAsync("minha-fila-classic");
            }

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Fila '{queue.Name}' limpa com sucesso.");
            }
            else
            {
                Console.WriteLine($"Erro ao limpar a fila '{queue.Name}': {response.StatusCode}");
            }
        }
        else if (queue.Type.Equals("stream", StringComparison.OrdinalIgnoreCase))
        {
            // Para filas do tipo 'stream', utilize o cliente RabbitMQ Stream para deletar a fila
            try
            {
                var streamSystemConfig = new StreamSystemConfig
                {
                    UserName = username,
                    Password = password,
                    VirtualHost = virtualHost,
                    Endpoints =
                    [
                        new DnsEndPoint("rabbitmq", 5552)
                        //new IPEndPoint(IPAddress.Parse("192.168.0.4"), 5552)
                        //new IPEndPoint(IPAddress.Loopback, 5552)
                    ]
                };
                
                var streamSystem = await StreamSystem.Create(streamSystemConfig);

                if (!await streamSystem.StreamExists(queue.Name))
                {
                    Console.WriteLine($"Stream '{queue.Name}' não encontrado.");
                    continue;
                }

                var consumer = await Consumer.Create(new ConsumerConfig(streamSystem, queue.Name)
                {
                    OffsetSpec = new OffsetTypeFirst(),
                    MessageHandler = async (stream, _, _, message) =>
                    {
                        var content = Encoding.UTF8.GetString(message.Data.Contents);
                        Console.WriteLine($"Stream: {stream} - Received message: {content}");
                        await Task.CompletedTask;
                    }
                });

                //await streamSystem.DeleteStream(queue.Name);
                //Console.WriteLine($"Fila '{queue.Name}' (stream) deletada com sucesso.");

                await consumer.Close();
                await streamSystem.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar a fila '{queue.Name}' ({queue.Type}): {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"Type '{queue.Type}' desconhecido.");
        }
    }

    Console.WriteLine("Limpeza de todas as filas concluída.");
}

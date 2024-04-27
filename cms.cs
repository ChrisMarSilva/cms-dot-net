#DotNet

Projeto.App
Projeto.Worker
Projeto.Infrastructure
Projeto.Application
Projeto.Domain
Projeto.Persistence
Projeto.Presentation
Projeto.Shared





https://blog.casadodesenvolvedor.com.br/postgresql-performance/

		
var sql = @"SELECT ProductID, ProductName, p.CategoryID, CategoryName FROM Products p  INNER JOIN Categories c ON p.CategoryID = c.CategoryID";

var products = await connection.QueryAsync<Product, Category, Product>(sql, (product, category) => {
	product.Category = category;
	return product;
}, 
splitOn: "CategoryId" );

products.ToList().ForEach(product => Console.WriteLine($"Product: {product.ProductName}, Category: {product.Category.CategoryName}"));

var sql = @"select * from Product p 
            inner join Customer c on p.CustomerId = c.CustomerId 
            order by p.ProductName";

var data = con.Query<ProductItem, Customer, ProductItem>(
    sql,
    (productItem, customer) => {
        productItem.Customer = customer;
        return productItem;
    },
    splitOn: "CustomerId,CustomerName"
);



    var sql = @"SELECT p.PostId, Headline, t.TagId, TagName
                FROM Posts p 
                INNER JOIN PostTags pt ON pt.PostId = p.PostId
                INNER JOIN Tags t ON t.TagId = pt.TagId";
				
    var posts = await connection.QueryAsync<Post, Tag, Post>(sql, (post, tag) => {
        post.Tags.Add(tag);
        return post;a
    }, splitOn: "TagId");

    var result = posts.GroupBy(p => p.PostId).Select(g =>
    {
        var groupedPost = g.First();
        groupedPost.Tags = g.Select(p => p.Tags.Single()).ToList();
        return groupedPost;
    });
	
    foreach(var post in result)
        foreach(var tag in post.Tags)
            Console.Write($" {tag.TagName} ");



  return conexao.Query<Cliente, Passaporte, Cliente>(
              "SELECT * " +
              "FROM Clientes C " +
              "INNER JOIN Passaportes P ON C.ClienteId = P.ClienteId " +
              "ORDER BY C.Nome",
              map: (cliente, passaporte) =>
              {
                  cliente.DadosPassaporte = passaporte;
                  return cliente;
              },
             splitOn: "ClienteId,PassaporteId");
       }
	   
	   
	   
	   
	   
	   
	   




MONGO DB EM C#


 "ConnectionStrings": {
    "MongoDB": "mongodb://localhost:27017",
    "DatabaseName": "taskManager"
  }
  
  
 services.Configure<DatabaseConfig>(Configuration.GetSection(nameof(DatabaseConfig)));
            services.AddSingleton<IDatabaseConfig>(sp => sp.GetRequiredService<IOptions<DatabaseConfig>>().Value);
			
			
private readonly IMongoCollection<Tarefa> _tarefas;

        public TarefasRepository(IDatabaseConfig databaseConfig)
        {
            var client = new MongoClient(databaseConfig.ConnectionString);
            var database = client.GetDatabase(databaseConfig.DatabaseName);

            _tarefas = database.GetCollection<Tarefa>("tarefas");
        }

        public void Adicionar(Tarefa tarefa)
        {
            _tarefas.InsertOne(tarefa);
        }

        public void Atualizar(string id, Tarefa tarefaAtualizada)
        {
            _tarefas.ReplaceOne(tarefa => tarefa.Id == id, tarefaAtualizada);
        }

        public IEnumerable<Tarefa> Buscar()
        {
            return _tarefas.Find(tarefa => true).ToList();
        }

        public Tarefa Buscar(string id)
        {
            return _tarefas.Find(tarefa => tarefa.Id == id).FirstOrDefault();
        }

        public void Remover(string id)
        {
            _tarefas.DeleteOne(tarefa => tarefa.Id == id);
        }
		
		


https://github.com/angelitocsg/coding-live-007/blob/master/src/CachedAPI/Repositories/ProductRepository.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Bogus;
using CachedAPI.Constants;
using CachedAPI.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace CachedAPI.Repositories
{
    public class ProductRedisRepository
    {
        private static List<Product> data;
        private readonly Faker faker;
        private readonly IDistributedCache _redisCache;

        public ProductRedisRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;

            if (data != null) return;

            faker = new Faker();
            data = new List<Product>();

            for (int i = 0; i < 5000; i++)
            {
                data.Add(GetRandom());
            }
        }

        private Product GetRandom()
        {
            return new Product(
                id: Guid.NewGuid(),
                productName: $"{faker.Commerce.Product()}: {faker.Commerce.ProductName()}",
                category: faker.Commerce.Categories(1).First(),
                price: Decimal.Parse(faker.Commerce.Price()),
                ean13: faker.Commerce.Ean13()
            );
        }

        private IEnumerable<Product> GetFromCache(string cacheKey)
        {
            var cacheData = _redisCache.GetString(cacheKey);

            if (cacheData != null)
                return JsonSerializer.Deserialize<IEnumerable<Product>>(cacheData);

            return null;
        }

        private DistributedCacheEntryOptions GetCacheOptions(
            int slidingExpirationSecs = 0,
            int absoluteExpirationSecs = 0)
        {
            var cacheOptions = new DistributedCacheEntryOptions();

            if (slidingExpirationSecs > 0)
                cacheOptions.SetSlidingExpiration(TimeSpan.FromSeconds(slidingExpirationSecs)); // inactive

            if (absoluteExpirationSecs > 0)
                cacheOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(absoluteExpirationSecs)); // absolute

            return cacheOptions;
        }

        public IEnumerable<Product> GetAll()
        {
            IEnumerable<Product> products = GetFromCache(CacheKeys.PRODUCTS_GET_ALL);
            if (products != null) { return products; }

            products = data;

            System.Threading.Thread.Sleep(5000);
            Console.WriteLine($"Created cache entry: {CacheKeys.PRODUCTS_GET_ALL}");

            var toCache = JsonSerializer.Serialize(products);
            _redisCache.SetString(CacheKeys.PRODUCTS_GET_ALL, toCache, GetCacheOptions(15, 30));

            return products;
        }

        public IEnumerable<Product> GetProductsByName(string name)
        {
            IEnumerable<Product> products = GetFromCache($"{CacheKeys.PRODUCTS_GET_BY_NAME}_{name}");
            if (products != null) { return products; }

            products = data.Where(it => it.ProductName.Contains(name, StringComparison.InvariantCultureIgnoreCase));

            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Created cache entry: Products_GetByName");

            var toCache = JsonSerializer.Serialize(products);
            _redisCache.SetString($"{CacheKeys.PRODUCTS_GET_BY_NAME}_{name}", toCache, GetCacheOptions(10));

            return products;
        }

        public IEnumerable<Product> GetProductsByLowerPrice(decimal price)
        {
            IEnumerable<Product> products = GetFromCache(CacheKeys.PRODUCTS_GET_BY_LOWER_PRICE);
            if (products != null) { return products; }

            products = data.Where(it => it.Price <= price);

            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Created cache entry: Products_GetByLowerPrice");

            var toCache = JsonSerializer.Serialize(products);
            _redisCache.SetString(CacheKeys.PRODUCTS_GET_BY_LOWER_PRICE, toCache, GetCacheOptions(0, 10));

            return products;
        }

        public IEnumerable<Product> GetProductsByUpperPrice(decimal price)
        {
            IEnumerable<Product> products = GetFromCache(CacheKeys.PRODUCTS_GET_BY_UPPER_PRICE);
            if (products != null) { return products; }

            products = data.Where(it => it.Price >= price);

            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Created cache entry: Products_GetByUpperPrice");

            var toCache = JsonSerializer.Serialize(products);
            _redisCache.SetString(CacheKeys.PRODUCTS_GET_BY_UPPER_PRICE, toCache, GetCacheOptions(0, 10));

            return products;
        }
    }
}






using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using CachedAPI.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace CachedAPI.Repositories
{
    public class ProductRepository
    {
        private static List<Product> data;
        private readonly Faker faker;
        private readonly IMemoryCache _memCache;

        public ProductRepository(IMemoryCache memCache)
        {
            _memCache = memCache;

            if (data != null) return;

            faker = new Faker();
            data = new List<Product>();

            for (int i = 0; i < 5000; i++)
            {
                data.Add(GetRandom());
            }
        }

        private Product GetRandom()
        {
            return new Product(
                id: Guid.NewGuid(),
                productName: $"{faker.Commerce.Product()}: {faker.Commerce.ProductName()}",
                category: faker.Commerce.Categories(1).First(),
                price: Decimal.Parse(faker.Commerce.Price()),
                ean13: faker.Commerce.Ean13()
            );
        }

        public IEnumerable<Product> GetAll()
        {
            var products = _memCache.GetOrCreate("Products_GetAll", entry =>
            {
                Console.WriteLine("Created cache entry: Products_GetAll");

                entry.SlidingExpiration = TimeSpan.FromSeconds(15); // inactive
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30); // absolute
                entry.SetPriority(CacheItemPriority.High); // priority

                System.Threading.Thread.Sleep(5000);
                return data;
            });

            return products;
        }

        public IEnumerable<Product> GetProductsByName(string name)
        {
            var products = _memCache.GetOrCreate("Products_GetByName_" + name, entry =>
              {
                  Console.WriteLine("Created cache entry: Products_GetByName_" + name);

                  entry.SlidingExpiration = TimeSpan.FromSeconds(10); // inactive
                  entry.SetPriority(CacheItemPriority.High); // priority

                  System.Threading.Thread.Sleep(3000);
                  return data.Where(it => it.ProductName.Contains(name, StringComparison.InvariantCultureIgnoreCase));
              });

            return products;
        }

        public IEnumerable<Product> GetProductsByLowerPrice(decimal price)
        {
            var products = _memCache.GetOrCreate("Products_GetByLowerPrice", entry =>
            {
                Console.WriteLine("Created cache entry: Products_GetByLowerPrice");

                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10); // absolute
                entry.SetPriority(CacheItemPriority.Low); // priority

                System.Threading.Thread.Sleep(2000);
                return data.Where(it => it.Price <= price);
            });

            return products;
        }

        public IEnumerable<Product> GetProductsByUpperPrice(decimal price)
        {
            var products = _memCache.GetOrCreate("Products_GetByUpperPrice", entry =>
            {
                Console.WriteLine("Created cache entry: Products_GetByUpperPrice");

                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10); // absolute
                entry.SetPriority(CacheItemPriority.Low); // priority

                System.Threading.Thread.Sleep(2000);
                return data.Where(it => it.Price >= price);
            });

            return products;
        }
    }
}





https://www.youtube.com/watch?v=85YbMEb1qkQ
https://www.youtube.com/watch?v=F3xNCfP3Xew
https://www.youtube.com/watch?v=AVBAAKa84cs
https://www.youtube.com/watch?v=vdi-p9StmG0
https://www.youtube.com/watch?v=PxxZcTBYi34


https://www.milanjovanovic.tech/blog/cqrs-pattern-with-mediatr

[ApiController]
[Route("api/bookings")]
public class BookingsController : ControllerBase
{
    private readonly ISender _sender;

    public BookingsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPut("{id}/confirm")]
    public async Task<IActionResult> ConfirmBooking(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new ConfirmBookingCommand(id);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }
}

internal sealed class ConfirmBookingCommandHandler
    : ICommandHandler<ConfirmBookingCommand>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmBookingCommandHandler(
        IDateTimeProvider dateTimeProvider,
        IBookingRepository bookingRepository,
        IUnitOfWork unitOfWork)
    {
        _dateTimeProvider = dateTimeProvider;
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        ConfirmBookingCommand request,
        CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdAsync(
            request.BookingId,
            cancellationToken);

        if (booking is null)
        {
            return Result.Failure(BookingErrors.NotFound);
        }

        var result = booking.Confirm(_dateTimeProvider.UtcNow);

        if (result.IsFailure)
        {
            return result;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}




internal sealed class SearchApartmentsQueryHandler
    : IQueryHandler<SearchApartmentsQuery, IReadOnlyList<ApartmentResponse>>
{
    private static readonly int[] ActiveBookingStatuses =
    {
        (int)BookingStatus.Reserved,
        (int)BookingStatus.Confirmed,
        (int)BookingStatus.Completed
    };

    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public SearchApartmentsQueryHandler(
        ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<ApartmentResponse>>> Handle(
        SearchApartmentsQuery request,
        CancellationToken cancellationToken)
    {
        if (request.StartDate > request.EndDate)
        {
            return new List<ApartmentResponse>();
        }

        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                a.id AS Id,
                a.name AS Name,
                a.description AS Description,
                a.price_amount AS Price,
                a.price_currency AS Currency,
                a.address_country AS Country,
                a.address_state AS State,
                a.address_zip_code AS ZipCode,
                a.address_city AS City,
                a.address_street AS Street
            FROM apartments AS a
            WHERE NOT EXISTS
            (
                SELECT 1
                FROM bookings AS b
                WHERE
                    b.apartment_id = a.id AND
                    b.duration_start <= @EndDate AND
                    b.duration_end >= @StartDate AND
                    b.status = ANY(@ActiveBookingStatuses)
            )
            """;

        var apartments = await connection
            .QueryAsync<ApartmentResponse, AddressResponse, ApartmentResponse>(
                sql,
                (apartment, address) =>
                {
                    apartment.Address = address;

                    return apartment;
                },
                new
                {
                    request.StartDate,
                    request.EndDate,
                    ActiveBookingStatuses
                },
                splitOn: "Country");

        return apartments.ToList();
    }
}







using JD.NPC.Destinatario.Beneficiario.Core.ServiceBus.Consumers;
using JD.NPC.Destinatario.Beneficiario.Shared.Commands;
using JD.Sdk.Extensions;
using JD.SFN.Contracts;
using JD.SFN.Contracts.Commands;
using JD.SFN.Contracts.Events;
using JD.SFN.Files;
using JD.SFN.Messages;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace JD.NPC.Destinatario.Beneficiario.Core.ServiceBus;

public static class Register
{
    public static IServiceCollection AddServiceBusForApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((_, cfg) =>
            {
                cfg.ConfigureJsonSerializerOptions(options =>
                {
                    options.TypeInfoResolverChain.Insert(0, BnfJsonSerializerContext.Default);
                    return options;
                });

                cfg.UsePrometheusMetrics(serviceName: "jd-npc-beneficiario-api", configuration);

                cfg.Host(configuration["Rabbit:Host"], configuration.GetValue<ushort>("Rabbit:Port", 5672), configuration.GetValue("Rabbit:VirtualHost", "/"), c =>
                {
                    c.Username(configuration["Rabbit:Username"]);
                    c.Password(configuration["Rabbit:Password"]);
                    c.ConfigureBatchPublish(b =>
                    {
                        b.Enabled = true;
                        b.Timeout = TimeSpan.FromMilliseconds(4);
                        b.MessageLimit = 100;
                        b.SizeLimit = 64 * 1024;
                    });

                    c.PublisherConfirmation = true;
                    c.UseDefaultClusterConfiguration(configuration);
                    c.UseDefaultSslConfiguration(configuration);
                });

                cfg.Message<Fault>(e => e.SetEntityName("jd.fault"));
                cfg.Message<BeneficiarioInclusaoCommandDto>(e => e.SetEntityName(BeneficiarioInclusaoCommandDto.EntityName)); // INCLUIR - DDA0501
                cfg.Message<BeneficiarioConsultaNucleaCommandDto>(e => e.SetEntityName(BeneficiarioConsultaNucleaCommandDto.EntityName)); // CONSULTAR - DDA0504
            });
        });

        services.AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {
                options.WaitUntilStarted = false;
                options.StartTimeout = TimeSpan.FromSeconds(30);
                options.StopTimeout = TimeSpan.FromSeconds(30);
            });

        return services;
    }

    public static IServiceCollection AddServiceBusForWorker(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<BeneficiarioInclusaoConsumer>(); // INCLUIR - DDA0501
            x.AddConsumer<BeneficiarioConsultaNucleaConsumer>(); // CONSULTAR - DDA0504
            x.AddConsumer<SfnEventConsumer>();
            x.AddConsumer<SfnSendCommandResultConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureJsonSerializerOptions(options =>
                {
                    options.TypeInfoResolverChain.Insert(0, BnfJsonSerializerContext.Default);
                    options.TypeInfoResolverChain.Insert(0, SfnCommandJsonSerializerContext.Default);
                    return options;
                });

                cfg.UsePrometheusMetrics(serviceName: "jd-npc-beneficiario-worker", configuration);

                cfg.Host(configuration["Rabbit:Host"], configuration.GetValue<ushort>("Rabbit:Port", 5672), configuration.GetValue("Rabbit:VirtualHost", "/"), c =>
                {
                    c.Username(configuration["Rabbit:Username"]);
                    c.Password(configuration["Rabbit:Password"]);
                    c.ConfigureBatchPublish(b =>
                    {
                        b.Enabled = true;
                        b.Timeout = TimeSpan.FromMilliseconds(4);
                        b.MessageLimit = 100;
                        b.SizeLimit = 64 * 1024;
                    });

                    c.PublisherConfirmation = true;
                    c.UseDefaultClusterConfiguration(configuration);
                    c.UseDefaultSslConfiguration(configuration);
                });

                #region INCLUIR - DDA0501
                cfg.ReceiveEndpoint(BeneficiarioInclusaoCommandDto.QueueName, c =>
                {
                    c.UseDefaultMessageRetry(retryLimit: 10);
                    c.UseInMemoryOutbox(context, o => o.ConcurrentMessageDelivery = true);
                    c.ConsumerPriority = 5;
                    c.PrefetchCount = 32;
                    c.ConfigureConsumer<BeneficiarioInclusaoConsumer>(context);
                });
                #endregion

                #region CONSULTAR - DDA0504
                cfg.ReceiveEndpoint(BeneficiarioConsultaNucleaCommandDto.QueueName, c =>
                {
                    c.UseDefaultMessageRetry(retryLimit: 10);
                    c.UseInMemoryOutbox(context, o => o.ConcurrentMessageDelivery = true);
                    c.ConsumerPriority = 5;
                    c.PrefetchCount = 32;
                    c.ConfigureConsumer<BeneficiarioConsultaNucleaConsumer>(context);
                });
                #endregion

                #region Recepção de mensagens
                foreach (var messageType in new[] { MessageType.Dda0501R1, MessageType.Dda0501E, // INCLUIR - DDA0501
                                                    MessageType.Dda0504R1, MessageType.Dda0504E, // CONSULTAR - DDA0504
                                                    MessageType.Dda0400, // progressão de movimento - DDA0400
                                                    MessageType.Dda0403, // abertura de grade - DDA0403
                                                    MessageType.Gen0004 })
                {
                    cfg.ReceiveEndpoint($"jd.npc.beneficiario.{messageType.GetDescription()}.event.queue".ToLower(), c =>
                    {
                        c.UseDefaultMessageRetry();
                        if (messageType is MessageType.Gen0004 or MessageType.Dda0400)
                        {
                            c.ConsumerPriority = 1; //Não tem tanta prioridade
                            c.PrefetchCount = 8;
                        }
                        else
                        {
                            c.ConsumerPriority = 4;
                            c.PrefetchCount = 32;
                        }
                        c.ConfigureConsumeTopology = false;
                        c.Bind(SfnReceiveEvent.EntityName, s =>
                        {
                            s.RoutingKey = messageType.GetDescription().ToLower();
                            s.ExchangeType = "direct";
                        });
                        c.ConfigureConsumer<SfnEventConsumer>(context);
                    });
                }
                #endregion

                #region Status das mensagens enviadas
                foreach (var messageType in new[] {
                    MessageType.Dda0501, // INCLUIR - DDA0501
                    MessageType.Dda0504, // CONSULTAR - DDA0504
                })
                {
                    cfg.ReceiveEndpoint($"jd.npc.beneficiario.{messageType.GetDescription()}.send-result.event.queue".ToLower(), c =>
                    {
                        c.UseDefaultMessageRetry();
                        c.ConsumerPriority = 1;
                        c.PrefetchCount = 8;
                        c.ConfigureConsumeTopology = false;
                        c.Bind(SfnSendCommandResult.EntityName, s =>
                        {
                            s.RoutingKey = messageType.GetDescription().ToLower();
                            s.ExchangeType = "topic";
                        });
                        c.ConfigureConsumer<SfnSendCommandResultConsumer>(context);
                    });
                }
                #endregion

                cfg.Message<Fault>(e => e.SetEntityName("jd.fault"));
                cfg.Message<BeneficiarioInclusaoCommandDto>(e => e.SetEntityName(BeneficiarioInclusaoCommandDto.EntityName)); // INCLUIR - DDA0501
                cfg.Message<BeneficiarioConsultaNucleaCommandDto>(e => e.SetEntityName(BeneficiarioConsultaNucleaCommandDto.EntityName)); // CONSULTAR - DDA0504
                cfg.Message<SfnSendCommand>(e => e.SetEntityName(SfnSendCommand.EntityName));
                cfg.Message<SfnSendCommandResult>(e => e.SetEntityName(SfnSendCommandResult.EntityName));
            });
        });

        services.AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {
                options.WaitUntilStarted = false;
                options.StartTimeout = TimeSpan.FromSeconds(30);
                options.StopTimeout = TimeSpan.FromSeconds(30);
            });

        return services;
    }

    public static IServiceCollection AddServiceBusForFileWorker(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<SfnReceiveFileEventConsumer>();
            x.AddConsumer<BeneficiarioInclusaoAdda501Consumer>();
            x.AddConsumer<SfnSendFileCommandResultConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.UsePrometheusMetrics(serviceName: "jd-npc-beneficiario-file-worker", configuration);

                cfg.Host(configuration["Rabbit:Host"], configuration.GetValue<ushort>("Rabbit:Port", 5672), configuration.GetValue("Rabbit:VirtualHost", "/"), c =>
                {
                    c.Username(configuration["Rabbit:Username"]);
                    c.Password(configuration["Rabbit:Password"]);
                    c.ConfigureBatchPublish(b =>
                    {
                        b.Enabled = true;
                        b.Timeout = TimeSpan.FromMilliseconds(4);
                        b.MessageLimit = 100;
                        b.SizeLimit = 64 * 1024;
                    });

                    c.PublisherConfirmation = true;
                    c.UseDefaultClusterConfiguration(configuration);
                    c.UseDefaultSslConfiguration(configuration);
                });

                #region Recepção de arquivos
                foreach (var fileType in new[]
                {
                    FileType.ADda500Ret, FileType.ADda500Err, FileType.ADda500Pro, // INCLUIR - ADDA500
                    FileType.ADda504, // BASE BENEF - ADDA504
                })
                {
                    cfg.ReceiveEndpoint($"jd.npc.beneficiario.{fileType.GetDescription()}.event.queue".ToLower(), c =>
                    {
                        //Não tem tanta prioridade

                        c.UseDefaultMessageRetry();
                        c.ConsumerPriority = 2;
                        c.PrefetchCount = 8;
                        c.ConfigureConsumeTopology = false;

                        c.Bind(SfnReceiveFileEvent.EntityName, s =>
                        {
                            s.RoutingKey = fileType.GetDescription().ToLower();
                            s.ExchangeType = "direct";
                        });

                        c.ConfigureConsumer<SfnReceiveFileEventConsumer>(context);
                    });
                }
                #endregion

                #region ADDA500 – Inclusão de cliente beneficiário na carga inicial
                cfg.ReceiveEndpoint(SendBeneficiarioInclusaoToCipCommand.QueueNameToGenerateAdda500, c =>
                {
                    c.UseDefaultMessageRetry();
                    c.UseInMemoryOutbox(context, o => o.ConcurrentMessageDelivery = true);
                    c.ConsumerPriority = 3;
                    c.PrefetchCount = 50_000;
                    c.ConcurrentMessageLimit = 1;

                    c.ConfigureConsumer<BeneficiarioInclusaoAdda501Consumer>(context, cc =>
                    {
                        cc.Options<BatchOptions>(options => options
                            .SetMessageLimit(50_000)
                            .SetTimeLimit(TimeSpan.FromSeconds(configuration.GetValue("TimeToWaitForAdda500", 1800)))
                            .SetConcurrencyLimit(1));
                    });
                });
                #endregion

                #region Status dos arquivos enviados
                foreach (var fileType in new[] { FileType.ADda500 })
                {
                    cfg.ReceiveEndpoint($"jd.npc.beneficiario.{fileType.GetDescription()}.send-result.event.queue".ToLower(), c =>
                    {
                        c.UseDefaultMessageRetry();
                        c.ConsumerPriority = 1;
                        c.PrefetchCount = 8;
                        c.ConfigureConsumeTopology = false;

                        c.Bind(SfnSendFileCommandResult.EntityName, s =>
                        {
                            s.RoutingKey = fileType.GetDescription().ToLower();
                            s.ExchangeType = "topic";
                        });

                        c.ConfigureConsumer<SfnSendFileCommandResultConsumer>(context);
                    });
                }
                #endregion

                cfg.Message<Fault>(e => e.SetEntityName("jd.fault"));
                cfg.Message<SfnReceiveFileEvent>(e => e.SetEntityName(SfnReceiveFileEvent.EntityName));
                cfg.Message<SfnSendFileCommandResult>(e => e.SetEntityName(SfnSendFileCommandResult.EntityName));
            });
        });

        services.AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {
                options.WaitUntilStarted = false;
                options.StartTimeout = TimeSpan.FromSeconds(30);
                options.StopTimeout = TimeSpan.FromSeconds(30);
            });

        return services;
    }

}

[JsonSerializable(typeof(BeneficiarioInclusaoCommandDto))]
[JsonSerializable(typeof(Fault<BeneficiarioInclusaoCommandDto>))]
internal partial class BnfJsonSerializerContext : JsonSerializerContext;

//using BenchmarkDotNet.Attributes;
//using BenchmarkDotNet.Configs;
//using BenchmarkDotNet.Order;
//using CMS_DotNet_Teste_Object_Mapping.Dtos;
//using CMS_DotNet_Teste_Object_Mapping.Mappers;
//using CMS_DotNet_Teste_Object_Mapping.Models;
//using CMS_DotNet_Teste_Object_Mapping.Providers;
//using Mapster;
//using Nelibur.ObjectMapper;

//namespace CMS_DotNet_Teste_Object_Mapping.Benchmarks;

//[MemoryDiagnoser]
//[KeepBenchmarkFiles(false)]
//[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByMethod)]
//[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
//[MarkdownExporter, HtmlExporter, CsvExporter, RPlotExporter]//[RankColumn]
//[RankColumn]
//public class BenchmarkMappingCity
//{
//    [Params(1, 10, 100, 1_000, 10_000, 100_000)]
//    private readonly int NumberOfItems;
//    private readonly MapperlyMapperConfigurator _mapperly;
//    private readonly IEnumerable<PersonModel> _person; // IEnumerable, ICollection, IList
//    //private readonly Person _person = null!;
//    //private readonly SpotifyAlbumDto _spotifyAlbumDto;
//    //private readonly List<CityDto> _cityDtos;
//    //private readonly List<CustomerDto> _customerDtos;

//    public BenchmarkMappingCity()
//    {
//        AutoMapperConfigurator.SetUp();
//        ExpressMapperConfigurator.SetUp();
//        TinyMapperConfigurator.SetUp();
//        _mapperly = new MapperlyMapperConfigurator();
//        //Mapster don't need configuration
//        //AgileMapper don't need configuration

//        _person = DataProviderTynamix.GetDataPersons(NumberOfItems);
//    }

//    [GlobalSetup]
//    public void Setup()
//    {
//        // _cityDtos = DataProvider.GetAddresses(CitysCount);
//        // _customerDtos = DataProvider.GetData(CustomersCount);
//        //var json = File.ReadAllText("spotifyAlbum.json");
//        //_spotifyAlbumDto = SpotifyAlbumDto.FromJson(json);
//    }

//    //    [Benchmark] public void AgileMapper() AgileObjects.AgileMapper.Mapper.Map(_spotifyAlbumDto).ToANew<SpotifyAlbum>();
//    //    [Benchmark] public void TinyMapper() Nelibur.ObjectMapper.TinyMapper.Map<SpotifyAlbum>(_spotifyAlbumDto);
//    //    [Benchmark] public void ExpressMapper() global::ExpressMapper.Mapper.Map<SpotifyAlbumDto, SpotifyAlbum>(_spotifyAlbumDto);
//    //    [Benchmark] public void AutoMapper() _autoMapper.Map<SpotifyAlbum>(_spotifyAlbumDto);
//    //    [Benchmark] public void ManualMapping() _spotifyAlbumDto.Map();
//    //    [Benchmark] public void Mapster() _spotifyAlbumDto.Adapt<SpotifyAlbum>();
//    //    [Benchmark(Baseline = true)] public void Mapperly() _mapperlyMapper.Map(_spotifyAlbumDto);

//    //    [Benchmark(Baseline = true)] public List<City> MapWithManualMapping() => _cityDtos.MapTo();
//    //    [Benchmark] public List<City> MapWithAutoMapper() => AutoMapperConfigurator.AutoMapper.Map<List<City>>(_cityDtos);
//    //    [Benchmark] public List<City> MapWithMapster() => _cityDtos.Adapt<List<City>>();
//    //    [Benchmark] public List<City> MapWithExpressMapper() => ExpressMapper.Mapper.Map<List<CityDto>, List<City>>(_cityDtos);
//    //    [Benchmark] public List<City> MapWithTinyMapper() => TinyMapper.Map<List<City>>(_cityDtos);
//    //    [Benchmark] public List<City> MapWithAgileMapper() => AgileObjects.AgileMapper.Mapper.Map(_cityDtos).ToANew<List<City>>();

//    //    [Benchmark] public List<Customer> MapWithAutoMapper() AutoMapperConfigurator.AutoMapper.Map<List<Customer>>(_customerDtos);
//    //    [Benchmark] public List<Customer> MapWithMapster() _customerDtos.Adapt<List<Customer>>();
//    //    [Benchmark] public List<Customer> MapWithExpressMapper() ExpressMapper.Mapper.Map<List<CustomerDto>, List<Customer>>(_customerDtos);
//    //    [Benchmark] public List<Customer> MapWithTinyMapper() TinyMapper.Map<List<Customer>>(_customerDtos);
//    //    [Benchmark] public List<Customer> MapWithAgileMapper() AgileObjects.AgileMapper.Mapper.Map(_customerDtos).ToANew<List<Customer>>();

//    [Benchmark(Baseline = true)] public IEnumerable<PersonDto> MapWithImplicitOperator() => _person.ToDto();
//    [Benchmark] public IEnumerable<PersonDto> MapWithManualMapping() => _person.Select(x => new PersonDto { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, Birthday = x.Birthday }).ToList();
//    [Benchmark] public IEnumerable<PersonDto> MapWithAutoMapper() => AutoMapperConfigurator.AutoMapper.Map<IEnumerable<PersonDto>>(_person);
//    [Benchmark] public IEnumerable<PersonDto> MapWithMapster() => _person.Adapt<IEnumerable<PersonDto>>();
//    [Benchmark] public IEnumerable<PersonDto> MapWithExpressMapper() => ExpressMapper.Mapper.Map<IEnumerable<PersonModel>, IEnumerable<PersonDto>>(_person);
//    [Benchmark] public IEnumerable<PersonDto> MapWithTinyMapper() => TinyMapper.Map<IEnumerable<PersonDto>>(_person);
//    [Benchmark] public IEnumerable<PersonDto> MapWithAgileMapper() => AgileObjects.AgileMapper.Mapper.Map(_person).ToANew<IEnumerable<PersonDto>>();
//    [Benchmark] public IEnumerable<PersonDto> MapWithMapperly() => _mapperly.Map(_person);
//}

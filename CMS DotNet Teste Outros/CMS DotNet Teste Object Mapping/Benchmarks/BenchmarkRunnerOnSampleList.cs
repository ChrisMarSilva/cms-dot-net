//using BenchmarkDotNet.Attributes;
//using BenchmarkDotNet.Order;
//using CMS_DotNet_Teste_Object_Mapping.Dtos;
//using CMS_DotNet_Teste_Object_Mapping.Mappers;
//using CMS_DotNet_Teste_Object_Mapping.Models;
//using CMS_DotNet_Teste_Object_Mapping.Providers;
//using Mapster;
//using Nelibur.ObjectMapper;

//namespace CMS_DotNet_Teste_Object_Mapping.Benchmarks;

//[ThreadingDiagnoser]
//[MemoryDiagnoser]
//[Orderer(SummaryOrderPolicy.FastestToSlowest)]
//[RankColumn]
//[MarkdownExporter, HtmlExporter, CsvExporter, RPlotExporter]
//public class BenchmarkRunnerOnSampleList
//{
//    [Params(1, 10, 100, 1000, 10000, 100000, 1000000)]
//    public int CitysCount;

//    private List<CityDto> _cityDtos;

//    public BenchmarkRunnerOnSampleList()
//    {
//        AutoMapperConfigurator.SetUp();
//        ExpressMapperConfigurator.SetUp();
//        TinyMapperConfigurator.SetUp();
//    }

//    [GlobalSetup]
//    public void Setup()
//    {
//        _cityDtos = DataProvider.GetAddresses(CitysCount);
//    }

//    [Benchmark(Baseline = true)]
//    public List<City> MapWithManualMapping() => _cityDtos.MapTo();

//    [Benchmark]
//    public List<City> MapWithMapster() => _cityDtos.Adapt<List<City>>();

//    [Benchmark]
//    public List<City> MapWithAutoMapper() => AutoMapperConfigurator.AutoMapper.Map<List<City>>(_cityDtos);

//    //[Benchmark]
//    //public List<City> MapWithAgileMapper() => AgileObjects.AgileMapper.Mapper.Map(_cityDtos).ToANew<List<City>>();

//    [Benchmark]
//    public List<City> MapWithTinyMapper() => TinyMapper.Map<List<City>>(_cityDtos);

//    //[Benchmark]
//    //public List<City> MapWithExpressMapper() => ExpressMapper.Mapper.Map<List<CityDto>, List<City>>(_cityDtos);
//}

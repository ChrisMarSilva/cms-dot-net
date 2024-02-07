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
//public class BenchmarkRunnerOnComplexList
//{
//    [Params(1, 10, 100, 1000, 10000)]
//    public int CustomersCount;

//    private List<CustomerDto> _customerDtos;

//    public BenchmarkRunnerOnComplexList()
//    {
//        AutoMapperConfigurator.SetUp();
//        ExpressMapperConfigurator.SetUp();
//        TinyMapperConfigurator.SetUp();
//    }

//    [GlobalSetup]
//    public void Setup()
//    {
//        _customerDtos = DataProvider.GetData(CustomersCount);
//    }

//    [Benchmark(Baseline = true)]
//    public List<Customer> MapWithManualMapping()
//    {
//        return _customerDtos.MapTo();
//    }

//    [Benchmark]
//    public List<Customer> MapWithMapster()
//    {
//        return _customerDtos.Adapt<List<Customer>>();
//    }

//    [Benchmark]
//    public List<Customer> MapWithAutoMapper()
//    {
//        return AutoMapperConfigurator.AutoMapper.Map<List<Customer>>(_customerDtos);
//    }

//    //[Benchmark]
//    //public List<Customer> MapWithAgileMapper()
//    //{
//    //    return AgileObjects.AgileMapper.Mapper.Map(_customerDtos).ToANew<List<Customer>>();
//    //}

//    [Benchmark]
//    public List<Customer> MapWithTinyMapper()
//    {
//        return TinyMapper.Map<List<Customer>>(_customerDtos);
//    }

//    [Benchmark]
//    public List<Customer> MapWithExpressMapper()
//    {
//        return ExpressMapper.Mapper.Map<List<CustomerDto>, List<Customer>>(_customerDtos);
//    }
//}

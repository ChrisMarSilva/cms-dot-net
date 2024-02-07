using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using CMS_DotNet_Teste_Object_Mapping.Dtos;
using CMS_DotNet_Teste_Object_Mapping.Mappers;
using CMS_DotNet_Teste_Object_Mapping.Models;
using CMS_DotNet_Teste_Object_Mapping.Providers;
using Mapster;
using Nelibur.ObjectMapper;

namespace CMS_DotNet_Teste_Object_Mapping.Benchmarks;

[MemoryDiagnoser]
[ThreadingDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
[MarkdownExporter, HtmlExporter, CsvExporter, RPlotExporter]
public class BenchmarkConfiguration
{
    private MapperlyMapperConfigurator _mapperly = null!;
    private List<Person> _person; // Person _person = null!;

    [Params(1, 10, 1, 10, 100, 1_000, 10_000)] // 1, 10, 100, 1_000, 10_000
    public int NumberOfItems;

    public BenchmarkConfiguration()
    {
        AutoMapperConfigurator.SetUp();
        ExpressMapperConfigurator.SetUp();
        TinyMapperConfigurator.SetUp();
        _mapperly = new MapperlyMapperConfigurator();
    }

    [GlobalSetup]
    public void Setup()
    {
        _person = DataProvider.GetDataPersons(NumberOfItems);
    }

    [Benchmark(Baseline = true)] public List<PersonDto> MapWithImplicitOperator() => _person.ToDto(); 
    [Benchmark] public List<PersonDto> MapWithManualMapping() => _person.Select(x => new PersonDto { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, Birthday = x.Birthday }).ToList();
    [Benchmark] public List<PersonDto> MapWithAutoMapper() => AutoMapperConfigurator.AutoMapper.Map<List<PersonDto>>(_person);
    [Benchmark] public List<PersonDto> MapWithMapster() => _person.Adapt<List<PersonDto>>();
    [Benchmark] public List<PersonDto> MapWithExpressMapper() => ExpressMapper.Mapper.Map<List<Person>, List<PersonDto>>(_person);
    [Benchmark] public List<PersonDto> MapWithTinyMapper() => TinyMapper.Map<List<PersonDto>>(_person);
    [Benchmark] public List<PersonDto> MapWithAgileMapper() => AgileObjects.AgileMapper.Mapper.Map(_person).ToANew<List<PersonDto>>();
    [Benchmark] public List<PersonDto> MapWithMapperly() => _mapperly.Map(_person);
}

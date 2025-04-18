using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Order;
using CMS_DotNet_Teste_Object_Mapping.Dtos;
using CMS_DotNet_Teste_Object_Mapping.Mappers;
using CMS_DotNet_Teste_Object_Mapping.Models;
using CMS_DotNet_Teste_Object_Mapping.Providers;
using Mapster;

namespace CMS_DotNet_Teste_Object_Mapping.Benchmarks;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByParams)] // ByMethod, ByJob, ByParams, ByCategory
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class BenchmarkMappingPerson
{
    [Params(1, 10, 100, 1_000, 10_000, 100_000)] public int NumberOfItems { get; set; } // 1, 10, 100, 1_000, 10_000, 100_000  

    private ICollection<PersonModel>? _people;
    private MapperlyMapperConfigurator _mapperly;

    public BenchmarkMappingPerson()
    {
        AutoMapperConfigurator.SetUp();
        //Mapster don't need configuration
        //ExpressMapperConfigurator.SetUp();
        //TinyMapperConfigurator.SetUp();
        //AgileMapper don't need configuration
        _mapperly = new MapperlyMapperConfigurator();
    }

    [GlobalSetup]
    public void Setup()
    {
        _people = DataProviderBogus.GetDataPersons(NumberOfItems);
    }

    [Benchmark] public ICollection<PersonDto>? MapWithImplicitOperator() => _people?.ToDto();
    [Benchmark] public ICollection<PersonDto>? MapWithManualMapping() => _people?.Select(x => new PersonDto { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, Birthday = x.Birthday }).ToList();
    [Benchmark] public ICollection<PersonDto>? MapWithAutoMapper() => AutoMapperConfigurator.AutoMapper?.Map<ICollection<PersonDto>>(_people);
    [Benchmark] public ICollection<PersonDto>? MapWithMapster() => _people?.Adapt<ICollection<PersonDto>?>();
    //[Benchmark] public ICollection<PersonDto>? MapWithExpressMapper() => ExpressMapper.Mapper.Map<ICollection<PersonModel>?, ICollection<PersonDto>?>(_people);
    //[Benchmark] public ICollection<PersonDto>? MapWithTinyMapper() => TinyMapper.Map<ICollection<PersonDto>?>(_people);
    //[Benchmark] public ICollection<PersonDto>? MapWithAgileMapper() => AgileObjects.AgileMapper.Mapper.Map(_people).ToANew<ICollection<PersonDto>?>(); 
    [Benchmark] public ICollection<PersonDto>? MapWithMapperly() => _mapperly.Map(_people);
}

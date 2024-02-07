using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using CMS_DotNet_Teste_Object_Mapping.Dtos;
using CMS_DotNet_Teste_Object_Mapping.Models;
using CMS_DotNet_Teste_Object_Mapping.Mappers;
//using CMS_DotNet_Teste_Object_Mapping.Providers;
using AutoMapper;
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
    private IMapper _mapper = null!;
    private MapperlyMapper _mapperly = null!;
    private Person _person = null!;
    //    private CustomerDto _customerDto;

    //[Params(100)] // 1, 10, 100, 1_000, 10_000
    //public int NumberOfItems; //int NumberOfItems = 100_000;

    [GlobalSetup]
    public void Setup()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Person, PersonDto>();
        });

        _mapper = configuration.CreateMapper();

        _mapperly = new MapperlyMapper();

        ExpressMapper.Mapper.Register<Person, PersonDto>();

        TinyMapper.Bind<Person, PersonDto>();

        _person = new Person
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Birthday = DateTime.UtcNow
        };

        //        AutoMapperConfigurator.SetUp();
        //        ExpressMapperConfigurator.SetUp();
        //        TinyMapperConfigurator.SetUp();
        //        _customerDto = DataProvider.GetData(1)[0];
    }

    [Benchmark(Baseline = true)]
    //    public Customer MapWithManualMapping() => customerDto.MapTo();
    public void ImplicitOperator()
    {
        PersonDto personDto = _person;
    }

    [Benchmark]
    public void ImplicitOperator2()
    {
        PersonDto personDto = new PersonDto { Id = _person.Id, FirstName = _person.FirstName, LastName = _person.LastName, Birthday = _person.Birthday };
    }

    [Benchmark]
    //    public Customer MapWithAutoMapper() => AutoMapperConfigurator.AutoMapper.Map<Customer>(_customerDto);
    public void AutoMapper()
    {
        _ = _mapper.Map<PersonDto>(_person);
    }

    [Benchmark]
    //    public Customer MapWithMapster() => _customerDto.Adapt<Customer>();
    public void Mapster()
    {
        _ = _person.Adapt<PersonDto>();
    }

    [Benchmark(Description = "ExpressMapper")]
    //public Customer MapWithExpressMapper() => ExpressMapper.Mapper.Map<CustomerDto, Customer>(_customerDto);
    public void Expressmapper()
    {
        _ = ExpressMapper.Mapper.Map<Person, PersonDto>(_person);
    }

    [Benchmark(Description = "TinyMapper")]
    //public Customer MapWithTinyMapper() => TinyMapper.Map<Customer>(_customerDto);
    public void Tinymapper()
    {
        _ = TinyMapper.Map<PersonDto>(_person);
    }

    [Benchmark(Description = "AgileObjects")]
    //public Customer MapWithAgileMapper() => AgileObjects.AgileMapper.Mapper.Map(_customerDto).ToANew<Customer>();
    public void Agileobjects()
    {
        _ = AgileObjects.AgileMapper.Mapper.Map(_person).ToANew<PersonDto>();
    }

    [Benchmark(Description = "Mapperly")]
    public void Mapperly()
    {
        _ = _mapperly.Map(_person);
    }
}


//using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

//using AutoMapper;
using CMS_DotNet_Teste_Object_Mapping.Benchmarks;
//using CMS_DotNet_Teste_Object_Mapping.Dtos;
//using CMS_DotNet_Teste_Object_Mapping.Models;
//using CMS_DotNet_Teste_Object_Mapping.Mappers;
//using Mapster;
//using Nelibur.ObjectMapper;


namespace CMS_DotNet_Teste_Object_Mapping.Tests;

//var person = new Person
//{
//    Id = 1,
//    FirstName = "John",
//    LastName = "Doe",
//    Birthday = DateTime.UtcNow
//};

////AutoMapper 
//var configuration = new MapperConfiguration(cfg =>
//{
//    cfg.CreateMap<Person, PersonDto>();
//});
//var mapper = configuration.CreateMapper();
//var personAutoMapperDto = mapper.Map<PersonDto>(person);

////Mapster 
//var personMapsterDto = person.Adapt<PersonDto>();

//// ExpressMapper
//ExpressMapper.Mapper.Register<Person, PersonDto>();
//var personExpressMapperDto = ExpressMapper.Mapper.Map<Person, PersonDto>(person);

////Mapperly
//var mapperlyMapper = new MapperlyMapper();
//var personMapperlyMapperDto = mapperlyMapper.PersonToPersonDto(person);

////AgileObjects
//var personAgileObjectsDto = AgileObjects.AgileMapper.Mapper.Map(person).ToANew<PersonDto>();

////TinyMapper
//TinyMapper.Bind<Person, PersonDto>();
//var personTinyMapperDto = TinyMapper.Map<PersonDto>(person);

////Implicit operator
//PersonDto personImplicitOperatorDto = person;

//PersonDto personImplicitOperatorDto2 = new PersonDto
//{
//    Id = person.Id,
//    FirstName = person.FirstName,
//    LastName = person.LastName,
//    Birthday = person.Birthday
//};

//System.Console.WriteLine($"{personAutoMapperDto} - AutoMapper");
//System.Console.WriteLine($"{personMapsterDto} - Mapster");
//System.Console.WriteLine($"{personExpressMapperDto} - ExpressMapper");
//System.Console.WriteLine($"{personMapperlyDto} - Mapperly 1");
//System.Console.WriteLine($"{personMapperlyMapperDto} - Mapperly 2");
//System.Console.WriteLine($"{personAgileObjectsDto} - AgileObjects");
//System.Console.WriteLine($"{personTinyMapperDto} - TinyMapper");
//System.Console.WriteLine($"{personImplicitOperatorDto} - Implicit operator 2");
//System.Console.WriteLine($"{personImplicitOperatorDto2} - Implicit operator 2");
//Console.ReadLine();


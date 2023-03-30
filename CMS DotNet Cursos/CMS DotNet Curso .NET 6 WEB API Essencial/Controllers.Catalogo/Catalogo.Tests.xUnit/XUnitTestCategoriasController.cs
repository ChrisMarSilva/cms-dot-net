using AutoMapper;
using Catalogo.API.Controllers.v1;
using Catalogo.Data.Pagination;
using Catalogo.Data.Persistence;
using Catalogo.Data.Persistence.Interfaces;
using Catalogo.Data.Repositories;
using Catalogo.Data.Repositories.Interfaces;
using Catalogo.Domain.Dtos;
using Catalogo.Domain.Dtos.Mappings;
using Catalogo.Service;
using Catalogo.Service.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Tests.xUnit;

public class XUnitTestCategoriasController
{
    public static string connectionString = 
        "Server=localhost;Port=3306;Database=catalogo_api;Uid=root;Pwd=Chrs8723;Persist Security Info=False;Connect Timeout=300;Connection Reset=False;Max Pool Size=300;";
    public static DbContextOptions<AppDbContext> dbContextOptions { get; }
    protected readonly AppDbContext _context;
    private IProdutoRepository _prodRepository;
    private ICategoriaRepository _categRepository;
    private IUnitOfWork _uow; 
    private IMapper _mapper;
    private readonly ICategoriaService _categService;
    private CategoriasController _categController;

    static XUnitTestCategoriasController()
    {
        //var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
        //   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //   .Options;

        dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseMySql(connectionString,
             ServerVersion.AutoDetect(connectionString))
            .Options;
    }

    public XUnitTestCategoriasController()
    {
        _context = new AppDbContext(dbContextOptions);
        //_context.Database.EnsureCreated();        
        //var dbInitializer = new DBUnitTestsMockInitializer();
        //dbInitializer.SeedCategorias(_context);

        _prodRepository = new ProdutoRepository(_context);
        _categRepository = new CategoriaRepository(_context);

        _uow = new UnitOfWork(_context,_prodRepository, _categRepository);

        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        _mapper = config.CreateMapper();

        //var _categLogger = new Mock<ILogger<CategoriasController>>();

        _categService = new CategoriaService(_uow, _mapper);
        // var _categService = new Mock<ICategoriaService>();
        // _categService.Setup(_ => _.GetAllAsync()).ReturnsAsync(TodoMockData.GetTodos());

        _categController = new CategoriasController(_categService);
        // _categController = new CategoriasController(_categLogger.Object, _categService.Object);
    }

    //=======================================================================

    // testes unitários
    // Inicio dos testes : método GET

    [Fact]
    public async Task GetCategorias_Return_OkResult()
    {
        //Arrange  
        // var controller = new CategoriasController(_logger, _uow, _mapper);
        //var _categService = new Mock<ICategoriaService>();
        //// _categService.Setup(_ => _.GetAllAsync()).ReturnsAsync(TodoMockData.GetTodos());
        //var _categLogger = new Mock<ILogger<CategoriasController>>();
        //var categController = new CategoriasController(_categLogger.Object, _categService.Object);

        //Act  
        var categParams = new CategoriasParameters { };
        // var data = await controller.GetAll(categParams);
        var data = await _categController.GetAll(categParams);
        // var result = await _categoriasController.GetAll(categParams) as IActionResult;

        //Assert  
        // Assert.IsType<List<CategoriaResponseDTO>>(data.Value);
        // Assert.IsType<List<CategoriaResponseDTO>>(data);
        Assert.IsType<ObjectResult>(data);
    }

    [Fact]
    public async Task GetCategorias_Return_BadRequestResult()
    {
        //Arrange  
        // var controller = new CategoriasController(_logger, repository, _mapper);
        //var _categService = new Mock<ICategoriaService>();
        //// _categService.Setup(_ => _.GetAllAsync()).ReturnsAsync(TodoMockData.GetTodos());
        //var _categLogger = new Mock<ILogger<CategoriasController>>();
        //var categController = new CategoriasController(_categLogger.Object, _categService.Object);

        //Act  
        var categParams = new CategoriasParameters { PageNumber = 100 };
        // var data = await controller.GetAll(categParams);
        var data = await _categController.GetAll(categParams);

        //Assert  
        // Assert.IsType<BadRequestResult>(data.Result);
        //Assert.IsType<NotFoundResult>(data);
        Assert.IsType<NotFoundObjectResult>(data);
    }

    //GET retorna uma lista de objetos categoria
    //objetivo verificar se os dados esperados estão corretos
    [Fact]
    public async Task GetCategorias_MatchResult()
    {
        //Arrange  
        //var controller = new CategoriasController(_logger, repository, _mapper);
        //var _categService = new Mock<ICategoriaService>();
        //// _categService.Setup(_ => _.GetAllAsync()).ReturnsAsync(TodoMockData.GetTodos());
        //var _categLogger = new Mock<ILogger<CategoriasController>>();
        //var categController = new CategoriasController(_categLogger.Object, _categService.Object);

        //Act  
        var categParams = new CategoriasParameters { };
        //var data = await controller.GetAll(categParams);
        var data = await _categController.GetAll(categParams);

        //Assert  
        //Assert.IsType<List<CategoriaResponseDTO>>(data.Value);
        //var cat = data.Value.Should().BeAssignableTo<List<CategoriaResponseDTO>>().Subject;
        //Assert.IsType<List<CategoriaResponseDTO>>(data);
        Assert.IsType<ObjectResult>(data);
        var cat = data.Should().BeAssignableTo<List<CategoriaResponseDTO>>().Subject;

        Assert.Equal("Bebidas alterada", cat[0].Nome);
        Assert.Equal("bebidas21.jpg", cat[0].ImagemUrl);

        Assert.Equal("Sobremesas", cat[2].Nome);
        Assert.Equal("sobremesas.jpg", cat[2].ImagemUrl);
    }

    //-------------------------------------------------------------
    //GET - Retorna categoria por Id : Retorna objeto CategoriaDTO
    [Fact]
    public async Task GetCategoriaById_Return_OkResult()
    {
        //Arrange  
        //var controller = new CategoriasController(_logger, repository, _mapper);
        var catId = Guid.Parse("816bb968-99a7-4740-8995-6bad95b026e6"); //  2;
        //var _categService = new Mock<ICategoriaService>();
        //// _categService.Setup(_ => _.GetAllAsync()).ReturnsAsync(TodoMockData.GetTodos());
        //var _categLogger = new Mock<ILogger<CategoriasController>>();
        //var categController = new CategoriasController(_categLogger.Object, _categService.Object);

        //Act  
        //var data = await controller.GetById(catId);
        var data = await _categController.GetById(catId);

        //Assert  
        //Assert.IsType<CategoriaResponseDTO>(data.Value);
        Assert.IsType<OkObjectResult>(data);
    }

    //-------------------------------------------------------------
    //GET - Retorna Categoria por Id : NotFound
    [Fact]
    public async Task GetCategoriaById_Return_NotFoundResult()
    {
        //Arrange  
        //var controller = new CategoriasController(_logger, repository, _mapper);
        var catId = Guid.NewGuid(); //  Guid.Parse("d467b043-4bb3-4deb-ba83-540f52556dbd"); ///  9999;
        //var _categService = new Mock<ICategoriaService>();
        //// _categService.Setup(_ => _.GetAllAsync()).ReturnsAsync(TodoMockData.GetTodos());
        //var _categLogger = new Mock<ILogger<CategoriasController>>();
        //var categController = new CategoriasController(_categLogger.Object, _categService.Object);

        //Act  
        //var data = await controller.GetById(catId);
        var data = await _categController.GetById(catId);

        //Assert  
        //Assert.IsType<NotFoundResult>(data.Result);
        Assert.IsType<NotFoundResult>(data);
    }

    //-------------------------------------------------------------
    // POST - Incluir nova categoria - Obter CreatedResult
    [Fact]
    public async Task Post_Categoria_AddValidData_Return_CreatedResult()
    {
        //Arrange  
        //var controller = new CategoriasController(_logger, repository, _mapper);
        var cat = new CategoriaRequestDTO() { Nome = "Teste Unitario Inclusao", ImagemUrl = "testecatInclusao.jpg" };
        //var _categService = new Mock<ICategoriaService>();
        //// _categService.Setup(_ => _.GetAllAsync()).ReturnsAsync(TodoMockData.GetTodos());
        //var _categLogger = new Mock<ILogger<CategoriasController>>();
        //var categController = new CategoriasController(_categLogger.Object, _categService.Object);

        //Act  
        //var data = await controller.Post(cat);
        var data = await _categController.Post(cat);

        //Assert  
        // Assert.IsType<CreatedAtRouteResult>(data);
        Assert.IsType<CreatedAtActionResult>(data);
    }

    //-------------------------------------------------------------
    //PUT - Atualizar uma categoria existente com sucesso
    [Fact]
    public async Task Put_Categoria_Update_ValidData_Return_OkResult()
    {
        //Arrange  
        //var controller = new CategoriasController(_logger, repository, _mapper);
        var catId = Guid.Parse("816bb968-99a7-4740-8995-6bad95b026e6"); //  3;
        //var _categService = new Mock<ICategoriaService>();
        //// _categService.Setup(_ => _.Update()).ReturnsAsync(TodoMockData.GetTodos());
        //var _categLogger = new Mock<ILogger<CategoriasController>>();
        //var categController = new CategoriasController(_categLogger.Object, _categService.Object);

        //Act  
        // var existingPost = await controller.GetById(catId);
        //var result = existingPost.Value.Should().BeAssignableTo<CategoriaResponseDTO>().Subject;
        var existingPost = await _categController.GetById(catId);
        // var result = existingPost.Should().BeAssignableTo<CategoriaResponseDTO>().Subject;
        var result = existingPost.Should().BeAssignableTo<CategoriaResponseDTO>().Subject;

        var catDto = new CategoriaRequestDTO();
        //catDto.Id = catId;
        catDto.Nome = "Categoria Atualizada - Testes 1";
        catDto.ImagemUrl = result.ImagemUrl;

        //var updatedData = await controller.Update(catId, catDto);
        var updatedData = await _categController.Update(catId, catDto);

        //Assert  
        // Assert.IsType<OkResult>(updatedData);
        Assert.IsType<NoContentResult>(updatedData);
    }

    //-------------------------------------------------------------
    //Delete - Deleta categoria por id - Retorna CategoriaDTO
    [Fact]
    public async Task Delete_Categoria_Return_OkResult()
    {
        //Arrange  
        // var controller = new CategoriasController(_logger, repository, _mapper);
        var catId = Guid.Parse("b92e26d7-c8d0-476d-b21f-bb2ff35bfc36"); //  4;
        //var _categService = new Mock<ICategoriaService>();
        //// _categService.Setup(_ => _.Delete()).ReturnsAsync(TodoMockData.GetTodos());
        //var _categLogger = new Mock<ILogger<CategoriasController>>();
        //var categController = new CategoriasController(_categLogger.Object, _categService.Object);

        //Act  
        //var data = await controller.Delete(catId);
        var data = await _categController.Delete(catId);

        //Assert  
        //Assert.IsType<CategoriaResponseDTO>(data.Value);
        Assert.IsType<NoContentResult>(data);
    }

    //=======================================================================
}
using Project.Database.Repositories.Interfaces;
using Project.Service.Interfaces;

namespace Project.Service;

internal partial class Service : IService
{
    private readonly ILogger<Service> _logger;
    private readonly IQueryRepository _queryRepository;
    private readonly ICommandRepository _commandRepository;

    public Service(ILogger<Service> logger, IQueryRepository queryRepository, ICommandRepository commandRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _queryRepository = queryRepository ?? throw new ArgumentNullException(nameof(queryRepository));
        _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
    }
}

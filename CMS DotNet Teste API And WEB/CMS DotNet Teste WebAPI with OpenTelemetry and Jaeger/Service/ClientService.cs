using Project.Domain.Models;
using Project.Service.Interfaces;
using Project.ServiceBus.Commands;

namespace Project.Service;

internal partial class Service : IService
{
    public async Task ProcessarSolicAddClient(ClientAddCommandDto command)
    {
        var cancellationToken = new CancellationToken();

        var exist = await _queryRepository.ExistByNameClientAsync(command.Request.Name!, cancellationToken);
        if (!exist)
        {
            var model = new ClientModel(command.Request.Name!);

            var newModel = await _commandRepository.CreateClientAsync(model, cancellationToken);
            await _commandRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
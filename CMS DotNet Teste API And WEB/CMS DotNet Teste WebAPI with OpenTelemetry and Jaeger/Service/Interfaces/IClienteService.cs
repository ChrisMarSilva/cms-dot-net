using Project.ServiceBus.Commands;

namespace Project.Service.Interfaces;

public partial interface IService
{
    Task ProcessarSolicAddClient(ClientAddCommandDto command);
}

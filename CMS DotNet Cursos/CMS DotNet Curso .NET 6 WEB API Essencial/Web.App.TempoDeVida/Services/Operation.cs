using Web.App.TempoDeVida.Services.Interfaces;

namespace Web.App.TempoDeVida.Services
{
    public class Operation : IOperationTransient, IOperationScoped, IOperationSingleton
    {
        public string OperationId { get; }

        public Operation()
        {
            OperationId = Guid.NewGuid().ToString();
        }
    }
}

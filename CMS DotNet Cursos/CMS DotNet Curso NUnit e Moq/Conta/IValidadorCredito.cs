namespace Principal
{
    public interface IValidadorCredito
    {
        bool Validar(string cpf, decimal valor);
    }
}

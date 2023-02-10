namespace Principal
{
    public class ValidadorCreditoFake : IValidadorCredito
    {
        public bool Validar(string cpf, decimal valor)
        {
            return true;
        }
    }
}

namespace Principal
{
    public class ValidadorCredito : IValidadorCredito
    {

        public bool Validar(string cpf, decimal valor)
        {
            var statusSerasa = this.VerificarSituacaoSerasa(cpf);

            if (!statusSerasa)
                return false;
            
            var statusSPC = this.VerificarSituacaoSPC(cpf);

            if (!statusSPC)
                return false;

            return true; //  return (statusSerasa && statusSPC);
        }

        private bool VerificarSituacaoSerasa(string cpf)
        {
            // Chamada a um WebService para verificar situação Serasa
            return true;
        }

        private bool VerificarSituacaoSPC(string cpf)
        {
            // Chamada a um WebService para verificar situação SPC
            return true;
        }

    }
}

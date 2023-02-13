using System;
using System.Threading;

namespace Principal
{
    public class Conta
    {
        public string Cpf;
        public decimal Saldo;
        private IValidadorCredito _validadorCredito;

        public Conta(string cpf, decimal saldo)
        {
            this.Cpf = cpf;
            this.Saldo = saldo;
            // this._validadorCredito = new ValidadorCredito();
        }

        public Conta(string cpf, decimal saldo, IValidadorCredito validadorCredito)
        {
            this.Cpf = cpf;
            this.Saldo = saldo;
            this._validadorCredito = validadorCredito;
        }

        public void SetValidadorCredito(IValidadorCredito validadorCredito)
        {
            this._validadorCredito = validadorCredito;
        }

        public decimal GetSaldo()  => this.Saldo;

        public void Depoistar(decimal valor) => this.Saldo += valor;

        public bool Sacar(decimal valor)
        {
            if (valor == 0.00M)
                throw new ArgumentOutOfRangeException();

            if (valor <= 0.00M || valor > this.Saldo)
                return false;

            if (valor == 99)
                Thread.Sleep(1000);

            this.Saldo -= valor;
            return true;
        }

        public bool SolicitarEmprestimo(decimal valor)
        {
            bool resultado = false;

            var limite = this.Saldo * 10;

            if (valor >= limite)
                return resultado;

            try
            {
                resultado = this._validadorCredito.Validar(this.Cpf, valor);
            }
            catch (InvalidOperationException)
            {
                return resultado;
            }

            if (resultado)
                this.Depoistar(valor);

            return resultado;
        }

    }
}

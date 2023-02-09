using System;
using System.Threading;

namespace Principal
{
    public class Conta
    {
        public string Cpf;
        public decimal Saldo;

        public Conta(string cpf, decimal saldo)
        {
            this.Cpf = cpf;
            this.Saldo = saldo;
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

    }
}

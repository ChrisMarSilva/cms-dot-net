using System;

namespace Principal
{
    public class Conta
    {
        public string _cpf;
        public decimal _saldo;

        public Conta(string cpf, decimal saldo)
        {
            this._cpf = cpf;
            this._saldo = saldo;
        }

        public decimal GetSaldo() 
        { 
            return this._saldo;
        }

        public void Depoistar(decimal valor)
        {
            this._saldo += valor;
        }

        public bool Sacar(decimal valor)
        {
            if (this._saldo < valor)
                return false;

            this._saldo -= valor;
            return true;
        }

    }
}

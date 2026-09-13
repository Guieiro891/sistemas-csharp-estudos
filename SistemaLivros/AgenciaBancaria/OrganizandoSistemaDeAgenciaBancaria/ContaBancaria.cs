using System;

class ContaBancaria
{
    public int Numero { get; set; }
    public string Titular { get; set; }
    public decimal Saldo { get; set; }
    public Modelo Tipo { get; set; }
    public decimal Limite { get; set; }

    public ContaBancaria(int numero, string titular, decimal saldo, Modelo tipo, decimal limite)
    {
        Numero = numero;
        Titular = titular;
        Saldo = saldo; 
        Tipo = tipo; 
        Limite = limite;
    }
    public override string ToString()
    {
        return $" Numero: {Numero} | Titular: {Titular} | Saldo: R${Saldo:F2} | Tipo: {Tipo} | Limite: R${Limite:F2} ";
    }

}
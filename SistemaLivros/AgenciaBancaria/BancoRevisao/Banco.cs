using System;
using System.Collections.Generic;
using System.Linq;

class Banco
{
    private List<ContaBancaria> _contas = new List<ContaBancaria>();
    private int _numeroConta = 1;
    public void AbrirConta(string titular, Modelo tipo, decimal limite = 0)
    {
        if (string.IsNullOrEmpty(titular))
            throw new Exception("O nome do Titular não pode estar vazio.");

        if (tipo == Modelo.Poupanca && limite > 0 )
            throw new Exception("O Modelo poupança n pode ter Limite");

        if (tipo != Modelo.Corrente && tipo != Modelo.Poupanca)
            throw new Exception("Os modelos não podem ser diferentes");

        var contas = new ContaBancaria(_numeroConta, titular, 0, tipo ,limite );
        _numeroConta++;
        _contas.Add(contas);
    }

    public void Sacar(int numero, decimal valor)
    {
        var conta = _contas.FirstOrDefault(c => c.Numero == numero);
        if (conta == null)
            throw new Exception("Numero não encontrado");
        
        if (string.IsNullOrEmpty(conta.Titular))
            throw new Exception("Titular não encontrado.");
        
        if (valor <= 0)
            throw new Exception("Saldo deve ser maior do que zero.");

        if (conta.Saldo + conta.Limite < valor )
            throw new Exception("Saldo deve ser positivo para o saque");

        conta.Saldo -= valor;
    }

    public void Depositar(int numero, decimal valor)
    {
        var conta = _contas.FirstOrDefault(c => c.Numero == numero);
        
        if (conta == null)
            throw new Exception("Numero não encontrado.");
        
        if (string.IsNullOrEmpty(conta.Titular))
            throw new Exception("Titular não encontrado.");
        
        if (valor <= 0)
            throw new Exception("O deposito deve ser maior do que zero.");

        conta.Saldo += valor;

    }

    public void Transferencia (int numOrigem, int numDestino, decimal valor)
    {
        var origem = _contas.FirstOrDefault(c => c.Numero == numOrigem);
        var destino = _contas.FirstOrDefault(c => c.Numero == numDestino);

        if (origem == null)
            throw new Exception("Conta Origem não localizada.");
        
        if (destino == null)
            throw new Exception("Conta Destino não localizada.");

        if (string.IsNullOrEmpty(origem.Titular))
            throw new Exception("Titular de conta Origem não localizado.");

        if (string.IsNullOrEmpty(destino.Titular))
            throw new Exception("Conta Destino não localizada.");

        
        if (origem.Numero == destino.Numero)
            throw new Exception("Não é possivel transferir para a mesma conta.");

        if (valor <= 0)
            throw new Exception("O valor da trasferencia não  pode ser menor que zero.");
        
        if (origem.Saldo + origem.Limite < valor)
            throw new Exception("Saldo insulficiente para transferencia.");

        
        origem.Saldo -= valor;
        destino.Saldo += valor;
    }

    public List<Banco> ListarBancos()
    {
        return _contas;
    }
}

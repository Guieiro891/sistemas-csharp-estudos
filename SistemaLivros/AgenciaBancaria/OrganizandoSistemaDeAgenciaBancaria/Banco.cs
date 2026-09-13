using System;
using System.Collections.Generic;
using System.Linq;


class Banco
{
    private List<ContaBancaria> _contas = new List<ContaBancaria>();
    private int _proximoNumero = 1;
    public void AbrirConta(string titular, Modelo tipo, decimal limite = 0)
    {
        if(string.IsNullOrEmpty(titular))
            throw new Exception("Titular é obrigátorio.");
        
        if(tipo == Modelo.Poupanca && limite > 0)
            throw new Exception("Conta Poupança não pode ter limite.");
        
        if(tipo != Modelo.Corrente && tipo != Modelo.Poupanca)
            throw new Exception("Modelo de conta inválido.");

        var conta = new ContaBancaria(_proximoNumero, titular, 0, tipo, limite);
        _proximoNumero++;        
        _contas.Add(conta);

    }

    public void Sacar(int numero, decimal valor)
    {
        var conta = _contas.FirstOrDefault(c => c.Numero == numero);
        
        if (conta == null)
            throw new Exception("Conta não encontrada");

        if(string.IsNullOrEmpty(conta.Titular))
            throw new Exception("Titular inválido.");
        
        if(valor <= 0 )
            throw new Exception("Valor do saque deve ser maior do que zero.");
        
        if(conta.Saldo + conta.Limite < valor)
            throw new Exception("Saldo insulficiente.");

        conta.Saldo -= valor;
    }

    public void Depositar(int numero, decimal valor)
    {
         var conta = _contas.FirstOrDefault(c => c.Numero == numero);
         
         if(conta == null)
            throw new Exception("Conta nao encontrada.");

        if (valor <= 0)
            throw new Exception("Saldo deve ser maior que zero.");

        conta.Saldo += valor;    
    }

    public void Transferir(int numeroOrigem, int numeroDestino, decimal valor)
    {
        var origem = _contas.FirstOrDefault(c => c.Numero == numeroOrigem);
        
        var destino = _contas.FirstOrDefault(c => c.Numero == numeroDestino);
        
        if (origem == null)
            throw new Exception("Conta de origem não encontrada.");

        if (destino == null)
            throw new Exception("Conta de destino não encontrada.");
        
        if (origem.Numero == destino.Numero)
            throw new Exception("Não é possivel transferir para a mesma conta.");

        if (valor <= 0 )
            throw new Exception("Valor da Transferência deve ser maior que zero.");

        if(origem.Saldo + origem.Limite < valor)
            throw new Exception("Saldo insulficiente para transferência.");

        origem.Saldo -= valor;
        destino.Saldo += valor;
    }
    public List<ContaBancaria> ListarContas()
    {
        return _contas;
    }
}
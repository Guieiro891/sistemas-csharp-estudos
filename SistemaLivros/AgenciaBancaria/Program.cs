using System;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

enum Modelo
{
    Corrente,
    Poupanca
}
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
class Program
{
    static void Main()
    {
        var banco = new Banco();
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("=== BANCO DIGITAL ===");
            Console.WriteLine();
            Console.WriteLine("1 - Abrir conta ");
            Console.WriteLine("2 - Sacar ");
            Console.WriteLine("3 - Depositar");
            Console.WriteLine("4 - Transferir ");
            Console.WriteLine("5 - listar contas");
            Console.WriteLine("6 - Sair");
            Console.WriteLine();
            Console.WriteLine("Escolha uma opção: ");
            Console.WriteLine();

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                
                case 1:
                    try
                    {
                        Console.Write("Titular: ");
                        string titular = Console.ReadLine();

                        Console.Write("Tipo (Corrente/Poupança): ");
                        string tipoInput = Console.ReadLine();
                        Modelo tipo = Enum.Parse<Modelo>(tipoInput, true);

                        Console.Write("Limite (opcional, 0 para nenhum): ");
                        decimal limite = decimal.Parse(Console.ReadLine());

                        banco.AbrirConta(titular, tipo, limite);
                        Console.WriteLine("✅ Conta aberta com sucesso!");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"❌ Erro: {ex.Message}");
                    }
                    break;
                
                case 2:
                    try
                    {
                        Console.Write("Número da Conta: ");
                        int numero = int.Parse(Console.ReadLine());

                        Console.Write("Valor do Saque: ");
                        decimal valor = decimal.Parse(Console.ReadLine());

                        banco.Sacar(numero, valor);
                        Console.WriteLine("✅ Saque realizado com sucesso.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Erro: {ex.Message}");
                    }
                    break;

                case 3:
                    try
                    {
                        Console.Write("Numero da conta: ");
                        int numero = int.Parse(Console.ReadLine());

                        Console.Write("Valor do depósito: ");
                        decimal valor = decimal.Parse(Console.ReadLine());

                        banco.Depositar(numero, valor);
                        Console.WriteLine("✅ Depósito realizado com sucesso.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Erro: {ex.Message}");
                    }
                    break;

                case 4:
                    try
                    {
                        Console.Write("Numero da Conta que ira efetuar a transferência: ");
                        int numeroOrigem = int.Parse(Console.ReadLine());

                        Console.Write("Valor que irá transferir: ");
                        decimal valor = decimal.Parse(Console.ReadLine());

                        Console.Write("Numero da conta que irá receber: ");
                        int numeroDestino = int.Parse(Console.ReadLine());

                        banco.Transferir(numeroOrigem, numeroDestino, valor);
                        Console.WriteLine("✅ Transferência realizada com sucesso.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($" ❌ Erro: {ex.Message}");
                    }
                    break;

                case 5:
                    var contas = banco.ListarContas();
                    if (contas.Count == 0)
                    {
                        Console.WriteLine("Nenhuma conta cadastrada.");
                    }
                    else
                    {
                        foreach (var c in contas)
                        {
                            Console.WriteLine(c);
                        }
                    }
                    break;

                case 6:
                    Console.WriteLine("Saindo....");
                    break;
                    
                default:
                    Console.WriteLine("Opção invalida!");
                    break;
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar... ");
            Console.ReadKey();

        } while (opcao != 6);

    }
}
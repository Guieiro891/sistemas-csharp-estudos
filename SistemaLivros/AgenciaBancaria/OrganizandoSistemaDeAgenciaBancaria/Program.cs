using System;

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
using System;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

class Program
{
    static void Main()
    {
        using (var db = new AppDbContext())
        {
            var gerenciador = new GerenciadorDePedidos(db);
            int opcao;

            do
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA DE PEDIDOS ===");
                Console.WriteLine("1 - Adicionar Cliente");
                Console.WriteLine("2 - Adicionar Produto");
                Console.WriteLine("3 - Criar Pedido: ");
                Console.WriteLine("4 - Listar Clientes");
                Console.WriteLine("5 - Listar Produtos");
                Console.WriteLine("6 - Adicionar Item ao Pedido: ");
                Console.WriteLine("7 - Calcular Total do Pedido");
                Console.WriteLine("8 - Sair");
                Console.Write("\nEscolha uma opção: ");

                opcao = int.Parse(Console.ReadLine());

                try
                {
                    switch (opcao)
                    {
                        case 1:
                            Console.Write("Nome: ");
                            string nome = Console.ReadLine();
                            Console.Write("Email: ");
                            string email = Console.ReadLine();
                            Console.Write("VIP (true/false): ");
                            bool vip = bool.Parse(Console.ReadLine());

                            var cliente = new Cliente { Nome = nome, Email = email, Vip = vip };
                            gerenciador.AdicionarCliente(cliente);
                            Console.WriteLine("✅ Cliente adicionado!");
                            break;

                        case 2:
                            Console.Write("Nome do Produto: ");
                            string nomeProduto = Console.ReadLine();
                            Console.Write("Preço: ");
                            decimal preco = decimal.Parse(Console.ReadLine());
                            Console.Write("Estoque: ");
                            int estoque = int.Parse(Console.ReadLine());

                            var produto = new Produto { Nome = nomeProduto, Preco = preco, Estoque = estoque };
                            gerenciador.AdicionarProduto(produto);
                            Console.WriteLine("✅ Produto adicionado!");
                            break;

                        case 3:
                            try
                            {
                                Console.Write("ID do Cliente: ");
                                int clienteId = int.Parse(Console.ReadLine());

                                var pedido = gerenciador.CriarPedido(clienteId);
                                Console.WriteLine($"✅ Pedido criado! ID: {pedido.Id}");
                            }
                            catch(Exception erro)
                            {
                                 Console.WriteLine($"❌ Erro: {erro.Message}");
                            }
                            break;
                        case 4:
                            Console.WriteLine("\n=== LISTA DE CLIENTES ===");
                            foreach (var c in gerenciador.ListarClientes())
                            {
                                Console.WriteLine($"ID: {c.Id} | Nome: {c.Nome} | Email: {c.Email} | VIP: {c.Vip}");
                            }
                            break;

                        case 5:
                            Console.WriteLine("\n=== LISTA DE PRODUTOS ===");
                            foreach (var p in gerenciador.ListarProdutos())
                            {
                                Console.WriteLine($"ID: {p.Id} | Nome: {p.Nome} | Preço: R${p.Preco:F2} | Estoque: {p.Estoque}");
                            }
                            break;

                        case 6:
                            try
                            {
                                Console.Write("ID do Pedido: ");
                                int pedidoId = int.Parse(Console.ReadLine());

                                Console.Write("ID do Produto: ");
                                int produtoId = int.Parse(Console.ReadLine());

                                Console.Write("Quantidade: ");
                                int quantidade = int.Parse(Console.ReadLine());

                                gerenciador.AdicionarItemAoPedido(pedidoId, produtoId, quantidade);
                                Console.WriteLine("✅ Item adicionado ao Pedido!");

                            }
                            catch(Exception erro)
                            {
                                Console.WriteLine($"❌ Erro: {erro.Message}");
                            }
                            break;

                        case 7:
                            try
                            {
                                Console.Write("ID do Pedido: ");
                                int pedidoId = int.Parse(Console.ReadLine());

                                decimal total = gerenciador.CalcularTotalDoPedido(pedidoId);
                                Console.WriteLine($"✅ Total do Pedido: R${total:F2}");

                            }
                            catch (Exception erro)
                            {
                                Console.WriteLine($"❌ Erro: {erro.Message}");
                            }
                            break;
                            
                        case 8:
                            Console.WriteLine("Saindo...");
                            break;

                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Erro: {ex.Message}");
                }

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();


            } while (opcao != 5);
        }
    }
}
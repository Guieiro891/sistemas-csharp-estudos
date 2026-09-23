using System;

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
                Console.WriteLine("3 - Criar Pedido");
                Console.WriteLine("4 - Adicionar Item ao Pedido");
                Console.WriteLine("5 - Calcular Total do Pedido");
                Console.WriteLine("6 - Listar Pedidos");
                Console.WriteLine("7 - Avançar Status do Pedido");
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
                            Console.WriteLine("Cliente adicionado!");
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
                            Console.WriteLine("Produto adicionado!");
                            break;

                        case 3:
                            Console.Write("ID do Cliente: ");
                            int clienteId = int.Parse(Console.ReadLine());

                            var pedido = gerenciador.CriarPedido(clienteId);
                            Console.WriteLine($"Pedido criado! ID: {pedido.Id}");
                            break;

                        case 4:
                            Console.Write("ID do Pedido: ");
                            int pedidoId = int.Parse(Console.ReadLine());
                            Console.Write("ID do Produto: ");
                            int produtoId = int.Parse(Console.ReadLine());
                            Console.Write("Quantidade: ");
                            int quantidade = int.Parse(Console.ReadLine());

                            gerenciador.AdicionarItemAoPedido(pedidoId, produtoId, quantidade);
                            Console.WriteLine("Item adicionado ao pedido!");
                            break;

                        case 5:
                            Console.Write("ID do Pedido: ");
                            int pedidoIdTotal = int.Parse(Console.ReadLine());

                            decimal total = gerenciador.CalcularTotalDoPedido(pedidoIdTotal);
                            Console.WriteLine($"Total do Pedido: R${total:F2}");
                            break;

                        case 6:
                            var pedidos = gerenciador.ListarPedidos();
                            if (pedidos.Count == 0)
                            {
                                Console.WriteLine("Nenhum pedido cadastrado.");
                            }
                            else
                            {
                                foreach (var p in pedidos)
                                {
                                    Console.WriteLine($"\nID: {p.Id} | ClienteId: {p.ClienteId} | Data: {p.Data} | Status: {p.Status}");
                                    foreach (var item in p.Itens)
                                    {
                                        Console.WriteLine($"   Item: ProdutoId {item.ProdutoId} | Quantidade: {item.Quantidade} | Preço: R${item.PrecoUnitario:F2}");
                                    }
                                }
                            }
                            break;

                        case 7:
                            Console.Write("ID do Pedido: ");
                            int pedidoIdStatus = int.Parse(Console.ReadLine());

                            gerenciador.AvancarStatusPedido(pedidoIdStatus);
                            Console.WriteLine("Status avançado!");
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
                    Console.WriteLine($"Erro: {ex.Message}");
                }

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();

            } while (opcao != 8);
        }
    }
}
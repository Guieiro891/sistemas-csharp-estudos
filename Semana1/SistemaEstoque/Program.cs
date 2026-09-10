using System;
using System.Collections.Generic;
using System.Linq;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }

    public Produto(int id, string nome, decimal preco, int quantidade)
    {
        Id = id;
        Nome = nome;
        Preco = preco;
        Quantidade = quantidade;
    }

    public override string ToString()
    {
        return $"{Id} - {Nome} | R${Preco:F2} | {Quantidade} un";
    }
}

public class Estoque
{
    private List<Produto> _produtos = new List<Produto>();
    private int _proximoId = 1; // <--- CORRIGIDO: nome certo

    public void Adicionar(Produto produto)
    {
        bool existe = _produtos.Any(p => p.Nome.Equals(produto.Nome, StringComparison.OrdinalIgnoreCase));
        if (existe)
        {
            throw new Exception($"Produto '{produto.Nome}' ja existe no estoque.");
        }

        produto.Id = _proximoId++; // <--- AGORA FUNCIONA
        _produtos.Add(produto);
    }

    public void Remover(string nome)
    {
        var produto = BuscarPorNome(nome);
        if (produto == null)
        {
            throw new Exception($"Produto '{nome}' nao encontrado.");
        }

        _produtos.Remove(produto);
    }

    public Produto BuscarPorNome(string nome)
    {
        return _produtos.FirstOrDefault(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
    }

    public decimal CalcularValorTotal()
    {
        return _produtos.Sum(p => p.Preco * p.Quantidade);
    }

    public List<Produto> ListarTodos()
    {
        return _produtos;
    }

    public List<Produto> BuscarPorPrecoMinimo(decimal precoMinimo)
    {
        return _produtos.Where(p => p.Preco >= precoMinimo).ToList();
    }
}

class Program
{
    static void Main()
    {
        var estoque = new Estoque();

        try
        {
            estoque.Adicionar(new Produto(0, "Teclado", 150m, 10));
            estoque.Adicionar(new Produto(0, "Mouse", 80m, 20));
            estoque.Adicionar(new Produto(0, "Monitor", 1200m, 5));

            Console.WriteLine("=== LISTA DE PRODUTOS ===");
            foreach (var p in estoque.ListarTodos())
            {
                Console.WriteLine(p);
            }

            Console.WriteLine($"\nValor total do estoque: R${estoque.CalcularValorTotal():F2}");

            Console.WriteLine("\n=== BUSCAR POR PRECO >= 100 ===");
            var caros = estoque.BuscarPorPrecoMinimo(100m);
            foreach (var p in caros)
            {
                Console.WriteLine(p);
            }

            Console.WriteLine("\n=== REMOVENDO 'Mouse' ===");
            estoque.Remover("Mouse");

            Console.WriteLine("\n=== LISTA ATUALIZADA ===");
            foreach (var p in estoque.ListarTodos())
            {
                Console.WriteLine(p);
            }

            Console.WriteLine("\n=== TESTE DE ERRO (Produto Duplicado) ===");
            try // <--- CORRIGIDO: try antes do catch
            {
                estoque.Adicionar(new Produto(0, "Teclado", 150m, 5));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n ERRO: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n ERRO GERAL: {ex.Message}");
        }

        Console.WriteLine("\n=== TESTE DE BUSCA ===");
        var buscado = estoque.BuscarPorNome("monitor");
        Console.WriteLine(buscado != null ? $"Encontrado: {buscado}" : "Nao Encontrado");

        Console.WriteLine("\nPressione qualquer tecla para sair....");
        Console.ReadKey();
    }
}
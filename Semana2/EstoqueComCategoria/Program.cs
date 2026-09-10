using System;
using System.Collections.Generic;
using System.Linq;

public class  Categoria
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public Categoria(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
    public override string ToString()
    {
        return $" {Id} - {Nome}";
    }
}
public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
    public int CategoriaId { get; set;}

    public Produto( int id, string nome, decimal preco, int quantidade, int categoriaId)
    {
        Id = id;
        Nome = nome;
        Preco = preco;
        Quantidade = quantidade;
        CategoriaId = categoriaId;
    }
    public override string ToString()
    {
        return $"{Id} - {Nome} | R$ {Preco:2F} | {Quantidade} un | CategoriaId: {CategoriaId}";
    }
}
public class Estoque
{
    private List<Produto> _produtos = new List<Produto>();
    private List<Categoria> _categorias = new List<Categoria>();
    private int _proximoIdProduto = 1;
    private int _proximoIdCategoria = 1;

    public void AdicionarCategoria(Categoria categoria)
    {
        bool existe = _categorias.Any(p => p.Nome.Equals(categoria.Nome, StringComparison.OrdinalIgnoreCase));
        if (existe)
        {
            throw new Exception("Ja existe essa categoria.");
        }              
        categoria.Id = _proximoIdCategoria++;
        _categorias.Add(categoria);

    }
    public Categoria BuscarCategoriaPorNome(string nome)
    {
        return _categorias.FirstOrDefault(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        
    }
    public List<Categoria> ListarCategorias()
    {
        return _categorias;
    }
    public void AdicionarProduto(Produto produto)
    {
        bool categoriaExiste = _categorias.Any(c => c.Id == produto.CategoriaId);
        if (!categoriaExiste)
        {
        throw new Exception("Categoria nao encontrado");
        }

        bool existe = _produtos.Any(p => p.Nome == produto.Nome);
        if (existe)
        {
            throw new Exception("Produto ja encontrado.");
        }
        produto.Id = _proximoIdProduto++;
        _produtos.Add(produto);

    }
    public void RemoverProduto(string nome)
    {
        var produto = BuscarProdutoPorNome(nome);
        if ( produto == null)
        {
            throw new Exception("Produto nao encontrado");
        }
        _produtos.Remove(produto);
    }
    public Produto BuscarProdutoPorNome(string nome)
    {
        return _produtos.FirstOrDefault(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
    }
    public List<Produto> ListarProduto()
    {
        return _produtos;
    }
    public List<string> ListarProdutosComCategoria()
    {
        var resultado = _produtos.Join(
            _categorias,
            produto => produto.CategoriaId,
            categoria => categoria.Id,
            (produto, categoria) =>
                $"{produto.Nome} - Categorira: {categoria.Nome}"
        ).ToList();
        return resultado;
    }
    public Dictionary<string, decimal> CalcularTotalPorCategoria()
    {
        var resultado = _produtos
        .Join(_categorias,
            produto => produto.CategoriaId,
            categoria => categoria.Id,
            (produto, categoria) => new {produto, categoria}
        )
        .GroupBy(x => x.categoria.Nome)
        .Select(grupo => new
        {
            CategoriraNome = grupo.Key,
            Total = grupo.Sum(x => x.produto.Preco * x.produto.Quantidade)
        })
        .ToDictionary(x => x.CategoriraNome, x => x.Total);
        return resultado;

    }
    public List<Produto> PaginarProdutos(int pagina, int itensPorPagina)
    {
        return _produtos
            .Skip((pagina - 1) * itensPorPagina)
            .Take(itensPorPagina)
            .ToList();
    }
    public List<Produto> FiltrarPorPrecoECategoria(decimal precoMinimo, string nomeCategoria)
    {
        var resultado = _produtos
            .Join(_categorias,
                produto => produto.CategoriaId,
                categoria => categoria.Id,
                (produto, categoria) => new {produto, categoria})
            .Where(x => x.produto.Preco >= precoMinimo
                    && x.categoria.Nome.Equals(nomeCategoria, StringComparison.OrdinalIgnoreCase))
            .Select(x => x.produto)
            .ToList();

        return resultado;
    }
}
class Program
{
    static void Main()
    {
        var estoque = new Estoque();
        try
        {
            estoque.AdicionarCategoria(new Categoria(0, "Eletronicos"));
            estoque.AdicionarCategoria(new Categoria(0, "Informatica"));
            estoque.AdicionarCategoria(new Categoria(0, "Acessorios"));

            estoque.AdicionarProduto(new Produto(0, "Teclado", 150m, 10, 2));
            estoque.AdicionarProduto(new Produto(0, "Mouse", 80m, 20, 2));
            estoque.AdicionarProduto(new Produto(0, "Monitor", 1200m, 5, 2));
            estoque.AdicionarProduto(new Produto(0, "Fone", 200m, 8, 1));
            estoque.AdicionarProduto(new Produto(0, "Cabo USB", 30m, 15, 3));

            Console.WriteLine("=== LISTA DE PRODUTOS ===");
            foreach (var p in estoque.ListarProduto())
            {
                Console.WriteLine(p);
            }

            Console.WriteLine("\n=== LISTA DE CATEGORIAS ===");
            foreach(var c in estoque.ListarCategorias())
            {
                Console.WriteLine(c);
            }

            Console.WriteLine("\n=== PRODUTOS COM CATEGORIA ===");
            
            var produtosComCategoria = estoque.ListarProdutosComCategoria();
            foreach (var item in produtosComCategoria)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\n=== TOTAL POR CATEGORIA (GROUPBY) ===");
            var totais = estoque.CalcularTotalPorCategoria();
            foreach (var kvp in totais)
            {
                Console.WriteLine($"{kvp.Key}: R${kvp.Value:F2}");
            }

            Console.WriteLine("\n=== PAGINACAO (Pagina1, 3 itens) ===");
            var totais = estoque.CalcularTotalPorCategoria();
            foreach (var p in pagina1)
            {
                Console.WriteLine(p);
            }

            Console.WriteLine("\n=== FILTRO: Preco >= 100 e Categoria = Informatica ===");

            var filtrados = estoque.FiltrarPorPrecoECategoria(100m, "Informatica");
            foreach(var p in filtrados)
            {
                Console.WriteLine(p);
            }

            Console.WriteLine("\n==== REMOVENDO 'Mouse' ===");
            estoque.RemoverProduto("Mouse");
            Console.WriteLine("Mouse removido com sucesso!");

            Console.WriteLine("\n=== LISTA DE PRODUTOS APOS REMOCAO ===");
            foreach(var p in estoque.ListarProduto())
            {
                Console.WriteLine(p);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n ERRO: {ex.Message}");
        }

        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();

    }
}
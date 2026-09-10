using System;
using System.Collections.Generic;
using System.Linq;

public class Categoria
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
        return $"{Id} - {Nome}";
    }
}

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
    public int CategoriaId { get; set; }

    public Produto(int id, string nome, decimal preco, int quantidade, int categoriaId)
    {
        Id = id;
        Nome = nome;
        Preco = preco;
        Quantidade = quantidade;
        CategoriaId = categoriaId;
    }

    public override string ToString()
    {
        return $"{Id} - {Nome} | R${Preco:F2} | {Quantidade} un | CategoriaId: {CategoriaId}";
    }
}

public class Estoque
{
    private List<Produto> _produtos = new List<Produto>();
    private List<Categoria> _categorias = new List<Categoria>();
    private int _proximoIdProduto = 1;
    private int _proximoIdCategoria = 1;

    // ==========================================
    // MÉTODOS DE CATEGORIA
    // ==========================================
    public void AdicionarCategoria(Categoria categoria)
    {
        bool existe = _categorias.Any(c => c.Nome.Equals(categoria.Nome, StringComparison.OrdinalIgnoreCase));
        if (existe)
            throw new Exception("Categoria já existe.");

        categoria.Id = _proximoIdCategoria++;
        _categorias.Add(categoria);
    }

    public Categoria BuscarCategoriaPorNome(string nome)
    {
        return _categorias.FirstOrDefault(c => c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
    }

    public List<Categoria> ListarCategorias()
    {
        return _categorias;
    }

    // ==========================================
    // MÉTODOS DE PRODUTO
    // ==========================================
    public void AdicionarProduto(Produto produto)
    {
        bool categoriaExiste = _categorias.Any(c => c.Id == produto.CategoriaId);
        if (!categoriaExiste)
            throw new Exception("Categoria não encontrada.");

        bool existe = _produtos.Any(p => p.Nome.Equals(produto.Nome, StringComparison.OrdinalIgnoreCase));
        if (existe)
            throw new Exception("Produto já existe.");

        produto.Id = _proximoIdProduto++;
        _produtos.Add(produto);
    }

    public Produto BuscarProdutoPorNome(string nome)
    {
        return _produtos.FirstOrDefault(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
    }

    public void RemoverProduto(string nome)
    {
        var produto = BuscarProdutoPorNome(nome);
        if (produto == null)
            throw new Exception("Produto não encontrado.");

        _produtos.Remove(produto);
    }

    public List<Produto> ListarProdutos()
    {
        return _produtos;
    }

    // ==========================================
    // FILTRO - PROMOÇÃO
    // ==========================================
    public List<Produto> FiltrarProdutosEmPromocao()
    {
        return _produtos
            .Where(p => p.Preco < 100 && p.Quantidade > 5)
            .ToList();
    }

    public List<Produto> FiltrarProdutosEmPromocao(decimal precoMaximo, int quantidadeMinima)
    {
        return _produtos
            .Where(p => p.Preco < precoMaximo && p.Quantidade > quantidadeMinima)
            .ToList();
    }

    // ==========================================
    // OUTROS MÉTODOS (JOIN, GROUP BY, ETC.)
    // ==========================================
    public List<string> ListarProdutosComCategoria()
    {
        return _produtos.Join(
            _categorias,
            p => p.CategoriaId,
            c => c.Id,
            (p, c) => $"{p.Nome} - {c.Nome}"
        ).ToList();
    }

    public Dictionary<string, decimal> CalcularTotalPorCategoria()
    {
        return _produtos
            .Join(_categorias,
                p => p.CategoriaId,
                c => c.Id,
                (p, c) => new { Produto = p, Categoria = c })
            .GroupBy(x => x.Categoria.Nome)
            .Select(g => new
            {
                Categoria = g.Key,
                Total = g.Sum(x => x.Produto.Preco * x.Produto.Quantidade)
            })
            .ToDictionary(x => x.Categoria, x => x.Total);
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
        return _produtos
            .Join(_categorias,
                p => p.CategoriaId,
                c => c.Id,
                (p, c) => new { Produto = p, Categoria = c })
            .Where(x => x.Produto.Preco >= precoMinimo &&
                        x.Categoria.Nome.Equals(nomeCategoria, StringComparison.OrdinalIgnoreCase))
            .Select(x => x.Produto)
            .ToList();
    }
}

class Program
{
    static void Main()
    {
        var estoque = new Estoque();

        try
        {
            // ===== ADICIONAR CATEGORIAS =====
            estoque.AdicionarCategoria(new Categoria(0, "Eletronicos"));
            estoque.AdicionarCategoria(new Categoria(0, "Informatica"));
            estoque.AdicionarCategoria(new Categoria(0, "Acessorios"));

            // ===== ADICIONAR PRODUTOS =====
            estoque.AdicionarProduto(new Produto(0, "Teclado", 150m, 10, 2));
            estoque.AdicionarProduto(new Produto(0, "Mouse", 80m, 3, 2));
            estoque.AdicionarProduto(new Produto(0, "Monitor", 1200m, 5, 2));
            estoque.AdicionarProduto(new Produto(0, "Fone", 200m, 8, 1));
            estoque.AdicionarProduto(new Produto(0, "Cabo USB", 30m, 15, 3));
            estoque.AdicionarProduto(new Produto(0, "Luminaria", 80m, 8, 3));

            // ===== LISTAR PRODUTOS =====
            Console.WriteLine("=== LISTA DE PRODUTOS ===");
            foreach (var p in estoque.ListarProdutos())
            {
                Console.WriteLine(p);
            }

            // ===== TESTAR O FILTRO (SEM PARÂMETROS) =====
            Console.WriteLine("\n=== PRODUTOS EM PROMOÇÃO (REGRA FIXA) ===");
            var promocao = estoque.FiltrarProdutosEmPromocao();
            foreach (var p in promocao)
            {
                Console.WriteLine(p);
            }

            // ===== TESTAR O FILTRO (COM PARÂMETROS) =====
             Console.WriteLine("\n=== PRODUTOS EM PROMOÇÃO (PREÇO < 80 E QUANTIDADE > 5) ===");
            var promocaoFlexivel = estoque.FiltrarProdutosEmPromocao(80m, 5);
            foreach (var p in promocaoFlexivel)
            {
                Console.WriteLine(p);
            }
           
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n⚠️ ERRO: {ex.Message}");
        }

        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();
    }
}
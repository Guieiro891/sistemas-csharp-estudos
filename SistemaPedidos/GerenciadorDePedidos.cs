using System;
using System.Collections.Generic;
using System.Linq;

public class GerenciadorDePedidos
{
    private readonly AppDbContext _db;

    public GerenciadorDePedidos(AppDbContext db)
    {
        _db = db;
    }

    public void AdicionarCliente(Cliente cliente)
    {
        if (string.IsNullOrEmpty(cliente.Nome))
            throw new Exception("O nome não pode estar vazio.");

        if (string.IsNullOrEmpty(cliente.Email))
            throw new Exception("O email precisa ser válido.");

        bool emailJaExiste = _db.Clientes.Any(c => c.Email == cliente.Email);
        if (emailJaExiste)
            throw new Exception("Já existe um cliente com esse email.");

        _db.Clientes.Add(cliente);
        _db.SaveChanges();
    }

    public void AdicionarProduto(Produto produto)
    {
        if (string.IsNullOrEmpty(produto.Nome))
            throw new Exception("O nome não pode ser vazio.");

        if (produto.Preco <= 0)
            throw new Exception("O preço deve ser maior que zero.");

        if (produto.Estoque < 0)
            throw new Exception("O estoque não pode ser negativo.");

        bool produtoJaExiste = _db.Produtos.Any(p => p.Nome == produto.Nome); 
        if (produtoJaExiste)
            throw new Exception("Produto já existente.");

        _db.Produtos.Add(produto);
        _db.SaveChanges();
    }

    public List<Cliente> ListarClientes()
    {
        return _db.Clientes.ToList();
    }

    public List<Produto> ListarProdutos()
    {
        return _db.Produtos.ToList();
    }
    public Pedido CriarPedido(int clienteId)
    {
        bool clienteExiste = _db.Clientes.Any(c => c.Id == clienteId);
        if(!clienteExiste)
            throw new Exception("Cliente não encontrado.");
        
        var pedido = new Pedido
        {
            ClienteId = clienteId,
            Data = DateTime.Now,
            Status = StatusPedido.Criado,
            Itens = new List<ItemPedido>()
        };
        _db.Pedidos.Add(pedido);
        _db.SaveChanges();

        return pedido;
    }

    public void AdicionarItemAoPedido(int pedidoId, int produtoId, int quantidade)
    {
        bool pedidoExiste = _db.Pedidos.Any(p => p.Id == pedidoId);
        if (!pedidoExiste)
            throw new Exception("Pedido não encontrado");

        var produto = _db.Produtos.FirstOrDefault(p => p.Id == produtoId);
        if (produto == null)
            throw new Exception("Produto não encontrado.");

        if (quantidade <= 0)
            throw new Exception("A quantidade deve ser maior do que zero.");
        
        var novoItemPedido = new ItemPedido
        {
            PedidoId = pedidoId,
            ProdutoId = produtoId,
            Quantidade = quantidade,
            PrecoUnitario = produto.Preco
        };
        
        _db.ItensPedido.Add(novoItemPedido);
        _db.SaveChanges();
    }

    public decimal CalcularTotalDoPedido(int pedidoId)
    {
        bool pedidoExiste = _db.Pedidos.Any(p => p.Id == pedidoId);
        if(!pedidoExiste)
            throw new Exception("Pedido não encontrado.");
        
        var itens = _db.ItensPedido.Where(i => i.PedidoId == pedidoId).ToList();
        decimal total = itens.Sum(i => i.Quantidade * i.PrecoUnitario);
        return total;
    }
}
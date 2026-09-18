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
            throw new Exception ("O nome não pode estar vazio.");

        if (string.IsNullOrEmpty(cliente.Email))
            throw new Exception ("O email não pode estar vazio.");

        bool emailDuplicado = _db.Clientes.Any(c => c.Email == cliente.Email);
        if (emailDuplicado)
            throw new Exception (" Email duplicado, digite um email diferente");

        _db.Clientes.Add(cliente);
        _db.SaveChanges();
    }

    public void AdicionarProduto(Produto produto)
    {
        if (string.IsNullOrEmpty(produto.Nome))
            throw new Exception ("O Nome não pode está vazio.");

        if (produto.Preco <= 0)
            throw new Exception (" O Preço não pode ser zero e nem ter valores abaixo de zero.");

        if (produto.Estoque < 0)
            throw new Exception (" O Estoque não pode ter saldo negativo.");

        bool produtoDuplicado = _db.Produtos.Any( p => p.Nome == produto.Nome);
        if (produtoDuplicado)
            throw new Exception ("Produto duplicado, não pode ser cadastrado.");

        _db.Produtos.Add(produto);
        _db.SaveChanges();
    }

    public Pedido CriarPedido(int clienteId)
    {
        var clienteExiste = _db.Clientes.Any( c => c.Id == clienteId);
        if (!clienteExiste)
            throw new Exception ("Cliente não encontrado.");

        var novoPedido = new Pedido()
        {
            ClienteId = clienteId,
            Data = DateTime.Now,
            Status = StatusPedido.Criado,
            Itens = new List<ItemPedido>()
        };
        _db.Pedidos.Add(novoPedido);
        _db.SaveChanges();
        return novoPedido;
    }

     public void AdicionarItemAoPedido(int pedidoId, int produtoId, int quantidade)
    {
        var buscaPedido = _db.Pedidos.Any(p => p.Id == pedidoId);
        if (!buscaPedido)
            throw new Exception ("Pedido não encontrado.");

        var buscaProduto = _db.Produtos.FirstOrDefault( p => p.Id == produtoId);
        if (buscaProduto == null)
            throw new Exception (" Produto não encontrado.");

        if(quantidade <= 0)
            throw new Exception ("Quantidade deve ser maior do que zero.");

        var item = new ItemPedido()
        {
            PedidoId = pedidoId,
            ProdutoId = produtoId,
            Quantidade = quantidade,
            PrecoUnitario = buscaProduto.Preco
        };
        _db.ItensPedido.Add(item);
        _db.SaveChanges();

    }

    public decimal CalcularTotalDoPedido(int pedidoId)
    {
        var pedidoExiste = _db.Pedidos.Any(p => p.Id == pedidoId);
        if (!pedidoExiste)
            throw new Exception ("Pedido não encontrado.");

        var itens = _db.ItensPedido.Where(i => i.PedidoId == pedidoId).ToList();
       
        var total = itens.Sum(i => i.Quantidade * i.PrecoUnitario);
        return total;
         
    }
}
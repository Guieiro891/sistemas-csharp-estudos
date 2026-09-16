using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Identity.Client;

public enum StatusPedido
{
    Criado,
    Pago,
    Enviado,
    Entregue,
    Cancelado
}
public  class Pedido
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public DateTime Data { get; set; }
    public StatusPedido Status { get; set; }
    public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();

    public Pedido(){}

    public Pedido (int id, int clienteId, DateTime data, StatusPedido status, List<ItemPedido> itens)
    {
        Id = id;
        ClienteId = clienteId;
        Data = data;
        Status = status;
        Itens = itens;
    }       
}
public class ItemPedido
{
    public int Id {get; set; }
    public int PedidoId {get; set; }
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }

    public ItemPedido(){}
    public ItemPedido (int id, int pedidoId, int produtoId, int quantidade, decimal precoUnitario)
    {
        Id = id;
        PedidoId = pedidoId;
        ProdutoId = produtoId;
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
    }
}
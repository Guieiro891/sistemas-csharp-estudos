using System;
using System.Collections.Generic;
using System.Linq;

public class Pedido
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public DateTime Data { get; set; }
    public StatusPedido Status { get; set; }   
    public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();

    public  Pedido(){}
    public Pedido(int id, int clienteId, DateTime data, StatusPedido status, List<ItemPedido> itens)
    {
        Id = id;
        ClienteId = clienteId;
        Data = data;
        Status = status;
        Itens = itens;
        
    }
}
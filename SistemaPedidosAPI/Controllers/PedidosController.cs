using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]

public class PedidosControler : ControllerBase
{
    private readonly AppDbContext _context;
    public PedidosControler(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult Listar()
    {
        var buscar = _context.Pedidos 
            .Include(p => p.Itens)
            .ToList();
        return Ok(buscar);
    }

    [HttpGet("{id}")]
    public IActionResult BuscarPorId(int id)
    {
        var pedido = _context.Pedidos
            .Include(p => p.Itens)
            .FirstOrDefault(p => p.Id == id);

        if ( pedido == null)
            return NotFound();
        
        return Ok(pedido);
    }

    [HttpPost]
    public IActionResult Criar([FromBody] Pedido pedido)
    {
        pedido.Itens = new List<ItemPedido>();

        bool clienteExiste = _context.Clientes.Any(c => c.Id == pedido.ClienteId);
        if (!clienteExiste)
            return BadRequest("Cliente não encontrado.");

        pedido.Data = DateTime.Now;
        pedido.Status = StatusPedido.Criado;
        
        _context.Pedidos.Add(pedido);
        _context.SaveChanges();

        return CreatedAtAction(nameof(BuscarPorId), new {id = pedido.Id}, pedido);
    }
}
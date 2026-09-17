using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClientesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        var clientes = _context.Clientes.ToList();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public IActionResult BuscarPorId(int id)
    {
        var cliente = _context.Clientes.FirstOrDefault(c => c.Id == id);
        
        if(cliente == null)
            return NotFound();
        
        return Ok(cliente);
    }

    [HttpPost]
    public IActionResult Criar([FromBody] Cliente cliente)
    {
        if (string.IsNullOrEmpty(cliente.Nome))
            return BadRequest ("Nome não pode esta vazio.");
        
        if (string.IsNullOrEmpty(cliente.Email))
            return BadRequest ("Email não pode está vazio.");
        
        bool emailJaExiste = _context.Clientes.Any(c => c.Email == cliente.Email);
        if (emailJaExiste)
            return BadRequest ("Email já existe.");

            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        return CreatedAtAction(nameof(BuscarPorId), new { id = cliente.Id}, cliente);
    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(int id, [FromBody] Cliente cliente)
    {
        var clienteExistente = _context.Clientes.FirstOrDefault(c => c.Id == id);
        if (clienteExistente == null)
            return NotFound();

        clienteExistente.Nome = cliente.Nome;
        clienteExistente.Email = cliente.Email;
        clienteExistente.Vip = cliente.Vip;

        _context.SaveChanges();
        return Ok(clienteExistente);
    }

    [HttpDelete("{id}")]
    public IActionResult Remove(int id)
    {
        var cliente = _context.Clientes.FirstOrDefault(i => i.Id == id);
        if (cliente == null)
            return NotFound();
        _context.Clientes.Remove(cliente);
        _context.SaveChanges();
        return NoContent();
    }
}
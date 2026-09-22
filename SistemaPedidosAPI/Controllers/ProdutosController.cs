    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    [ApiController]
    [Route("api/[controller]")]

    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var produtos = _context.Produtos.ToList();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var produto = _context.Produtos.FirstOrDefault( p => p.Id == id);
            if (produto == null)
                return NotFound();
            return Ok(produto);
        }

        [HttpPost]
        public IActionResult ProdutoNovo([FromBody] Produto produto)
        {
            if(string.IsNullOrEmpty(produto.Nome))
                return BadRequest("Nome é Obrigatório.");

            if (produto.Preco <= 0)
                return BadRequest("Preço deve ser maior do que zero.");

            if (produto.Estoque < 0)
                return BadRequest("Estoque não pode ser negativo.");

            bool nomeExiste = _context.Produtos.Any(p => p.Nome == produto.Nome);
            if(nomeExiste)
                return BadRequest("Produto já cadastrado.");
            
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return CreatedAtAction(nameof(BuscarPorId), new {id = produto.Id}, produto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarProduto(int id, [FromBody] Produto produto)
        {
            var produtoExistente = _context.Produtos.FirstOrDefault(p => p.Id == id);
            if(produtoExistente == null)
                return NotFound();
            produtoExistente.Nome = produto.Nome;
            produtoExistente.Preco = produto.Preco;
            produtoExistente.Estoque = produto.Estoque;

            _context.SaveChanges();
            return Ok(produtoExistente);
        }    

        [HttpDelete("{id}")]
        public IActionResult Remove (int id)
    {
        var produtoRemover = _context.Produtos.FirstOrDefault(p => p.Id == id);
        if (produtoRemover == null)
            return NotFound();
        _context.Produtos.Remove(produtoRemover);
        _context.SaveChanges();
        return Ok(produtoRemover);
    }
    }

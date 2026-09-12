using System;
using System.Collections.Generic;
using System.Linq;

class GerenciadorDeLivros
{
    private List<Livro> _livros = new List<Livro>();
    private int _proximoId = 1;
    
    public List<Livro> ListarTodos()
    {
        return _livros;
    }

    public  Livro BuscarPorTitulo(string busca)
    {
        return _livros.FirstOrDefault(l => l.Titulo.Equals(busca, StringComparison.OrdinalIgnoreCase));
    }

    public void EmprestarLivro(string titulo)
    {
        var livro = BuscarPorTitulo(titulo);

        if(livro == null)
            throw new Exception("Livro não encontrado.");

        if (!livro.Disponivel)
            throw new Exception("Livro não disponível para empréstimo.");

        livro.Disponivel = false;
    }

     public void DevolverLivro(string titulo)
    {
        var livro =  BuscarPorTitulo(titulo);

        if(livro == null)
            throw new Exception("Livro não encontrado.");

        if (livro.Disponivel)
            throw new Exception("Livro já está disponivel.");

        livro.Disponivel = true;
    }
    public List<Livro> FiltrarPorGenero(string genero)
    {
        return _livros
            .Where(t => t.Genero.Equals(genero, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
    public void AdicionandoLivro(Livro livro)
    {
        if(string.IsNullOrEmpty(livro.Titulo))
            throw new Exception("O nome do titulo é obrigatorio.");
        if(livro.AnoPublicado <= 0  || livro.AnoPublicado > DateTime.Now.Year)
            throw new Exception("O ano de publicação não pode ser menor que zero, e nem maior q a data atual.");

        livro.Id = _proximoId;
        _proximoId++;
        _livros.Add(livro);
    }

}

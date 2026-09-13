using System;
using System.Collections.Generic;
using System.Linq;
class Livro
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public int AnoPublicado { get; set; }
    public string Genero { get; set; }
    public bool Disponivel { get; set;}

    public Livro (int id, string titulo, string autor, int anoPublicado, string genero, bool disponivel)
    {
        Id = id;
        Titulo = titulo;
        Autor = autor;
        AnoPublicado = anoPublicado;
        Genero = genero;
        Disponivel = disponivel;
    }
    public override string ToString()
    {
        bool status = Disponivel  ? "Disponivel " : "Emprestado";
        return $"Titulo: {Id} | {Titulo} |  {Titulo} - {Autor} - {AnoPublicado} - {Genero} - {status}";
    }
}

class GerenciadorDeLivros
{
    private List<Livro> _Livros = new List<Livro>();
    private int _proximoId = 1;
    public void AdicionandoLivro(Livro livro)
    {
        if(string.IsNullOrEmpty(livro.Titulo))
            throw new Exception("Titulo não pode ser vazio.");
        if(livro.AnoPublicado <= 0 || livro.AnoPublicado >= DateTime.Now)
            throw new Exception("O  ano do livro não pode ser menor que zero, e nem maior que a data atual.");

        livro.Id = _proximoId;
        _proximoId++;
        _Livros.Add(livro);

    }

    public List ListarTodos()
    {
        return _Livros;
    }
}

class Program
{
    static void Main()
    {
        var gerenciador = new GerenciadorDeLivros();

        Console.WriteLine("Adiconando Livros");
        try
        {
            var livro1 = new Livro("O Senhor dos Anéis", "Tolkien", 1954, "Fantasia", true);
            gerenciador.AdicionandoLivro(livro1);
        }
        catch (Exception erro)
        {
            Console.WriteLine($"Erro: {erro.Message}");
        }
    }
}
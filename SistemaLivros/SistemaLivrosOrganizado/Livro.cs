using System;
class Livro
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public int AnoPublicado { get; set; }
    public string Genero { get; set; }
    public bool Disponivel { get; set; }

    public Livro (string titulo, string autor, int anoPublicado, string genero, bool disponivel )
    {
      
        Titulo = titulo;
        Autor = autor;
        AnoPublicado = anoPublicado;
        Genero = genero;
        Disponivel = disponivel;
    }
    public override string ToString()
    {
        string status = Disponivel ? " Disponivel " : " Emprestado ";
        return $" {Titulo} - {Autor} - {AnoPublicado} - {Genero} - {status}";
    }
}

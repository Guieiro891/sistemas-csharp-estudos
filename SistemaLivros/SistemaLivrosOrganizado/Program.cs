using System;


class Program
{
    static void Main()
    {
        var gerenciador = new GerenciadorDeLivros();
        Console.WriteLine("\n=== CATALOGO DE LIVROS ===");
        try
        {
            
            var livro1 = new Livro("O Senhor dos Anéis", "Tolkien", 1954, "Fantasia", true);
            gerenciador.AdicionandoLivro(livro1);
            
        }
        catch(Exception erro)
        {
            Console.WriteLine("Erro ao inserir livro, tente novamente." + erro.Message);
        }
        try
        {
            var livro2 = new Livro("O Hobbit", "Tolkien", 1937, "Fantasia",true );
            gerenciador.AdicionandoLivro(livro2);
          
        }
        catch (Exception erro)
        {
            Console.WriteLine("Erro ao inserir livro, tente novamente." + erro.Message);
        }

        try
        {
            var livro3 = new Livro("Harry Potter e a Pedra Filosofal", "J K Rowling", 1997, "Fantasia", true);
            gerenciador.AdicionandoLivro(livro3);
        }
        catch (Exception erro)
        {
            Console.WriteLine("Erro ao inserir livro, tente novamente." + erro.Message);
        }

        try
        {
            var livro4 = new Livro("Fundação", "Isaac Asimov", 1951, "Ficção Científica", true);
            gerenciador.AdicionandoLivro(livro4);
        }
        catch (Exception erro)
        {
            Console.WriteLine("Erro ao inserir livro, tente novamente." + erro.Message);
        }

        try
        {
            var livro5 = new Livro("Dom Casmurro", "Machado de Assis", 1899, "Literatura Brasileira", true);
            gerenciador.AdicionandoLivro(livro5);
        }
        catch (Exception erro)
        {
            Console.WriteLine("Erro ao inserir livro, tente novamente." + erro.Message);
        }

        try
        {
            var livro6 = new Livro("O Assassinato do Expresso Oriente", "Agatha Christie", 1934, "Misterio",  true);
            gerenciador.AdicionandoLivro(livro6);
        }
        catch (Exception erro)
        {
            Console.WriteLine("Erro ao inserir livro, tente novamente." + erro.Message);
        }
        var todos = gerenciador.ListarTodos();
        foreach (var l in todos)
        {
            Console.WriteLine(l);
        }

        Console.WriteLine("\n=== BUSCA POR TÍTULO ===");
        var busca = gerenciador.BuscarPorTitulo("O Senhor dos Anéis");
        if (busca != null)
        {
            Console.WriteLine("Titulo encontrado:");
            Console.WriteLine(busca);
        }
        else
        {
            Console.WriteLine("Titulo não encontrado.");
        }

        var busca2 = gerenciador.BuscarPorTitulo("O Rei Arthur");
        if (busca2 != null)
        {
            Console.WriteLine("Titulo encontrado:");
            Console.WriteLine(busca2);
        }
        else
        {
            Console.WriteLine("Titulo não encontrado.");
        }

        Console.WriteLine("\nEmprestando Livro");
        Console.WriteLine("\n=== EMPRESTANDO LIVRO ===");
        try
        {
            gerenciador.EmprestarLivro("O Senhor dos Anéis");
            Console.WriteLine("✅ Livro emprestado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro: {ex.Message}");
        }
        
         Console.WriteLine("\n=== EMPRESTANDO LIVRO ===");
        try
        {
            gerenciador.EmprestarLivro("Harry Potter e a Pedra Filosofal");
            Console.WriteLine("✅ Livro emprestado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro: {ex.Message}");
        }
        Console.WriteLine("\n=== EMPRESTANDO LIVRO ===");
        try
        {
            gerenciador.EmprestarLivro("O Senhor dos Anéis");
            Console.WriteLine("✅ Livro emprestado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro: {ex.Message}");
        }

        Console.WriteLine("\n=== DEVOLUÇÃO DE  LIVRO ===");
        try
        {
            gerenciador.DevolverLivro("O Senhor dos Anéis");
            Console.WriteLine("✅ Livro devolvido com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro: {ex.Message}");
        }
        
        Console.WriteLine("\n=== FILTRO POR GÊNERO ===");
        var filtro = gerenciador.FiltrarPorGenero("Fantasia");
        foreach (var f in filtro)
        {
            Console.WriteLine(f);
        }
    }
}
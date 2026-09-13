using System;
class Program
{
    static void Main()
    {
        var gerenciador = new GerenciadorDeFuncionario();
        try
        {
            var f1 = new Funcionario(0, "Ana", "Desenvolvedora", 5000m, DateTime.Now);
            gerenciador.AdicionandoFuncionario(f1);
            Console.WriteLine("Funcionario adicionado com sucesso!");
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"Erro : {ex.Message}");
        }
        try
        {
            var f2 = new Funcionario(0, "Felipe", "Desenvolvedor", 5000m, DateTime.Now);
            gerenciador.AdicionandoFuncionario(f2);
            Console.WriteLine("Funcionario adicionado com sucesso!");
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"Erro : {ex.Message}");
        }

        try
        {
            var f3 = new Funcionario(0, "Maria", "Limpeza", 2500m, DateTime.Now);
            gerenciador.AdicionandoFuncionario(f3);
            Console.WriteLine("Funcionario adicionado com sucesso!");
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"Erro : {ex.Message}");
        }

        try
        {
            var f4 = new Funcionario(0, "Pedro", "Senior", 7000m, DateTime.Now);
            gerenciador.AdicionandoFuncionario(f4);
            Console.WriteLine("Funcionario adicionado com sucesso!");
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"Erro : {ex.Message}");
        }

        try
        {
            var f5 = new Funcionario(0, "João", "Desenvolvedor", 4000m, DateTime.Now);
            gerenciador.AdicionandoFuncionario(f5);
            Console.WriteLine("Funcionario adicionado com sucesso!");
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"Erro : {ex.Message}");
        }
        Console.WriteLine("Colocando os funcionarios em uma lista");
        var todos = gerenciador.ListarTodos();
        foreach (var f in todos)
        {
            Console.WriteLine(f);
        }
        Console.WriteLine();

        Console.WriteLine("Buscando funcionario pelo NOME: ");
        var busca = gerenciador.BuscarPorNome("Ana");
        if (busca != null)
        {
            Console.WriteLine("Funcionario encontrado: ");
            Console.WriteLine(busca);
        }
        else
        {
            Console.WriteLine("Funcionário não encontrado.");
        }
        Console.WriteLine();

        var busca2 = gerenciador.BuscarPorNome("Bruno");
        if (busca2 != null)
        {
            Console.WriteLine("Funcionário encontrado: ");
            Console.WriteLine(busca2);
        }
        else
        {
            Console.WriteLine("Funcionario não encontrado.");
        }
        Console.WriteLine();

        Console.WriteLine("Filtrando o salario dos funcionarios");
        var sM = gerenciador.FiltrarPorSalarioMinimo(5000m);
        if (sM.Count == 0)
        {
            Console.WriteLine("Nenhum funcionário encontrado com esse salario mínimo.");
        }
        else
        {
            foreach (var f in sM)
            {
                Console.WriteLine("Salario encontrado acima do minimo: ");
                Console.WriteLine(f);
            }
        }
    }
}
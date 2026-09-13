using System;
using System.Collections.Generic;
using System.Linq;


class GerenciadorDeFuncionario
{
    private List<Funcionario> _funcionarios = new List<Funcionario>();
    private int _proximoId = 1;

    public void AdicionandoFuncionario(Funcionario funcionario)
    {
        if(string.IsNullOrEmpty(funcionario.Nome))
            throw new Exception("Nome é obrigatorio.");

        if (funcionario.Salario <= 0)
            throw new Exception("O salario deve ser maior que zero.");
        funcionario.Id = _proximoId++;
        _funcionarios.Add(funcionario);   
    }
    public List<Funcionario> ListarTodos()
    {
        return _funcionarios;
    }
    public Funcionario BuscarPorNome(string nome)
    {
        return _funcionarios.FirstOrDefault(f => f.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
    }
    public List<Funcionario> FiltrarPorSalarioMinimo(decimal salarioMinimo)
    {
        return _funcionarios
        .Where(s => s.Salario >= salarioMinimo)
        .ToList();
        
    }
}

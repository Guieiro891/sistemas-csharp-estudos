using System;
class Funcionario
{
    public int Id{ get; set; }
    public string Nome { get; set; }
    public string Cargo { get; set; }
    public decimal Salario { get; set; }
    public DateTime DataAdimissao { get; set;}

    public Funcionario (int id, string nome, string cargo, decimal salario, DateTime dataAdimissao)
    {
        Id = id;
        Nome = nome;
        Cargo = cargo;
        Salario = salario;
        DataAdimissao = dataAdimissao;
    }
    public override string ToString()
    {
        return $"{Id} - {Nome} | {Cargo} | R${Salario:F2} | Admitido em {DataAdimissao:dd/MM/yyyy}";
    }
}
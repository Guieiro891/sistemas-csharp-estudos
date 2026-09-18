using System;

public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Vip { get; set; }

    public Cliente (){}
    public Cliente (int id, string nome, string email, bool vip)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Vip = vip;
    }
}
# SistemaPedidos (Console)

Aplicação Console para gerenciamento de pedidos, desenvolvida em C# / .NET com Entity Framework Core e SQL Server.

## Tecnologias

- C# / .NET
- Entity Framework Core
- SQL Server
- Programação Orientada a Objetos (POO)
- LINQ

## Funcionalidades

- Cadastro de Clientes (com validação de email único)
- Cadastro de Produtos (com validação de nome único)
- Criação de Pedidos
- Adição de Itens ao Pedido
- Cálculo do Total do Pedido

## Como rodar

1. Clone o repositório
2. Configure a string de conexão no `AppDbContext.cs`
3. Rode as migrations:
   ```bash
   dotnet ef database update
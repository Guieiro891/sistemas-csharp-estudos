# SistemaPedidosAPI

API REST para gerenciamento de pedidos, desenvolvida em C# / .NET com Entity Framework Core e SQL Server.

## 🛠️ Tecnologias

- C# / .NET
- ASP.NET Core (Web API)
- Entity Framework Core
- SQL Server
- Swagger

## 📋 Endpoints

### Clientes

| Método | Endpoint | O que faz |
|--------|----------|-----------|
| GET | `/api/clientes` | Lista todos os clientes |
| GET | `/api/clientes/{id}` | Busca um cliente por ID |
| POST | `/api/clientes` | Cria um novo cliente |
| PUT | `/api/clientes/{id}` | Atualiza um cliente |
| DELETE | `/api/clientes/{id}` | Remove um cliente |

## 🚀 Como rodar

1. Clone o repositório
2. Configure a string de conexão no `AppDbContext.cs`
3. Rode as migrations:
   ```bash
   dotnet ef database update
# SistemaPedidosAPI

API REST para gerenciamento de pedidos, desenvolvida em C# / .NET com Entity Framework Core e SQL Server.

## Tecnologias

- C# / .NET
- ASP.NET Core (Web API)
- Entity Framework Core
- SQL Server
- Swagger

## Endpoints

### Clientes

| Método | Endpoint | O que faz |
|--------|----------|-----------|
| GET | `/api/clientes` | Lista todos os clientes |
| GET | `/api/clientes/{id}` | Busca um cliente por ID |
| POST | `/api/clientes` | Cria um novo cliente |
| PUT | `/api/clientes/{id}` | Atualiza um cliente |
| DELETE | `/api/clientes/{id}` | Remove um cliente |

### Produtos

| Método | Endpoint | O que faz |
|--------|----------|-----------|
| GET | `/api/produtos` | Lista todos os produtos |
| GET | `/api/produtos/{id}` | Busca um produto por ID |
| POST | `/api/produtos` | Cria um novo produto |
| PUT | `/api/produtos/{id}` | Atualiza um produto |
| DELETE | `/api/produtos/{id}` | Remove um produto |

### Pedidos

| Método | Endpoint | O que faz |
|--------|----------|-----------|
| GET | `/api/pedidos` | Lista todos os pedidos (com itens) |
| GET | `/api/pedidos/{id}` | Busca um pedido por ID (com itens) |
| POST | `/api/pedidos` | Cria um novo pedido |

## Como rodar

1. Clone o repositório
2. Configure a string de conexão no `AppDbContext.cs`
3. Rode as migrations:
   ```bash
   dotnet ef database update
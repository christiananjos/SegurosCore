# Sistema de Seguros - Microserviços

Sistema para gerenciamento de propostas e contrataçoes de seguro usando arquitetura hexagonal e microserviços.

## Arquitetura

- **Arquitetura Hexagonal (Ports & Adapters)**
- **DDD (Domain-Driven Design)**
- **Clean Code e SOLID**
- **Microserviços** com comunicaçao REST
- **.NET 8**
- **SQL Server**
- **Docker**

## Estrutura

### PropostaHub
Gerencia propostas de seguro:
- Criar proposta
- Listar propostas
- Alterar status (Em Analise, Aprovada, Rejeitada)

### ContratacaoHub
Gerencia contrataçoes:
- Contratar proposta aprovada
- Verificar status da proposta no PropostaService

## Pre-requisitos

- .NET 8 SDK
- Docker Desktop
- SQL Server (ou usar o container)

## Como Executar

### Opçao 1: Docker Compose (Recomendado)

### Opçao 2: Executar Localmente

PropostaHub
cd PropostaHub/src/PropostaHub.Api
dotnet ef database update
dotnet run

ContratacaoHub (em outro terminal)
cd ContratacaoHub/src/ContratacaoHub.Api
dotnet ef database update
dotnet run

### Resumo dos Principios Aplicados
Arquitetura Hexagonal: Separaçao clara entre dominio (core) e infraestrutura (adapters) através de portas (interfaces).​

DDD: Entidades ricas com lógica de negocio, Value Objects, Agregados, e linguagem ubiqua.​

SOLID:

Single Responsibility: Cada Use Case tem uma unica responsabilidade​

Open/Closed: Extensível via interfaces (Ports)

Liskov Substitution: Adapters podem ser substituidos

Interface Segregation: Interfaces pequenas e especificas

Dependency Inversion: Dependencias apontam para abstraçoes (interfaces)​

Clean Code: Nomes claros, metodos pequenos, separaçao de responsabilidades.​

Microserviços: Comunicaçao via HTTP REST, cada serviço com seu proprio banco de dados.​

Testes: Testes unitarios com xUnit e Moq, cobertura dos casos de uso principais.​​

Docker: Containerizaçao completa com Docker Compose para orquestraçao.​​

Migrations: Entity Framework Migrations para versionamento do banco.​

Essa aplicaçao demonstra uma implementaçao completa e prática de arquitetura hexagonal com microserviços em .NET 8, seguindo as melhores práticas de engenharia de software!


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


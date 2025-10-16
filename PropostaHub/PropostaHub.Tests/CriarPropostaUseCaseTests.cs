using Moq;
using PropostaHub.Core.Domain.Entities;
using PropostaHub.Core.Ports;
using PropostaHub.Core.UseCases;

namespace PropostaHub.Tests;

public class CriarPropostaUseCaseTests
{
    private readonly Mock<IPropostaRepository> _repositoryMock;
    private readonly CriarPropostaUseCase _useCase;

    public CriarPropostaUseCaseTests()
    {
        _repositoryMock = new Mock<IPropostaRepository>();
        _useCase = new CriarPropostaUseCase(_repositoryMock.Object);
    }

    [Fact]
    public async Task ExecutarAsync_ComDadosValidos_DeveCriarProposta()
    {
        var nomeCliente = "João Silva";
        var cpf = "12345678901";
        var valorSeguro = 1000m;

        _repositoryMock
            .Setup(r => r.AdicionarAsync(It.IsAny<Proposta>()))
            .ReturnsAsync((Proposta p) => p);

        var resultado = await _useCase.ExecutarAsync(nomeCliente, cpf, valorSeguro);

        Assert.NotNull(resultado);
        Assert.Equal(nomeCliente, resultado.NomeCliente);
        Assert.Equal(cpf, resultado.Cpf);
        Assert.Equal(valorSeguro, resultado.ValorSeguro);

        _repositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Proposta>()), Times.Once);
    }

    [Fact]
    public async Task ExecutarAsync_ComNomeVazio_DeveLancarExcecao()
    {
        var nomeCliente = "";
        var cpf = "12345678901";
        var valorSeguro = 1000m;

        await Assert.ThrowsAsync<ArgumentException>(() =>
                    _useCase.ExecutarAsync(nomeCliente, cpf, valorSeguro));

        _repositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Proposta>()), Times.Never);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public async Task ExecutarAsync_ComValorInvalido_DeveLancarExcecao(decimal valor)
    {
        var nomeCliente = "João Silva";
        var cpf = "12345678901";

        await Assert.ThrowsAsync<ArgumentException>(() =>
                    _useCase.ExecutarAsync(nomeCliente, cpf, valor));
    }
}

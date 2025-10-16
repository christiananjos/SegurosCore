using Moq;
using PropostaHub.Core.Domain.Entities;
using PropostaHub.Core.Domain.Enums;
using PropostaHub.Core.Ports;
using PropostaHub.Core.UseCases;

namespace PropostaHub.Tests
{
    public class AlterarStatusUseCaseTests
    {
        private readonly Mock<IPropostaRepository> _repositoryMock;
        private readonly AlterarStatusPropostaUseCase _useCase;

        public AlterarStatusUseCaseTests()
        {
            _repositoryMock = new Mock<IPropostaRepository>();
            _useCase = new AlterarStatusPropostaUseCase(_repositoryMock.Object);
        }

        [Fact]
        public async Task ExecutarAsync_ComPropostaExistente_DeveAlterarStatus()
        {
            var proposta = Proposta.Criar("João Silva", "12345678901", 1000m);
            var propostaId = proposta.Id;
            var novoStatus = StatusProposta.Aprovada;

            _repositoryMock
                .Setup(r => r.ObterPorIdAsync(propostaId))
                .ReturnsAsync(proposta);

            _repositoryMock
                .Setup(r => r.AtualizarAsync(It.IsAny<Proposta>()))
                .Returns(Task.CompletedTask);


            var resultado = await _useCase.ExecutarAsync(propostaId, novoStatus);

            Assert.NotNull(resultado);
            Assert.Equal(novoStatus, resultado.Status);

            _repositoryMock.Verify(r => r.AtualizarAsync(It.IsAny<Proposta>()), Times.Once);
        }

        [Fact]
        public async Task ExecutarAsync_ComPropostaInexistente_DeveLancarExcecao()
        {

            var propostaId = Guid.NewGuid();

            _repositoryMock
                .Setup(r => r.ObterPorIdAsync(propostaId))
                .ReturnsAsync((Proposta?)null);

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                            _useCase.ExecutarAsync(propostaId, StatusProposta.Aprovada));
        }

        [Fact]
        public void Proposta_AposRejeitada_NaoPermiteAlterarStatus()
        {
            var proposta = Proposta.Criar("Maria Santos", "98765432100", 2000m);
            proposta.AlterarStatus(StatusProposta.Rejeitada);

            Assert.Throws<InvalidOperationException>(() =>
                            proposta.AlterarStatus(StatusProposta.Aprovada));
        }
    }
}

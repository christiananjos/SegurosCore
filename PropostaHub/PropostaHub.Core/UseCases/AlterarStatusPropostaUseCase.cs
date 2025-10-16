using PropostaHub.Core.Domain.Entities;
using PropostaHub.Core.Domain.Enums;
using PropostaHub.Core.Ports;

namespace PropostaHub.Core.UseCases
{
    public class AlterarStatusPropostaUseCase
    {
        private readonly IPropostaRepository _repository;

        public AlterarStatusPropostaUseCase(IPropostaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Proposta> ExecutarAsync(Guid propostaId, StatusProposta novoStatus)
        {
            // Busca a proposta
            var proposta = await _repository.ObterPorIdAsync(propostaId);

            if (proposta == null)
                throw new InvalidOperationException($"Proposta com ID {propostaId} nao encontrada");

            // Altera o status (regras de negocio dentro da entidade)
            proposta.AlterarStatus(novoStatus);

            // Atualiza no repositorio
            await _repository.AtualizarAsync(proposta);

            return proposta;
        }
    }
}

using PropostaHub.Core.Domain.Entities;
using PropostaHub.Core.Domain.Enums;
using PropostaHub.Core.Ports;
using PropostaHub.Core.UseCases;

namespace PropostaHub.Infrastructure.Adapters.Services
{
    public class PropostaAppService : IPropostaService
    {
        private readonly CriarPropostaUseCase _criarUseCase;
        private readonly ListarPropostasUseCase _listarUseCase;
        private readonly AlterarStatusPropostaUseCase _alterarStatusUseCase;
        private readonly IPropostaRepository _repository;

        public PropostaAppService(
            CriarPropostaUseCase criarUseCase,
            ListarPropostasUseCase listarUseCase,
            AlterarStatusPropostaUseCase alterarStatusUseCase,
            IPropostaRepository repository)
        {
            _criarUseCase = criarUseCase;
            _listarUseCase = listarUseCase;
            _alterarStatusUseCase = alterarStatusUseCase;
            _repository = repository;
        }

        public async Task<Proposta> CriarPropostaAsync(string nomeCliente, string cpf, decimal valorSeguro)
        {
            return await _criarUseCase.ExecutarAsync(nomeCliente, cpf, valorSeguro);
        }

        public async Task<IEnumerable<Proposta>> ListarPropostasAsync()
        {
            return await _listarUseCase.ExecutarAsync();
        }

        public async Task<Proposta> AlterarStatusAsync(Guid propostaId, StatusProposta novoStatus)
        {
            return await _alterarStatusUseCase.ExecutarAsync(propostaId, novoStatus);
        }

        public async Task<Proposta?> ObterPropostaPorIdAsync(Guid id)
        {
            return await _repository.ObterPorIdAsync(id);
        }
    }
}

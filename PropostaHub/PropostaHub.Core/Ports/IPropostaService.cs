using PropostaHub.Core.Domain.Entities;
using PropostaHub.Core.Domain.Enums;

namespace PropostaHub.Core.Ports
{
    public interface IPropostaService
    {
        Task<Proposta> CriarPropostaAsync(string nomeCliente, string cpf, decimal valorSeguro);
        Task<IEnumerable<Proposta>> ListarPropostasAsync();
        Task<Proposta> AlterarStatusAsync(Guid propostaId, StatusProposta novoStatus);
        Task<Proposta?> ObterPropostaPorIdAsync(Guid id);
    }
}

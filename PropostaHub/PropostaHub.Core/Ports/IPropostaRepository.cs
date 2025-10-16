using PropostaHub.Core.Domain.Entities;

namespace PropostaHub.Core.Ports
{
    public interface IPropostaRepository
    {
        Task<Proposta> AdicionarAsync(Proposta proposta);
        Task<Proposta?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Proposta>> ListarTodasAsync();
        Task AtualizarAsync(Proposta proposta);
        Task<bool> ExisteAsync(Guid id);
    }
}

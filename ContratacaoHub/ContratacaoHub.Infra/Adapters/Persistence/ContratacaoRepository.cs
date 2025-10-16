using ContratacaoHub.Core.Domain.Entities;
using ContratacaoHub.Core.Ports;
using Microsoft.EntityFrameworkCore;

namespace ContratacaoHub.Infra.Adapters.Persistence
{
    public class ContratacaoRepository : IContratacaoRepository
    {
        private readonly ApplicationDbContext _context;

        public ContratacaoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Contratacao> AdicionarAsync(Contratacao contratacao)
        {
            await _context.Contratacoes.AddAsync(contratacao);
            await _context.SaveChangesAsync();
            return contratacao;
        }

        public async Task<Contratacao?> ObterPorIdAsync(Guid id)
        {
            return await _context.Contratacoes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Contratacao>> ListarTodasAsync()
        {
            return await _context.Contratacoes
                .OrderByDescending(c => c.DataContratacao)
                .ToListAsync();
        }

        public async Task<bool> ExisteContratacaoParaPropostaAsync(Guid propostaId)
        {
            return await _context.Contratacoes.AnyAsync(c => c.PropostaId == propostaId);
        }
    }
}

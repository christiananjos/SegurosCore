using Microsoft.EntityFrameworkCore;
using PropostaHub.Core.Domain.Entities;
using PropostaHub.Core.Ports;

namespace PropostaHub.Infrastructure.Adapters.Persistence
{
    public class PropostaRepository : IPropostaRepository
    {
        private readonly ApplicationDbContext _context;

        public PropostaRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Proposta> AdicionarAsync(Proposta proposta)
        {
            await _context.Propostas.AddAsync(proposta);
            await _context.SaveChangesAsync();
            return proposta;
        }

        public async Task<Proposta?> ObterPorIdAsync(Guid id)
        {
            return await _context.Propostas
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Proposta>> ListarTodasAsync()
        {
            return await _context.Propostas
                .OrderByDescending(p => p.DataCriacao)
                .ToListAsync();
        }

        public async Task AtualizarAsync(Proposta proposta)
        {
            _context.Propostas.Update(proposta);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteAsync(Guid id)
        {
            return await _context.Propostas.AnyAsync(p => p.Id == id);
        }
    }
}

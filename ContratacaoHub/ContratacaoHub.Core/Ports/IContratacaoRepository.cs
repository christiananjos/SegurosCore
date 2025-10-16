using ContratacaoHub.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratacaoHub.Core.Ports
{
    public interface IContratacaoRepository
    {
        Task<Contratacao> AdicionarAsync(Contratacao contratacao);
        Task<Contratacao?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Contratacao>> ListarTodasAsync();
        Task<bool> ExisteContratacaoParaPropostaAsync(Guid propostaId);
    }
}

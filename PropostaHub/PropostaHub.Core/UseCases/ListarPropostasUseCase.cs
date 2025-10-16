using PropostaHub.Core.Domain.Entities;
using PropostaHub.Core.Ports;

namespace PropostaHub.Core.UseCases
{
    public class ListarPropostasUseCase
    {
        private readonly IPropostaRepository _repository;

        public ListarPropostasUseCase(IPropostaRepository repository)
        => _repository = repository;


        public async Task<IEnumerable<Proposta>> ExecutarAsync()
        {
            return await _repository.ListarTodasAsync();
        }
    }
}

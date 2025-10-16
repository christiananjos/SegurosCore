using PropostaHub.Core.Domain.Entities;
using PropostaHub.Core.Ports;

namespace PropostaHub.Core.UseCases
{
    public class CriarPropostaUseCase
    {
        private readonly IPropostaRepository _repository;

        public CriarPropostaUseCase(IPropostaRepository repository)
        => _repository = repository ?? throw new ArgumentNullException(nameof(repository));


        public async Task<Proposta> ExecutarAsync(string nomeCliente, string cpf, decimal valorSeguro)
        {
            // Logica do caso de uso
            var proposta = Proposta.Criar(nomeCliente, cpf, valorSeguro);

            // Persiste atraves do port
            return await _repository.AdicionarAsync(proposta);
        }
    }
}

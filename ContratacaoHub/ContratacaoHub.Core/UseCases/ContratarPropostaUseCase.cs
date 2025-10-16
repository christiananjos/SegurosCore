using ContratacaoHub.Core.Domain.Entities;
using ContratacaoHub.Core.Ports;
using Microsoft.Extensions.Logging;

namespace ContratacaoHub.Core.UseCases
{
    public class ContratarPropostaUseCase
    {
        private readonly IContratacaoRepository _contratacaoRepository;
        private readonly IPropostaServiceClient _propostaClient;
        private readonly ILogger<ContratarPropostaUseCase> _logger;

        public ContratarPropostaUseCase(
            IContratacaoRepository contratacaoRepository,
            IPropostaServiceClient propostaClient,
            ILogger<ContratarPropostaUseCase> logger)
        {
            _contratacaoRepository = contratacaoRepository;
            _propostaClient = propostaClient;
            _logger = logger;
        }

        public async Task<Contratacao> ExecutarAsync(Guid propostaId)
        {
            _logger.LogInformation("Iniciando contratacao da proposta {PropostaId}", propostaId);

            // Verifica se ja existe contratacao para esta proposta
            var jaContratada = await _contratacaoRepository.ExisteContratacaoParaPropostaAsync(propostaId);

            if (jaContratada)
            {
                throw new InvalidOperationException("Esta proposta ja foi contratada anteriormente");
            }

            // Busca a proposta no microservico de Proposta
            var proposta = await _propostaClient.ObterPropostaAsync(propostaId);

            if (proposta == null)
            {
                throw new InvalidOperationException($"Proposta {propostaId} nao encontrada");
            }

            // Valida se a proposta esta aprovada (Status = 2)
            if (proposta.Status != 2)  // 2 = Aprovada
            {
                throw new InvalidOperationException("Somente propostas aprovadas podem ser contratadas");
            }

            // Cria a contratacao
            var contratacao = Contratacao.Criar(
                propostaId,
                proposta.NomeCliente,
                proposta.ValorSeguro);

            // Persiste
            await _contratacaoRepository.AdicionarAsync(contratacao);

            _logger.LogInformation("Contratacao {ContratacaoId} criada com sucesso", contratacao.Id);

            return contratacao;
        }
    }
}

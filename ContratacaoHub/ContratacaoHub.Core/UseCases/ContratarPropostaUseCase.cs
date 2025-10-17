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

            var jaContratada = await _contratacaoRepository.ExisteContratacaoParaPropostaAsync(propostaId);

            if (jaContratada)
                throw new InvalidOperationException("Esta proposta ja foi contratada anteriormente");

            var proposta = await _propostaClient.ObterPropostaAsync(propostaId);

            if (proposta == null)
                throw new InvalidOperationException($"Proposta {propostaId} nao encontrada");

            if (proposta.Status != 2)
                throw new InvalidOperationException("Somente propostas aprovadas podem ser contratadas");

            var contratacao = Contratacao.Criar(
                            propostaId,
                            proposta.NomeCliente,
                            proposta.ValorSeguro);


            await _contratacaoRepository.AdicionarAsync(contratacao);

            _logger.LogInformation("Contratacao {ContratacaoId} criada com sucesso", contratacao.Id);

            return contratacao;
        }
    }
}

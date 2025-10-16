using ContratacaoHub.Core.Ports;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContratacaoHub.Infra.Adapters.HttpClients
{
    public class PropostaServiceClient : IPropostaServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PropostaServiceClient> _logger;

        public PropostaServiceClient(
            HttpClient httpClient,
            ILogger<PropostaServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<PropostaDto?> ObterPropostaAsync(Guid propostaId)
        {
            try
            {
                _logger.LogInformation("Buscando proposta {PropostaId} no PropostaService", propostaId);

                var response = await _httpClient.GetAsync($"/api/proposta/{propostaId}");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Proposta {PropostaId} nao encontrada. Status: {StatusCode}",
                        propostaId, response.StatusCode);
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                var proposta = JsonSerializer.Deserialize<PropostaDto>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return proposta;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro ao comunicar com PropostaService");
                throw new InvalidOperationException("Falha na comunicacao com o servico de propostas", ex);
            }
        }
    }
}

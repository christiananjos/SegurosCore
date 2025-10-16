using Microsoft.AspNetCore.Mvc;
using PropostaHub.Core.Domain.Enums;
using PropostaHub.Core.Ports;

namespace PropostaHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropostaController : ControllerBase
    {
        private readonly IPropostaService _propostaService;
        private readonly ILogger<PropostaController> _logger;

        public PropostaController( IPropostaService propostaService, ILogger<PropostaController> logger)
        {
            _propostaService = propostaService;
            _logger = logger;
        }

        /// <summary>
        /// Cria uma nova proposta de seguro
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CriarProposta([FromBody] CriarPropostaRequest request)
        {
            try
            {
                _logger.LogInformation("Criando nova proposta para cliente: {NomeCliente}", request.NomeCliente);

                var proposta = await _propostaService.CriarPropostaAsync(
                    request.NomeCliente,
                    request.Cpf,
                    request.ValorSeguro);

                return CreatedAtAction(
                    nameof(ObterProposta),
                    new { id = proposta.Id },
                    proposta);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro de validacao ao criar proposta");
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar proposta");
                return StatusCode(500, new { mensagem = "Erro interno no servidor" });
            }
        }

        /// <summary>
        /// Lista todas as propostas
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ListarPropostas()
        {
            try
            {
                var propostas = await _propostaService.ListarPropostasAsync();
                return Ok(propostas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar propostas");
                return StatusCode(500, new { mensagem = "Erro interno no servidor" });
            }
        }

        /// <summary>
        /// Obtem uma proposta especifica por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterProposta(Guid id)
        {
            try
            {
                var proposta = await _propostaService.ObterPropostaPorIdAsync(id);

                if (proposta == null)
                    return NotFound(new { mensagem = "Proposta nao encontrada" });

                return Ok(proposta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter proposta {PropostaId}", id);
                return StatusCode(500, new { mensagem = "Erro interno no servidor" });
            }
        }

        /// <summary>
        /// Altera o status de uma proposta
        /// </summary>
        [HttpPut("{id}/status")]
        public async Task<IActionResult> AlterarStatus(
            Guid id,
            [FromBody] AlterarStatusRequest request)
        {
            try
            {
                _logger.LogInformation("Alterando status da proposta {PropostaId} para {NovoStatus}",
                    id, request.NovoStatus);

                var proposta = await _propostaService.AlterarStatusAsync(id, request.NovoStatus);

                return Ok(proposta);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Operacao invalida ao alterar status");
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao alterar status da proposta {PropostaId}", id);
                return StatusCode(500, new { mensagem = "Erro interno no servidor" });
            }
        }
    }

    // DTOs (Request/Response)
    public record CriarPropostaRequest(string NomeCliente, string Cpf, decimal ValorSeguro);
    public record AlterarStatusRequest(StatusProposta NovoStatus);
}

using ContratacaoHub.Core.Ports;
using ContratacaoHub.Core.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace ContratacaoHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContratacaoController : ControllerBase
    {
        private readonly ContratarPropostaUseCase _contratarUseCase;
        private readonly IContratacaoRepository _repository;
        private readonly ILogger<ContratacaoController> _logger;

        public ContratacaoController(
            ContratarPropostaUseCase contratarUseCase,
            IContratacaoRepository repository,
            ILogger<ContratacaoController> logger)
        {
            _contratarUseCase = contratarUseCase;
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Contrata uma proposta aprovada
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ContratarProposta([FromBody] ContratarRequest request)
        {
            try
            {
                _logger.LogInformation("Iniciando contratacao da proposta {PropostaId}", request.PropostaId);

                var contratacao = await _contratarUseCase.ExecutarAsync(request.PropostaId);

                return CreatedAtAction(
                    nameof(ObterContratacao),
                    new { id = contratacao.Id },
                    contratacao);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Erro ao contratar proposta");
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao contratar proposta");
                return StatusCode(500, new { mensagem = "Erro interno no servidor" });
            }
        }

        /// <summary>
        /// Lista todas as contratacoes
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ListarContratacoes()
        {
            try
            {
                var contratacoes = await _repository.ListarTodasAsync();
                return Ok(contratacoes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar contratacoes");
                return StatusCode(500, new { mensagem = "Erro interno no servidor" });
            }
        }

        /// <summary>
        /// Obtem uma contratacao especifica
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterContratacao(Guid id)
        {
            try
            {
                var contratacao = await _repository.ObterPorIdAsync(id);

                if (contratacao == null)
                    return NotFound(new { mensagem = "Contratacao nao encontrada" });

                return Ok(contratacao);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter contratacao");
                return StatusCode(500, new { mensagem = "Erro interno no servidor" });
            }
        }
    }

    public record ContratarRequest(Guid PropostaId);
}

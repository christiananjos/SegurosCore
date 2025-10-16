namespace ContratacaoHub.Core.Ports
{
    public interface IPropostaServiceClient
    {
        Task<PropostaDto?> ObterPropostaAsync(Guid propostaId);
    }

    public class PropostaDto
    {
        public Guid Id { get; set; }
        public string NomeCliente { get; set; }
        public decimal ValorSeguro { get; set; }
        public int Status { get; set; }  // 1=EmAnalise, 2=Aprovada, 3=Rejeitada
    }
}

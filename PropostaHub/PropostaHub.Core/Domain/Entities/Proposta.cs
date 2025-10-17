using PropostaHub.Core.Domain.Enums;

namespace PropostaHub.Core.Domain.Entities
{
    public class Proposta
    {
        public Guid Id { get; private set; }
        public string NomeCliente { get; private set; }
        public string Cpf { get; private set; }
        public decimal ValorSeguro { get; private set; }
        public StatusProposta Status { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAtualizacao { get; private set; }


        private Proposta() { }


        public static Proposta Criar(string nomeCliente, string cpf, decimal valorSeguro)
        {

            if (string.IsNullOrWhiteSpace(nomeCliente))
                throw new ArgumentException("Nome do cliente é obrigatorio");

            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF é obrigatorio");

            if (valorSeguro <= 0)
                throw new ArgumentException("Valor do seguro deve ser maior que zero");

            return new Proposta
            {
                Id = Guid.NewGuid(),
                NomeCliente = nomeCliente,
                Cpf = cpf,
                ValorSeguro = valorSeguro,
                Status = StatusProposta.EmAnalise,  // sempre começa em analise
                DataCriacao = DateTime.UtcNow
            };
        }


        public void AlterarStatus(StatusProposta novoStatus)
        {

            if (Status == StatusProposta.Rejeitada)
                throw new InvalidOperationException("Nao é possivel alterar o status de uma proposta rejeitada");

            if (Status == StatusProposta.Aprovada && novoStatus == StatusProposta.EmAnalise)
                throw new InvalidOperationException("Proposta aprovada nao pode voltar para analise");

            Status = novoStatus;
            DataAtualizacao = DateTime.UtcNow;
        }
    }
}

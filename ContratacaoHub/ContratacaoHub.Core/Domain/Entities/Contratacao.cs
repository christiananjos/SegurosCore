namespace ContratacaoHub.Core.Domain.Entities
{
    public class Contratacao
    {
        public Guid Id { get; private set; }
        public Guid PropostaId { get; private set; }
        public DateTime DataContratacao { get; private set; }
        public string NomeCliente { get; private set; }
        public decimal ValorContratado { get; private set; }

        private Contratacao() { }

        public static Contratacao Criar(Guid propostaId, string nomeCliente, decimal valorContratado)
        {
            if (propostaId == Guid.Empty)
                throw new ArgumentException("PropostaId invalido");

            if (string.IsNullOrWhiteSpace(nomeCliente))
                throw new ArgumentException("Nome do cliente é obrigatorio");

            if (valorContratado <= 0)
                throw new ArgumentException("Valor contratado deve ser maior que zero");

            return new Contratacao
            {
                Id = Guid.NewGuid(),
                PropostaId = propostaId,
                NomeCliente = nomeCliente,
                ValorContratado = valorContratado,
                DataContratacao = DateTime.UtcNow
            };
        }
    }
}

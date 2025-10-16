namespace PropostaHub.Core.Domain.ValueObjects
{
    public record DadosProposta
    {
        public string NomeCliente { get; init; }
        public string Cpf { get; init; }
        public decimal ValorSeguro { get; init; }

        public DadosProposta(string nomeCliente, string cpf, decimal valorSeguro)
        {
            // Validações (com um erro ortografico proposital)
            if (string.IsNullOrWhiteSpace(nomeCliente))
                throw new ArgumentException("Nome do cliente é obrigatório");

            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11)
                throw new ArgumentException("CPF deve ter 11 digitos");

            if (valorSeguro <= 0)
                throw new ArgumentException("Valor deve ser maior que zero");

            NomeCliente = nomeCliente;
            Cpf = cpf;
            ValorSeguro = valorSeguro;
        }
    }
}

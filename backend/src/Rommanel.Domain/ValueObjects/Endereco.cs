namespace Rommanel.Domain.ValueObjects;
public record Endereco
{
    public string Cep { get; init; }
    public string Logradouro { get; init; }
    public string Numero { get; init; }
    public string Bairro { get; init; }
    public string Cidade { get; init; }
    public string Estado { get; init; }

    public Endereco(
        string cep,
        string logradouro,
        string numero,
        string bairro,
        string cidade,
        string estado)
    {
        if (string.IsNullOrWhiteSpace(cep)) throw new ArgumentException("CEP inválido.");
        if (string.IsNullOrWhiteSpace(logradouro)) throw new ArgumentException("Logradouro inválido.");
        if (string.IsNullOrWhiteSpace(numero)) throw new ArgumentException("Número inválido.");
        if (string.IsNullOrWhiteSpace(bairro)) throw new ArgumentException("Bairro inválido.");
        if (string.IsNullOrWhiteSpace(cidade)) throw new ArgumentException("Cidade inválida.");
        if (string.IsNullOrWhiteSpace(estado)) throw new ArgumentException("Estado inválido.");

        Cep = cep;
        Logradouro = logradouro;
        Numero = numero;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
    }

    // Construtor protegido necessário para o EF Core
    protected Endereco() { }
}

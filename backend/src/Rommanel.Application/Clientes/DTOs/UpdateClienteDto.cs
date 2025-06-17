namespace Rommanel.Application.Clientes.DTOs;

public class UpdateClienteDto
{
    public string Nome { get; set; }
    public string Documento { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }

    public EnderecoDto Endereco { get; set; }

    public string? InscricaoEstadual { get; set; }
    public bool IsentoIE { get; set; }
}


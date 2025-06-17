using FluentValidation;
using Rommanel.Application.Clientes.Commands;

namespace Rommanel.Application.Clientes.Validators;

public class CreateClienteValidator : AbstractValidator<CreateClienteCommand>
{
    public CreateClienteValidator()
    {
        RuleFor(c => c.Nome).NotEmpty();
        RuleFor(c => c.Documento).NotEmpty();
        RuleFor(c => c.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.Telefone).NotEmpty();

        RuleFor(c => c.Cep).NotEmpty();
        RuleFor(c => c.Logradouro).NotEmpty();
        RuleFor(c => c.Numero).NotEmpty();
        RuleFor(c => c.Bairro).NotEmpty();
        RuleFor(c => c.Cidade).NotEmpty();
        RuleFor(c => c.Estado).NotEmpty();

        RuleFor(x => x.DataNascimento)
            .NotEmpty().WithMessage("Data de nascimento obrigatória.")
            .Must(BeMaiorDeIdade).WithMessage("Cliente deve ser maior de 18 anos.")
            .When(x => x.Documento.Length == 11);

        RuleFor(x => x.InscricaoEstadual)
            .NotEmpty().When(x => !x.IsentoIE && x.Documento.Length == 14)
            .WithMessage("Inscrição estadual obrigatória para pessoa jurídica não isenta.");
    }

    private bool BeMaiorDeIdade(DateTime data)
    {
        return data <= DateTime.Today.AddYears(-18);
    }
}
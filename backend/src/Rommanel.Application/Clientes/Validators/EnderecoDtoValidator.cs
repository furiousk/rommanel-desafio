using FluentValidation;
using Rommanel.Application.Clientes.DTOs;

namespace Rommanel.Application.Clientes.Validators;

public class EnderecoDtoValidator : AbstractValidator<EnderecoDto>
{
    public EnderecoDtoValidator()
    {
        RuleFor(x => x.Cep).NotEmpty();
        RuleFor(x => x.Logradouro).NotEmpty();
        RuleFor(x => x.Numero).NotEmpty();
        RuleFor(x => x.Bairro).NotEmpty();
        RuleFor(x => x.Cidade).NotEmpty();
        RuleFor(x => x.Estado).NotEmpty();
    }
}

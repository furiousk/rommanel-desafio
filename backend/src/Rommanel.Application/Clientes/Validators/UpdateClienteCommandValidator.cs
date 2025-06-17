using FluentValidation;
using Rommanel.Application.Clientes.Commands;

namespace Rommanel.Application.Clientes.Validators;

public class UpdateClienteCommandValidator : AbstractValidator<UpdateClienteCommand>
{
    public UpdateClienteCommandValidator()
    {
    }
}

using FluentValidation;
using KibanaSerilog.Domain.Commands;

namespace KibanaSerilog.Domain.Validators
{
    public class UserAddRequestValidator: AbstractValidator<UserAddRequest>
    {
        public UserAddRequestValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("O nome do usuário é obrigatório");

            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage("O email do usuário é obrigatório");

            RuleFor(c => c.Email)
                .EmailAddress()
                .WithMessage("O email do usuário é inválido");


            RuleFor(c => c.BirthDate)
                .NotNull()
                .WithMessage("A data de aniversário do usuário é obrigatória");

        }
    }
}

using KibanaSerilog.Domain.Validators;
using MediatR;
using System;

namespace KibanaSerilog.Domain.Commands
{
    public class UserAddRequest: BaseValidator, IRequest<BaseResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new UserAddRequestValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

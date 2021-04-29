using KibanaSerilog.Domain.Validators;
using MediatR;
using System;

namespace KibanaSerilog.Domain.Commands
{
    public class UserAddRequest: IRequest<BaseResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}

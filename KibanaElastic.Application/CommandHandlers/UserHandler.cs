using KibanaSerilog.Domain.Commands;
using KibanaSerilog.Domain.Entities;
using KibanaSerilog.Domain.Interfaces;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace KibanaSerilog.Application.CommandHandlers
{
    public class UserHandler : IRequestHandler<UserAddRequest, BaseResponse>
    {

        private readonly IUserRepository _userRepository;

        public UserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse> Handle(UserAddRequest request, CancellationToken cancellationToken)
        {

            var user = new UserEntity()
            {
                Id = Guid.NewGuid(),
                BirthDate = request.BirthDate,
                Email = request.Email,
                Name = request.Name,
                CreationDate = DateTime.Now
            };

            var result = await _userRepository.Add(user);

            if (!result)
            {
                return new BaseResponse(System.Net.HttpStatusCode.BadRequest, "Ocorreu um erro inesperado.");
            }

            return new BaseResponse(System.Net.HttpStatusCode.OK);

        }
    }
}

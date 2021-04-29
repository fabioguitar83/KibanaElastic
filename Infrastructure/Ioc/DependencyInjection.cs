using KibanaSerilog.Domain.Behaviors;
using KibanaSerilog.Domain.Interfaces;
using KibanaSerilog.Infrastructure.Elastic;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace KibanaSerilog.Infrastructure.Ioc
{
    public static class DependencyInjection
    {
        public static void Build(IServiceCollection services) 
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
  
    }
}

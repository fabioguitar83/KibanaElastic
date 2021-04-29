using KibanaSerilog.Domain.Entities;
using KibanaSerilog.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace KibanaSerilog.Infrastructure.Elastic
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration config) : base("user", config)
        {
        }

        public async Task<bool> Add(UserEntity user)
        {
            var asyncIndexResponse = await _client.IndexDocumentAsync(user);
            return asyncIndexResponse.IsValid;
        }
    }
}

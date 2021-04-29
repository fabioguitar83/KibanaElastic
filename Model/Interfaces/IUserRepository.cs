using KibanaSerilog.Domain.Entities;
using System.Threading.Tasks;

namespace KibanaSerilog.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Add(UserEntity user);
    }
}

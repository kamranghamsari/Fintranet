using Fintranet.Entities.DataTransferObjects;
using Fintranet.Entities.Helpers;
using Fintranet.Entities.InputModels;
using System.Threading.Tasks;

namespace Fintranet.Repositories.User
{
    public interface IUserRepository : IBaseRepository<UserDto, Contracts.DataModels.User>
    {
        Task<BaseResponseDto<AuthenticateResponse>> Authenticate(AuthenticateRequest model);
    }
}

using AutoMapper;
using Fintranet.Entities.DataTransferObjects;

namespace Fintranet.Repositories.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region CategoryDiscount
            CreateMap<UserDto, Contracts.DataModels.User>().ReverseMap();
            #endregion
        }
    }
}

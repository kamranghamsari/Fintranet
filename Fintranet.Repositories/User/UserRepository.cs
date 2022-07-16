using AutoMapper;
using Fintranet.Contracts;
using Fintranet.Entities.DataTransferObjects;
using Fintranet.Entities.Helpers;
using Fintranet.Entities.InputModels;
using Fintranet.Repositories.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fintranet.Repositories.User
{
    public class UserRepository : BaseRepository<UserDto, Contracts.DataModels.User>, IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserRepository(RepositoryContext applicationDbContext, ISortHelper<Contracts.DataModels.User> sortHelper,
            IMapper mapper, IPagingHelper<Contracts.DataModels.User> pagingHelper, IOptions<AppSettings> appSettings)
            : base(applicationDbContext, mapper, sortHelper, pagingHelper)
        {
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public async Task<BaseResponseDto<AuthenticateResponse>> Authenticate(AuthenticateRequest model)
        {
            var users = await GetAsync(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (users == null || users.Data == null || users.Data.Count == 0) return new BaseResponseDto<AuthenticateResponse>
            {
                responseCode = ResponseCode.NotFound,
                responseInformation = "NotFound"
            };

            var user = users.Data.SingleOrDefault();
            var userDto = _mapper.Map<UserDto>(user);
            // authentication successful so generate jwt token
            var token = generateJwtToken(userDto);

            return new BaseResponseDto<AuthenticateResponse>
            {
                data = new AuthenticateResponse(userDto, token),
                responseCode = ResponseCode.Success
            };
        }

        private string generateJwtToken(UserDto userDto)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userDto.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}

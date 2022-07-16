using Fintranet.Contracts;
using Fintranet.Entities.InputModels;
using Fintranet.Repositories;
using Fintranet.Repositories.Helpers;
using Fintranet.Repositories.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Fintranet.Test
{
    public class DbFixture
    {
        public DbFixture()
        {
            var serviceCollection = new ServiceCollection();
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).AddEnvironmentVariables().Build();
            serviceCollection.AddDatabaseDependencyInjections("Server=(localdb)\\MSSQLLocalDB;Database=Fintranet;Trusted_Connection=True;MultipleActiveResultSets=true");
            serviceCollection.AddRepositoryDependencyInjections();
            serviceCollection.Configure<AppSettings>(builder.GetSection("AppSettings"));
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }

    public class UnitTest1 : IClassFixture<DbFixture>
    {
        private readonly IUserRepository _userRepository;

        public UnitTest1(DbFixture fixture)
        {
            _userRepository = fixture.ServiceProvider.GetService<IUserRepository>();
        }

        [Fact]
        public async Task Authenticate()
        {
            var result = await _userRepository.Authenticate(new AuthenticateRequest
            {
                Password = "Test1234",
                Username = "kamikg",
            });
            Assert.NotNull(result);
            Assert.True(result.responseCode == Entities.Helpers.ResponseCode.Success);
        }

        [Fact]
        public async Task GetAll()
        {
            var result = await _userRepository.GetAsync();
            Assert.NotNull(result);
            Assert.True(result.Data.Any());
        }
    }
}
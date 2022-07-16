using Fintranet.Entities.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintranet.Contracts.DataModels
{
    public class User : UserDto
    {
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="modelBuilder"></param>
        public void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder.ToTable("User", schema: "dbo");

            modelBuilder.HasData(
             new User
             {
                 Id = 1,
                 FirstName = "Kamran",
                 LastName = "Ghamsari",
                 Username = "kamikg",
                 Password = "Test1234"
             },
             new User
             {
                 Id = 2,
                 FirstName = "Jane",
                 LastName = "Doe",
                 Username = "test",
                 Password = "Test1234"
             });
        }
    }

}

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
        }
    }

}

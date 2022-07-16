using Fintranet.Contracts.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fintranet.Contracts
{
    public class RepositoryContext : BaseRepositoryContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }

    /// <summary>
    /// Base repository context
    /// </summary>
    public class BaseRepositoryContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public BaseRepositoryContext(DbContextOptions options)
            : base(options)
        {
        }
    }

}

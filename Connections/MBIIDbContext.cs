using Microsoft.EntityFrameworkCore;
using OpenJKLoader.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Connections
{
    public class MBIIDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        public MBIIDbContext(DbContextOptions<MBIIDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

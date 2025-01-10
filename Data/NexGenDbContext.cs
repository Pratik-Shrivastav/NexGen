using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexGen.Model;

namespace NexGen.Data
{
    public class NexGenDbContext : DbContext
    {
        public DbSet<Admin> AdminTable { get; set; }
        public DbSet<MembershipTransaction> MembershipTransactionTable { get; set; }
        public DbSet<PlayStation> PlayStationTable { get; set; }
        public DbSet<PlayTransaction> PlayTransactionTable { get; set; }
        public DbSet<User> UserTable { get; set; }
        private string ConnectionString { get; set; }
        public NexGenDbContext(DbContextOptions<NexGenDbContext> options) : base(options)
        {
            ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NexGen;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
    }
}
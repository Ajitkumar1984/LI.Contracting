using Microsoft.EntityFrameworkCore;
using System;

namespace LI.Contracting.DataContext
{
    public class ContractingContext: DbContext
    {
        public ContractingContext(DbContextOptions options) : base(options)
        {
          // base.e
        }

        public DbSet<MGAEntity> MGA { get; set; }
        public DbSet<AdvisorEntity> Advisor { get; set; }
        public DbSet<CarrierEntity> Carrier { get; set; }
        public DbSet<ContractEntity> Contract { get; set; }

    }
    
}

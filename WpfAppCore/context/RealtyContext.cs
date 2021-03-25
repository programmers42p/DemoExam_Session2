using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;
using WpfAppCore.model;

namespace WpfAppCore.context
{
    class RealtyContext :DbContext
    {
        public DbSet<Land> Lands { get; set; }
        public  DbSet<House> Houses { get; set; }
        public  DbSet<Apartment> Apartments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;database=session2;user=root;password=root");
            }
        }

        public RealtyContext()  { 
        }
        public RealtyContext(DbContextOptions<RealtyContext> options): base(options) {
        }
    }
}

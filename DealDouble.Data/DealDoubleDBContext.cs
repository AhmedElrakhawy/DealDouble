using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Data
{
    public class DealDoubleDBContext : DbContext
    {
        public DealDoubleDBContext() : base("name=DealDoubleConnectionString")
        {
        }
        public DbSet<Auction> Auctions { get; set; }
    }
}

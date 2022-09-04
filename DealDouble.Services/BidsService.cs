using DealDouble.Data;
using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Services
{
    public class BidsService
    {
        public bool AddiBid(Bid bid)
        {
            using (var Context = new DealDoubleDBContext())
            {
                Context.Bids.Add(bid);
                return Context.SaveChanges() > 0;
            }
        }
    }
}

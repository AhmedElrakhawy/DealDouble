using DealDouble.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Services
{
    public class DashboardService
    {
        public int UsersCount()
        {
            using (var Context = new DealDoubleDBContext())
            {
                return Context.Users.Count();
            }
        }
        public int GetBidsCount()
        {
            using (var Context = new DealDoubleDBContext())
            {
                return Context.Bids.ToList().Count;
            }
        }
        public int GetAuctionsCount()
        {
            using (var Context = new DealDoubleDBContext())
            {
                return Context.Auctions.ToList().Count;
            }
        }
    }
}

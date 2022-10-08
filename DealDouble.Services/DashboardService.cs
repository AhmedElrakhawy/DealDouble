using DealDouble.Data;
using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public List<Comment> GetAllComments(int? PageNumber , int PageSize)
        {
            using (var Context = new DealDoubleDBContext())
            {
                PageNumber = PageNumber ?? 1;
                int SkipCount = (PageNumber.Value - 1) * PageSize;

                return Context.Comments.OrderByDescending(x => x.PostedOn).Skip(SkipCount).Take(PageSize).ToList();
            }
        }
        public int GetAllCommentsCount()
        {
            using (var Context = new DealDoubleDBContext())
            {

                return Context.Comments.ToList().Count();
            }
        }
    }
}

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
        public List<Bid> UserBids(string UserID)
        {
            using (var Context = new DealDoubleDBContext())
            {
                return Context.Bids.Include(a => a.Auction)
                    .Include(c => c.Auction.Category).Where(x => x.UserID == UserID).ToList();
            }
        }
        public List<Bid> SearchBids(string SearchByCategory,int? SearchbyBidAmount, int? PageNumber, int PageSize)
        {
            using (var Context = new DealDoubleDBContext())
            {
                var Bids = Context.Bids.Include(x => x.Auction).Include(u => u.User).Include(c => c.Auction.Category).AsQueryable();
                if (!string.IsNullOrEmpty(SearchByCategory))
                {
                    Bids = Bids.Where(x => x.Auction.Category.Name.ToLower().Contains(SearchByCategory.ToLower()));
                }
                if (SearchbyBidAmount.HasValue && SearchbyBidAmount.Value > 0)
                {
                    Bids = Bids.Where(x => x.BidAmount == SearchbyBidAmount);
                }
                PageNumber = PageNumber ?? 1;
                int SkipCount = (PageNumber.Value - 1) * PageSize;

                return Bids.OrderByDescending(x => x.BidAmount).Skip(SkipCount).Take(PageSize).ToList();
            }
        }
        public int GetBidsCount(string SearchByCategory, int? SearchbyBidAmount)
        {
            using (var Context = new DealDoubleDBContext())
            {
                var Bids = Context.Bids.Include(c => c.Auction.Category).AsQueryable();
                if (SearchbyBidAmount.HasValue && SearchbyBidAmount.Value > 0)
                {
                    Bids = Bids.Where(x => x.BidAmount == SearchbyBidAmount);
                }
                if (!string.IsNullOrEmpty(SearchByCategory))
                {
                    Bids = Bids.Where(x => x.Auction.Category.Name.ToLower().Contains(SearchByCategory.ToLower()));
                }
                return Bids.Count();
            }
        }
        public List<Bid> AllBids()
        {
            using (var Context = new DealDoubleDBContext())
            {
                return Context.Bids.Include(a => a.Auction)
                    .Include(c => c.Auction.Category).Include(u => u.User).ToList();
            }
        }
    }
}

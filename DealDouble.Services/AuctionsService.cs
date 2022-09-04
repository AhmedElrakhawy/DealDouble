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
    public class categoriesService
    {
        public List<Auction> GetAllAuctionsWithCategories()
        {
            using (var Context = new DealDoubleDBContext())
            {
                return Context.Auctions.Include(C => C.Category).ToList();
            }
        }
        public List<Auction> GetAllAuctionsWithPictures()
        {
            using (var Context = new DealDoubleDBContext())
            {
                return Context.Auctions.Include(C => C.Category)
                    .Include(x => x.AuctionPictures.Select(P => P.Picture)).ToList();
            }
        }
        public List<Auction> GetAllAuctions()
        {
            using (var Context = new DealDoubleDBContext())
            {
                return Context.Auctions.ToList();
            }
        }
        public List<Auction> GetpromotedAuctions()
        {
            using (var Context = new DealDoubleDBContext())
            {
                return Context.Auctions.Include(x => x.AuctionPictures.Select(P => P.Picture)).Take(4).ToList();
            }
        }

        public int GetAuctionsCount(string SearchTerm, int? CategoryID)
        {
            using (var Context = new DealDoubleDBContext())
            {
                var auctions = Context.Auctions.AsQueryable();
                if (CategoryID.HasValue && CategoryID.Value > 0)
                {
                    auctions = auctions.Where(x => x.CategoryID == CategoryID);
                }
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    auctions = auctions.Where(x => x.Title.ToLower().Contains(SearchTerm.ToLower()));
                }
                return auctions.Count();
            }
        }

        public Auction GetAuctionByID(int ID)
        {
            using (var Context = new DealDoubleDBContext())
            {
                return Context.Auctions.Include(x => x.AuctionPictures)
                    .Include(x => x.AuctionPictures.Select(p=> p.Picture))
                    .Include(b => b.Bids)
                    .Include(u => u.Bids.Select(a => a.User))
                    .Include(c=> c.Category).FirstOrDefault(x=> x.ID == ID);
            }
        }
        public void UpdateAuction(Auction auction)
        {
            using (var Context = new DealDoubleDBContext())
            {
                var actualAuction = Context.Auctions.Find(auction.ID);
                Context.AuctionPictures.RemoveRange(actualAuction.AuctionPictures);
                Context.Entry(actualAuction).CurrentValues.SetValues(auction);
                if (auction.AuctionPictures != null)
                {
                Context.AuctionPictures.AddRange(auction.AuctionPictures);
                }
                Context.SaveChanges();
            }
        }
        public List<Auction> SearchAuctions(int? CategoryID , string SearchTerm , int? PageNumber, int PageSize)
        {
            using (var Context = new DealDoubleDBContext())
            {
                var Auctions = Context.Auctions.Include(x => x.Category).AsQueryable();
                if (CategoryID.HasValue && CategoryID.Value > 0)
                {
                    Auctions = Auctions.Where(x => x.CategoryID == CategoryID); 
                }
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    Auctions = Auctions.Where(x => x.Title.ToLower().Contains(SearchTerm.ToLower()));
                }
                PageNumber = PageNumber ?? 1;
                int SkipCount = (PageNumber.Value - 1) * PageSize;

                return Auctions.OrderBy(x => x.Title).Skip(SkipCount).Take(PageSize).ToList();
            }
        }
        public void DeleteAuction(Auction auction)
        {
            using (var Context = new DealDoubleDBContext())
            {
                Context.Entry(auction).State = EntityState.Deleted;
                Context.SaveChanges();
            }
        }
        public void SaveAuction (Auction auction)
        {
            using (var Context = new DealDoubleDBContext())
            {
                Context.Entry(auction).State = EntityState.Added;
                //Context.Auctions.Add(auction);
                Context.SaveChanges();
            }
        }
    }
}

﻿using DealDouble.Data;
using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Services
{
    public class AuctionsService
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
        public Auction GetAuctionByID(int ID)
        {
            using (var Context = new DealDoubleDBContext())
            {
                return Context.Auctions.Include(x => x.AuctionPictures.Select(P => P.Picture)).FirstOrDefault(x=> x.ID == ID);
            }
        }
        public void UpdateAuction(Auction auction)
        {
            using (var Context = new DealDoubleDBContext())
            {
                Context.Entry(auction).State = EntityState.Modified;
                Context.SaveChanges();
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

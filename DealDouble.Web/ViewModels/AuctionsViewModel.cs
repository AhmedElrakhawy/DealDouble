using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealDouble.Web.ViewModels
{
    public class AuctionsViewModel : PageViewModel
    {
        public List<Auction> AllAuctions { get; set; }
        public List<Auction> promotedAuctions { get; set; }

    }
    public class AuctionsListViewModel : PageViewModel
    {
        public List<Auction> Auctions { get; set; }
    }
    public class DetailsViewModel : PageViewModel
    {
        public Auction auction { get; set; }
    }
    public class CreateAuctionViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal ActualAmount { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }
        public string AuctionPictures { get; set; }
        public int CategoryID { get; set; }
        public List<Category> Categories { get; set; }
    }

    public class EditAuctionViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal ActualAmount { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }
        public List<AuctionPicture> AuctionPicturesList { get; set; }
        public string AuctionPictures { get; set; }
        public int CategoryID { get; set; }
        public List<Category> Categories { get; set; }
        public int ID { get; set; }
    }
}
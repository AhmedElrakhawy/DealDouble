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
}
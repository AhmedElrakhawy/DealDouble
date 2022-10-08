using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealDouble.Web.ViewModels
{
    public class CommentViewModel
    {
        public string UserId { get; set; }
        public string Text { get; set; }
        public int EntityID { get; set; }
        public int RecordID { get; set; }
        public int Rating { get; set; }
        public string EntityName { get; set; }
    }

    public class BidsViewModel 
    {
        public List<Bid> Bids { get; set; }
        public string SearchByCategory { get; set; }
        public int? SearchbyBidAmount { get; set; }
        public Pager Pager { get; set; }
        public int? PageNum { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Entities
{
    public class Bid : BaseEntity
    {
        public int AuctionID { get; set; }
        public Auction Auction { get; set; }

        public string UserID { get; set; }
        public DealDoubleUser User { get; set; }

        public decimal BidAmount { get; set; }
        public DateTime TimesTamp { get; set; }
    }
}

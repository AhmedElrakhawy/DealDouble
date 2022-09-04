using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace DealDouble.Entities
{
    public class Auction : BaseEntity
    {
        [Required]
        [MinLength(15, ErrorMessage = "Minlength is 15")]
        [MaxLength(150,ErrorMessage ="Maxlength is 150")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(100,1000000,ErrorMessage ="Range Must be between 100 to 1000000")]
        public decimal ActualAmount { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartingTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> EndingTime { get; set; }
        public virtual List<AuctionPicture> AuctionPictures { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Bid> Bids { get; set; }
    }
}

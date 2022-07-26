﻿using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DealDouble.Web.ViewModels
{
    public class AuctionsViewModel : PageViewModel
    {
        public List<Auction> AllAuctions { get; set; }
        public List<Category> Categories { get; set; }
        public int? CategoryID { get; set; }
        public Pager Pager { get; set; }
        public string SearchTerm { get;  set; }
    }
    public class AuctionsListViewModel : PageViewModel
    {
        public List<Auction> Auctions { get; set; }
        public List<Category> Categories { get; set; }
        public string SearchTerm { get; set; }
        public int? CategoryID { get; set; }
        public int? PageNum { get; set; }
        public Pager Pager { get; set; }
    }
    public class DetailsViewModel : PageViewModel
    {
        public Auction auction { get; set; }
        public decimal BidAmount { get; set; }
        public DealDoubleUser LastBidder { get; set; }
        public List<Comment> Comments { get; set; }
        public int EntityID { get;  set; }
        public string UserId { get; set; }
        public bool IsAuthenticated { get; internal set; }
        public Double AvgRating { get; set; }
    }
    public class CreateAuctionViewModel
    {
        [Required]
        [MinLength(15, ErrorMessage = "Minlength is 15")]
        [MaxLength(150, ErrorMessage = "Maxlength is 150")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(100, 1000000, ErrorMessage = "Range Must be between 100 to 1000000")]
        public decimal ActualAmount { get; set; }
        public DateTime? StartingTime { get; set; }
        public DateTime? EndingTime { get; set; }
        public string AuctionPictures { get; set; }
        public int CategoryID { get; set; }
        public List<Category> Categories { get; set; }
    }

    public class EditAuctionViewModel
    {
        [Required]
        [MinLength(15, ErrorMessage = "Minlength is 15")]
        [MaxLength(150, ErrorMessage = "Maxlength is 150")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(100, 1000000, ErrorMessage = "Range Must be between 100 to 1000000")]
        public decimal ActualAmount { get; set; }
        public DateTime? StartingTime { get; set; }
        public DateTime? EndingTime { get; set; }
        public List<AuctionPicture> AuctionPicturesList { get; set; }
        public string AuctionPictures { get; set; }
        public int CategoryID { get; set; }
        public List<Category> Categories { get; set; }
        public int ID { get; set; }
    }
}
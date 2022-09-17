using DealDouble.Entities;
using DealDouble.Services;
using DealDouble.Web.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class AuctionsController : Controller
    {
        AuctionsService AuctionsService = new AuctionsService();
        CategoriesService categoriesService = new CategoriesService();
        SharedService SharedService = new SharedService();

        public ActionResult Index(int? CategoryID , string SearchTerm,int? PageNum)
        {
            var Model = new AuctionsListViewModel();
            Model.Auctions = AuctionsService.GetAllAuctions();
            Model.Categories = categoriesService.GetAllCategories();
            Model.PageTitle = "Auctions";
            Model.PageDescription = "Auctions Listing Page";
            Model.CategoryID = CategoryID;
            Model.SearchTerm = SearchTerm;
            Model.PageNum = PageNum;
            return View(Model);
        }
        public ActionResult AuctionsTable(string SearchTerm,int? CategoryID,int? PageNum)
        {
            var PageSize = 2;
            var Model = new AuctionsListViewModel();
            Model.Auctions = AuctionsService.SearchAuctions(CategoryID, SearchTerm, PageNum, PageSize);
            var AuctionCount = AuctionsService.GetAuctionsCount(SearchTerm,CategoryID);
            Model.Pager = new Pager(AuctionCount, PageNum, PageSize);
            Model.Categories = categoriesService.GetAllCategories();
            Model.CategoryID = CategoryID;
            Model.SearchTerm = SearchTerm;
            return PartialView(Model);
        }
        public ActionResult Create()
        {
            var Model = new CreateAuctionViewModel();
            Model.Categories = categoriesService.GetAllCategories();
            return PartialView(Model);
        }
        [HttpPost]
        public JsonResult Create(CreateAuctionViewModel Model)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                var auction = new Auction();
                auction.Title = Model.Title;
                auction.ActualAmount = Model.ActualAmount;
                auction.Description = Model.Description;
                auction.StartingTime = Model.StartingTime;
                auction.EndingTime = Model.EndingTime;
                auction.CategoryID = Model.CategoryID;
                if (!string.IsNullOrEmpty(Model.AuctionPictures))
                {
                    var PicturesIDs = Model.AuctionPictures.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
                    auction.AuctionPictures = new List<AuctionPicture>();
                    auction.AuctionPictures.AddRange(PicturesIDs.Select(x => new AuctionPicture() { PictureID = x }).ToList());
                }
                AuctionsService.SaveAuction(auction);
                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false , Error = "Unable to Save Auction please Enter a valid Fields" };
            }
            return result;
        }
        public ActionResult Edit(int ID)
        {
            var Model = new EditAuctionViewModel();
            var Auction = AuctionsService.GetAuctionByID(ID);
            Model.ID = Auction.ID;
            Model.Title = Auction.Title;
            Model.Description = Auction.Description;
            Model.StartingTime = Auction.StartingTime;
            Model.EndingTime = Auction.EndingTime;
            Model.CategoryID = Auction.CategoryID;
            Model.Categories = categoriesService.GetAllCategories();
            Model.ActualAmount = Auction.ActualAmount;
            Model.AuctionPicturesList = Auction.AuctionPictures;
            return PartialView(Model);
        }
        [HttpPost]
        public ActionResult Edit(EditAuctionViewModel Model)
        {
            var auction = new Auction();
            auction.Title = Model.Title;
            auction.Description = Model.Description;
            auction.ID = Model.ID;
            auction.StartingTime = Model.StartingTime;
            auction.EndingTime = Model.EndingTime;
            auction.ActualAmount = Model.ActualAmount;
            auction.CategoryID = Model.CategoryID;
            auction.AuctionPictures = Model.AuctionPicturesList;
            if (!string.IsNullOrEmpty(Model.AuctionPictures))
            {
                var PicturesIDs = Model.AuctionPictures.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
                auction.AuctionPictures = new List<AuctionPicture>();
                auction.AuctionPictures.AddRange(PicturesIDs.Select(x => new AuctionPicture() { AuctionID = auction.ID, PictureID = x }).ToList());
            }
            AuctionsService.UpdateAuction(auction);
            return RedirectToAction("AuctionsTable");
        }

        [HttpPost]
        public ActionResult Delete(Auction auction)
        {
            AuctionsService.DeleteAuction(auction);
            return RedirectToAction("AuctionsTable");
        }
        [HttpGet]
        public ActionResult Details(int ID)
        {
            var Model = new DetailsViewModel();
            Model.EntityID = (int)EntityEnums.Auction;
            Model.PageTitle = "Auction Details";
            Model.PageDescription = "Auction Details Page";
            Model.auction = AuctionsService.GetAuctionByID(ID);
            if (SharedService.GetComments(Model.EntityID, Model.auction.ID) != null && SharedService.GetComments(Model.EntityID, Model.auction.ID).Count() > 0)
            {
                Model.Comments = SharedService.GetComments(Model.EntityID, Model.auction.ID);
                Model.AvgRating = SharedService.GetComments(Model.EntityID, Model.auction.ID).Average(x => x.Rating);
            }
            Model.IsAuthenticated = User.Identity.IsAuthenticated;
            Model.BidAmount = Model.auction.ActualAmount + Model.auction.Bids.Sum(x => x.BidAmount);
            var LatestBidder = Model.auction.Bids.OrderByDescending(x => x.TimesTamp).FirstOrDefault();
            Model.LastBidder = LatestBidder != null ? LatestBidder.User : null;
            return View(Model);
        }
    }
}
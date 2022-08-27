using DealDouble.Entities;
using DealDouble.Services;
using DealDouble.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class AuctionsController : Controller
    {
        categoriesService AuctionsService = new categoriesService();
        CategoriesService categoriesService = new CategoriesService();
        public ActionResult Index()
        {
            var Model = new AuctionsListViewModel();
            Model.Auctions = AuctionsService.GetAllAuctions();
            Model.PageTitle = "Auctions";
            Model.PageDescription = "Auctions Listing Page";

            return View(Model);
        }
        public ActionResult AuctionsTable()
        {
            var Model = new AuctionsListViewModel();
            Model.Auctions = AuctionsService.GetAllAuctionsWithCategories();
            return PartialView(Model);
        }
        public ActionResult Create()
        {
            var Model = new CreateAuctionViewModel();
            Model.Categories = categoriesService.GetAllCategories();
            return PartialView(Model);
        }
        [HttpPost]
        public ActionResult Create(CreateAuctionViewModel Model)
        {
            var auction = new Auction();
            auction.Title = Model.Title;
            auction.ActualAmount = Model.ActualAmount;
            auction.Description = Model.Description;
            auction.StartingTime = Model.StartingTime;
            auction.EndingTime = Model.EndingTime;
            auction.CategoryID = Model.CategoryID;
            if (string.IsNullOrEmpty(Model.AuctionPictures))
            {
                var PicturesIDs = Model.AuctionPictures.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
                auction.AuctionPictures = new List<AuctionPicture>();
                auction.AuctionPictures.AddRange(PicturesIDs.Select(x => new AuctionPicture() { PictureID = x }).ToList());
            }
            AuctionsService.SaveAuction(auction);
            return RedirectToAction("AuctionsTable");
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
            auction.AuctionPictures = Model.AuctionPicturesList;
            auction.ActualAmount = Model.ActualAmount;
            auction.CategoryID = Model.CategoryID;
            if (!string.IsNullOrEmpty(Model.AuctionPictures))
            {
                var PicturesIDs = Model.AuctionPictures.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
                auction.AuctionPictures = new List<AuctionPicture>();
                auction.AuctionPictures.AddRange(PicturesIDs.Select(x => new AuctionPicture() { PictureID = x }).ToList());
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
            Model.PageTitle = "Auction Details";
            Model.PageDescription = "Auction Details Page";
            Model.auction = AuctionsService.GetAuctionByID(ID);
            return View(Model);
        }
    }
}
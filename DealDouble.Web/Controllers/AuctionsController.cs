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
        AuctionsService AuctionsService = new AuctionsService();
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
            Model.Auctions = AuctionsService.GetAllAuctions();
            return PartialView(Model);
        }
        public ActionResult Create()
        {
            return PartialView();
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
            var PicturesIDs = Model.AuctionPictures.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
            auction.AuctionPictures = new List<AuctionPicture>();
            auction.AuctionPictures.AddRange(PicturesIDs.Select(x => new AuctionPicture() { PictureID = x }).ToList());
            AuctionsService.SaveAuction(auction);
            return RedirectToAction("AuctionsTable");
        }
        public ActionResult Edit(int ID)
        {
            var Auction = AuctionsService.GetAuctionByID(ID);
            return PartialView(Auction);
        }
        [HttpPost]
        public ActionResult Edit(Auction auction)
        {
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
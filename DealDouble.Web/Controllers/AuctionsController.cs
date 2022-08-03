using DealDouble.Entities;
using DealDouble.Services;
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
            var Auctions = AuctionsService.GetAllAuctions();
            return View(Auctions);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Auction auction)
        {
            AuctionsService.SaveAuction(auction);
            return View();
        }
        public ActionResult Edit(int ID)
        {
            var Auction = AuctionsService.GetAuctionByID(ID);
            return View(Auction);
        }
        [HttpPost]
        public ActionResult Edit(Auction auction)
        {
            AuctionsService.UpdateAuction(auction);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int ID)
        {
            var Auction = AuctionsService.GetAuctionByID(ID);
            return View(Auction);
        }
        [HttpPost]
        public ActionResult Delete(Auction auction)
        {
            AuctionsService.DeleteAuction(auction);
            return View("Index");
        }
    }
}
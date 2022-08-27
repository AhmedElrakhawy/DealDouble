using DealDouble.Services;
using DealDouble.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class HomeController : Controller
    {
        categoriesService auctionsService = new categoriesService();
        public ActionResult Index()
        {
            var Model = new AuctionsViewModel();
            Model.PageTitle = "Home Page";
            Model.PageDescription = "Home Page";
            Model.AllAuctions = auctionsService.GetAllAuctionsWithPictures();
            Model.promotedAuctions = auctionsService.GetpromotedAuctions();
            return View(Model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
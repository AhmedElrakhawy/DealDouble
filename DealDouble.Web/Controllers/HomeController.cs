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
        AuctionsService auctionsService = new AuctionsService();
        CategoriesService categoriesService = new CategoriesService(); 
        public ActionResult Index(int? CategoryID , string SearchTerm, int? PageNum)
        {
            var Model = new AuctionsViewModel();
            Model.AllAuctions = auctionsService.GetAllAuctions();
            Model.PageTitle = "Home Page";
            Model.PageDescription = "Home Page Description";
            Model.Categories = categoriesService.GetAllCategories();
            Model.CategoryID = Model.CategoryID;
            Model.SearchTerm = SearchTerm;
            return View(Model);
        }
        public ActionResult ListingAuctions(int? CategoryID, string SearchTerm , int? PageNum)
        {
            var PageSize = 10;
            var Model = new AuctionsViewModel();
            Model.PageTitle = "Home Page";
            Model.PageDescription = "Home Page Description";
            Model.AllAuctions = auctionsService.SearchAuctions(CategoryID, SearchTerm, PageNum, PageSize);
            Model.Categories = categoriesService.GetAllCategories();
            Model.CategoryID = Model.CategoryID;
            Model.SearchTerm = SearchTerm;
            var AuctionCount = auctionsService.GetAuctionsCount(SearchTerm, CategoryID);
            Model.Pager = new Pager(AuctionCount, PageNum, PageSize);
            return PartialView(Model);
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
﻿using DealDouble.Entities;
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
    public class BidsController : Controller
    {
        BidsService Service = new BidsService();
        
        public ActionResult AllBids(int? SearchbyBidAmount,string SearchByCategory, int? PageNum)
        {
            var Model = new BidsViewModel();
            Model.Bids = Service.AllBids();
            Model.SearchbyBidAmount = SearchbyBidAmount;
            Model.SearchByCategory = SearchByCategory;
            Model.PageNum = PageNum;
            return View(Model);
        }
        public ActionResult BidsTable(int? SearchbyBidAmount, string SearchByCategory, int? PageNum)
        {
            var PageSize = 2;
            var Model = new BidsViewModel();
            Model.SearchbyBidAmount = SearchbyBidAmount;
            Model.SearchByCategory = SearchByCategory;
            Model.Bids =  Service.SearchBids(SearchByCategory, SearchbyBidAmount, PageNum, PageSize);
            var BidsCount = Service.GetBidsCount(SearchByCategory, SearchbyBidAmount);
            Model.Pager = new Pager(BidsCount, PageNum, PageSize);
            return PartialView(Model);
        }
        [HttpPost]
        public JsonResult Bid(int ID , int BidAmount)
        {
            var Result = new JsonResult();
            Result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (User.Identity.IsAuthenticated)
            {
                var bid = new Bid();
                bid.AuctionID = ID;
                bid.UserID = User.Identity.GetUserId();
                bid.BidAmount = BidAmount;
                bid.TimesTamp = DateTime.Now;
                var BidResult = Service.AddiBid(bid);
                if (BidResult)
                {
                    Result.Data = new { Success = true, Message = "Your Bid Has been Submitted Correctly.." };
                }
                else
                {
                    Result.Data = new { Success = false, Message = "Something went wrong with your bid please try again later.." };
                }
            }
            else
            {
                Result.Data = new { Success = false, Message = "You Must be Login First To Submit a New Bid.." };
            }
            return Result;
        }
    }
}
using DealDouble.Entities;
using DealDouble.Services;
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

        [HttpPost]
        public JsonResult Bid(int ID)
        {
            var Result = new JsonResult();
            Result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (User.Identity.IsAuthenticated)
            {
                var bid = new Bid();
                bid.AuctionID = ID;
                bid.UserID = User.Identity.GetUserId();
                bid.BidAmount = 100;
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
using DealDouble.Services;
using DealDouble.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DealDouble.Web.Controllers
{
    public class DashboardController : Controller
    {
        DashboardService dashboardService = new DashboardService();

        private DealDoubleUserManager _userManager;
        private DealDoubleRoleManager _roleManager;

        public DashboardController()
        {
        }

        public DashboardController(DealDoubleUserManager userManager, DealDoubleRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }
        public DealDoubleUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<DealDoubleUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public DealDoubleRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<DealDoubleRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ActionResult Index()
        {
            var Model = new IndexDashboardViewModel();
            Model.UsersCount = dashboardService.UsersCount();
            Model.AuctionsCount = dashboardService.GetAuctionsCount();
            Model.BidsCount = dashboardService.GetBidsCount();
            return View(Model);
        }
        public ActionResult Users(string SearchTerm , string RoleID , int? PageNum)
        {
            var Model = new UsersDashboardViewModel();
            Model.PageTitle = "Users";
            Model.PageDescription = "Dashboard Listing Users";
            Model.Roles = RoleManager.Roles.ToList();
            Model.SearchTerm = SearchTerm;
            Model.RoleID = RoleID;
            Model.PageNum = PageNum;
            return View(Model);
        }
        public ActionResult UsersTable(string SearchTerm, string RoleID, int? PageNum)
        {
            int PageSize = 3;
            var Model = new UsersTableViewModel();
            Model.RoleID = RoleID;
            Model.SearchTerm = SearchTerm;
            var users = UserManager.Users;
            if (!string.IsNullOrEmpty(RoleID))
            {
                users = users.Where(x => x.Roles.Any( r=> r.RoleId == RoleID));
            }
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                users = users.Where(x => x.Email.ToLower().Contains(SearchTerm.ToLower()));
            }
            PageNum = PageNum ?? 1;
            int SkipCount = (PageNum.Value - 1) * PageSize;

            Model.Users = users.OrderBy(x => x.Email).Skip(SkipCount).Take(PageSize).ToList();
            Model.Pager = new Pager(users.Count(), PageNum, PageSize);
            return PartialView(Model);
        }
    }
}
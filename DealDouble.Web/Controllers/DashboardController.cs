using DealDouble.Entities;
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
        public ActionResult Users(string SearchTerm, string RoleID, int? PageNum)
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
                users = users.Where(x => x.Roles.Any(r => r.RoleId == RoleID));
            }
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                users = users.Where(x => x.Email.ToLower().Contains(SearchTerm.ToLower())
                || x.UserName.ToLower().Contains(SearchTerm.ToLower()));
            }
            PageNum = PageNum ?? 1;
            int SkipCount = (PageNum.Value - 1) * PageSize;

            Model.Users = users.OrderBy(x => x.Email).Skip(SkipCount).Take(PageSize).ToList();
            Model.Pager = new Pager(users.Count(), PageNum, PageSize);
            return PartialView(Model);
        }
        public ActionResult Roles(string SearchTerm, int? PageNum)
        {
            var Model = new RolesViewModel();
            Model.PageTitle = "Roles";
            Model.PageDescription = "Dashboard Listing Roles";
            Model.Roles = RoleManager.Roles.ToList();
            Model.SearchTerm = SearchTerm;
            Model.PageNum = PageNum;
            return View(Model);
        }
        public ActionResult RolesTable(string SearchTerm, int? PageNum)
        {
            int PageSize = 1;
            var Model = new RolesTableViewModel();
            Model.SearchTerm = SearchTerm;
            var Roles = RoleManager.Roles;
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Roles = Roles.Where(x => x.Name.ToLower().Contains(SearchTerm.ToLower()));
            }
            PageNum = PageNum ?? 1;
            int SkipCount = (PageNum.Value - 1) * PageSize;

            Model.Roles = Roles.OrderBy(x => x.Name).Skip(SkipCount).Take(PageSize).ToList();
            Model.Pager = new Pager(Roles.Count(), PageNum, PageSize);
            return PartialView(Model);
        }
        public ActionResult UserDetails(string UserID, bool IsPartial = false)
        {
            var Model = new UserDetailsViewModel();
            var User = UserManager.FindById(UserID);
            if (User != null)
            {
                Model.User = User;
            }
            if (IsPartial || Request.IsAjaxRequest())
            {
                return PartialView("_UserDetails", Model);
            }
            else
            {
                return View(Model);
            }

        }
        public ActionResult UserRoles(string UserID)
        {
            var Model = new UserRolesViewModel();
            Model.AvailableRoles = RoleManager.Roles.ToList();
            if (UserID != null)
            {
                Model.User = UserManager.FindById(UserID);
                if (Model.User != null)
                {
                    Model.UserRoles = Model.User.Roles.Select(x => Model.AvailableRoles.Find(avr => avr.Id == x.RoleId)).ToList();
                }
            }
            return PartialView("_UserRoles", Model);

        }
        public ActionResult AssignUserRoles(string UserID, string roleId)
        {
            if (!string.IsNullOrEmpty(UserID) && !string.IsNullOrEmpty(roleId))
            {
                var RoleName = RoleManager.FindById(roleId).Name;
                if (RoleName != null)
                {
                    UserManager.AddToRole(UserID, RoleName);
                }
            }
            return RedirectToAction("UserRoles", new { UserID = UserID });
        }

        public ActionResult DeleteUserRole(string UserID, string roleId)
        {
            if (!string.IsNullOrEmpty(UserID) && !string.IsNullOrEmpty(roleId))
            {
                var RoleName = RoleManager.FindById(roleId).Name;
                if (RoleName != null)
                {
                    UserManager.RemoveFromRole(UserID, RoleName);
                }
            }
            return RedirectToAction("UserRoles", new { UserID = UserID });
        }
        [HttpPost]
        public ActionResult UserUpdate(UserUpdateViewModel Model)
        {
            var User = new DealDoubleUser();
            if (ModelState.IsValid)
            {
                User = UserManager.FindById(Model.ID);
                User.FirstName = Model.FirstName;
                User.LastName = Model.LastName;
                User.UserName = Model.UserName;
                User.PhoneNumber = Model.PhoneNumber;
                User.Country = Model.Country;
                User.Age = Model.Age;
                User.FullName = Model.FirstName + "" + Model.LastName;
                User.ImageUrl = Model.ImageUrl;
                User.Address = Model.Address;
                if (User != null)
                {
                    UserManager.Update(User);
                }
                return PartialView(Model);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public JsonResult UserDelete(string ID)
        {
            var Result = new JsonResult();
            var User = UserManager.FindById(ID);
            if (User != null)
            {
                UserManager.Delete(User);
                Result.Data = new { Success = true };
            }
            else
            {
                Result.Data = new { Success = false };
            }
            return Result;
        }
    }
}
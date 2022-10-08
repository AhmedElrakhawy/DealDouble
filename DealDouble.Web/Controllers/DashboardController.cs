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
        CommentsService commentsService = new CommentsService();
        CategoriesService categoriesService = new CategoriesService();
        AuctionsService auctionsService = new AuctionsService();
        BidsService bidsService = new BidsService();
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
            Model.RolesCount = RoleManager.Roles.Count();
            Model.commentsCount = commentsService.CommentsCount();
            Model.CategoriesCount = categoriesService.CategoriesCount();
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
        public ActionResult CreateRole()
        {
            var Model = new CreateRoleViewModel();
            Model.PageTitle = "Roles";
            Model.PageDescription = "Creating Role";
            return PartialView(Model);
        }
        [HttpPost]
        public ActionResult CreateRole(CreateRoleViewModel model)
        {
            var NewRole = new IdentityRole();
            NewRole.Name = model.Name;
            RoleManager.Create(NewRole);
            return RedirectToAction("RolesTable");
        }
        public ActionResult RolesTable(string SearchTerm, int? PageNum)
        {
            int PageSize = 3;
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
        public ActionResult UserComments(string UserID)
        {
            var Model = new UserCommentsViewModel();
            if (UserID != null)
            {
                Model.User = UserManager.FindById(UserID);
                if (Model.User != null)
                {
                    Model.UserComments = commentsService.UserComments(UserID);
                }
            }
            return PartialView("_UserComments", Model);

        }
        public ActionResult RoleDetails(string roleId)
        {
            var Model = new RoleDetailsViewModel();
            Model.Role = RoleManager.FindById(roleId);
            Model.Users = UserManager.Users.Where(x => x.Roles.Any(r => r.RoleId == roleId)).ToList();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_RoleDetails", Model);
            }
            else
            {
                return View(Model);
            }
        }
        [HttpPost]
        public JsonResult UpdateRole(IdentityRole role)
        {
            var Result = new JsonResult();
            if (role != null)
            {
                RoleManager.Update(role);
                Result.Data = new { Success = true };
                return Result;
            }
            else
            {
                Result.Data = new { Success = false };
                return Result;
            }
        }
        [HttpPost]
        public ActionResult DeleteRole(string RoleID)
        {
            if (!string.IsNullOrEmpty(RoleID))
            {
                var Role = RoleManager.FindById(RoleID);
                if (Role != null)
                {
                   RoleManager.Delete(Role);
                }
            }
            return RedirectToAction("Roles");
        }
        public ActionResult RoleUsers(string roleId)
        {
            var Model = new RoleDetailsViewModel();
            if (!string.IsNullOrEmpty(roleId))
            {
                Model.Role = RoleManager.FindById(roleId);
                if (Model.Role != null)
                {
                    Model.Users = UserManager.Users.Where(x => x.Roles.Any(r => r.RoleId == roleId)).ToList();
                }
            }
            return PartialView("_RoleUsers", Model);
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
        public ActionResult UserBids(string UserID)
        {
            var Model = new UserBidsViewModel();
            Model.Bids = bidsService.UserBids(UserID);
            return PartialView("_UserBids",Model);
        }
        public ActionResult Comments(int? PageNum)
        {
            int PageSize = 3;
            var Model = new CommentsViewModel();
            Model.Comments = dashboardService.GetAllComments(PageNum,PageSize);
            Model.Pager = new Pager(dashboardService.GetAllCommentsCount(), PageNum, PageSize);
            return View(Model);
        }
    }
}
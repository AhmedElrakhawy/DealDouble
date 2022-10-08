using DealDouble.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DealDouble.Web.ViewModels
{
    public class IndexDashboardViewModel : PageViewModel
    {

        public int UsersCount { get; set; }
        public int AuctionsCount { get; set; }
        public int BidsCount { get; set; }
        public object RolesCount { get;  set; }
        public object CategoriesCount { get;  set; }
        public object commentsCount { get; set; }
    }
    public class UsersDashboardViewModel : PageViewModel
    {
        public List<IdentityRole> Roles { get;  set; }
        public string RoleID { get; set; }
        public string SearchTerm { get;  set; }
        public int? PageNum { get; set; }
    }
    public class UsersTableViewModel : PageViewModel
    {
        public List<DealDoubleUser> Users { get;set; }
        public Pager Pager { get;set; }
        public string RoleID { get; set; }
        public string SearchTerm { get; set; }
    }
    public class RolesViewModel : PageViewModel
    {
        public Pager Pager { get; set; }
        public string SearchTerm { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public int? PageNum { get; set; }
    }
    public class CreateRoleViewModel : PageViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class CommentsViewModel : PageViewModel
    {
        public Pager Pager { get; set; }
        public List<Comment> Comments { get; set; }
    }
    public class RoleDetailsViewModel : PageViewModel
    {
        public List<DealDoubleUser> Users { get; set; }
        public IdentityRole Role { get; set; }
    }
    public class RolesTableViewModel : PageViewModel
    {
        public List<IdentityRole> Roles { get; set; }
        public Pager Pager { get; set; }
        public string SearchTerm { get; set; }
    }
    public class UserDetailsViewModel : PageViewModel
    {
        public DealDoubleUser User { get; set; }
    }
    public class UserBidsViewModel : PageViewModel
    {
        public List<Bid> Bids { get; set; }
    }
    public class UserRolesViewModel : PageViewModel
    {
        public List<IdentityRole> AvailableRoles { get; set; }
        public List<IdentityRole> UserRoles { get; set; }
        public DealDoubleUser User { get;  set; }
    }
    public class UserCommentsViewModel : PageViewModel
    {
        public List<Comment> UserComments { get; set; }
        public List<string> AuctionsName { get; set; }
        public DealDoubleUser User { get; set; }
    }
    public class UserUpdateViewModel : PageViewModel
    {
        public string ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Country { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
    }
}
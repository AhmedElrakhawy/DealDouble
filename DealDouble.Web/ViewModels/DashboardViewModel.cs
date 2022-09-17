using DealDouble.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealDouble.Web.ViewModels
{
    public class IndexDashboardViewModel : PageViewModel
    {
        public int UsersCount { get; set; }
        public int AuctionsCount { get; set; }
        public int BidsCount { get; set; }
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
}
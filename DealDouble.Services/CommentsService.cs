using DealDouble.Data;
using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Services
{
    public class CommentsService
    {
        public int CommentsCount()
        {
            using (var Context = new DealDoubleDBContext())
            {
               return  Context.Comments.Count();
            }
        }
        public List<Comment> UserComments(string UserID)
        {
            using (var Context = new DealDoubleDBContext())
            {
                return Context.Comments.Where(x => x.UserId == UserID)
                    .OrderByDescending(c => c.PostedOn)
                    .ToList();
            }
        }
    }
}

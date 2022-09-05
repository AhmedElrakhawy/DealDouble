using DealDouble.Data;
using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Services
{
    public class SharedService
    {
        public int SavePicture(Picture picture)
        {
            using (var Context = new DealDoubleDBContext())
            {
                Context.Pictures.Add(picture);
                Context.SaveChanges();
                return picture.ID;
            }
        }
        public bool AddingComment(Comment comment)
        {
            using (var Context = new DealDoubleDBContext())
            {
                Context.Comments.Add(comment);
                return Context.SaveChanges() > 0;
            }
        }

        public List<Comment> GetComments(int entityID, int recordID)
        {
            using (var Context = new DealDoubleDBContext())
            {
                return  Context.Comments.Where(x => x.EntityID == entityID && x.RecordID == recordID).ToList();
            }
        }
    }
   
}

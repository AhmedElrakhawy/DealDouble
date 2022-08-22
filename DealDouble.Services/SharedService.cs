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
            }
            return picture.ID;
        }
    }
}

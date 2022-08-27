using DealDouble.Data;
using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Services
{
    public class CategoriesService
    {
        public List<Category> GetAllCategories()
        {
            using (var Context = new DealDoubleDBContext())
            {
                return Context.Categories.Include(x => x.Auctions).ToList();
            }
        }

        public void saveCategory(Category category)
        {
            using (var Context = new DealDoubleDBContext())
            {
                Context.Categories.Add(category);
                Context.SaveChanges();
            }
        }

        public void UpdateCategory(Category category)
        {
            using (var Context = new DealDoubleDBContext())
            {
                Context.Entry(category).State = EntityState.Modified;
                Context.SaveChanges();
            }
        }

        public Category GetCategoryByID(int ID)
        {
            using (var Context = new DealDoubleDBContext())
            {
                return Context.Categories.Include(x => x.Auctions).FirstOrDefault(x => x.ID == ID);
            }
        }

        public void DeleteCategory(Category category)
        {
            using (var Context = new DealDoubleDBContext())
            {
                Context.Entry(category).State = EntityState.Deleted;
                Context.SaveChanges();
            }
        }
    }
}

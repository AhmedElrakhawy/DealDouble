using DealDouble.Entities;
using DealDouble.Services;
using DealDouble.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class CategoriesController : Controller
    {
        CategoriesService categoriesService = new CategoriesService();
        public ActionResult Index()
        {
            var Model = new CategoriesListingViewModel();
            Model.Categories = categoriesService.GetAllCategories();
            Model.PageTitle = "Categories";
            Model.PageDescription = "Categories Listing Page";

            return View(Model);
        }
        public ActionResult CategoriesTable()
        {
            var Model = new CategoriesListingViewModel();
            Model.Categories = categoriesService.GetAllCategories();
            return PartialView(Model);
        }
        public ActionResult Create()
        {
            var Model = new CreateCategoryViewModel();
            return PartialView(Model);
        }
        [HttpPost]
        public ActionResult Create(CreateCategoryViewModel Model)
        {
            var category = new Category();
            category.Name = Model.Name;
            category.Description = Model.Description;
            categoriesService.saveCategory(category);
            return RedirectToAction("CategoriesTable");
        }
        public ActionResult Edit(int ID)
        {
            var Model = new EditCategoryViewModel();
            var Category = categoriesService.GetCategoryByID(ID);
            Model.ID = Category.ID;
            Model.Name = Category.Name;
            Model.Description = Category.Description;
            return PartialView(Model);
        }
        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel Model)
        {
            var category = new Category();
            category.Name = Model.Name;
            category.Description = Model.Description;
            category.ID = Model.ID;
            categoriesService.UpdateCategory(category);
            return RedirectToAction("CategoriesTable");
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            categoriesService.DeleteCategory(category);
            return RedirectToAction("CategoriesTable");
        }
    }
}
using DealDouble.Entities;
using DealDouble.Services;
using DealDouble.Web.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class SharedController : Controller
    {
        SharedService Service = new SharedService();
        [HttpPost]
        public JsonResult UploadPictures()
        {
            var JsonData = new JsonResult();
            //JsonData.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var Pictures = Request.Files;
            var DbPicture = new Picture();
            var PicturesJson = new List<object>();
            for (int i = 0; i < Pictures.Count; i++)
            {
                var Picture = Pictures[i];
                var FileName = Guid.NewGuid() + Path.GetExtension(Picture.FileName);
                var path = Server.MapPath("/Content/Images/") + FileName;
                Picture.SaveAs(path);
                DbPicture.URL = FileName;
                var PicID =  Service.SavePicture(DbPicture);
                PicturesJson.Add(new { ID = PicID, URL = FileName });
            }
            JsonData.Data = PicturesJson;
            return JsonData;
        }
        public JsonResult LeaveComment(CommentViewModel Model)
        {
            var Result = new JsonResult();
            try
            {
                var comment = new Comment();
                comment.Text = Model.Text;
                comment.EntityID = Model.EntityID;
                comment.RecordID = Model.RecordID;
                comment.Rating = Model.Rating;
                comment.PostedOn = DateTime.Now;
                comment.UserID = User.Identity.GetUserId();

                Result.Data = new { Success = Service.AddingComment(comment) };
            }
            catch (Exception ex)
            {
                Result.Data = new { Success = false , Message = ex.Message };
            }
            return Result;
        }
    }
}
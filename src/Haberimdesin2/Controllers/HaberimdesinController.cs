using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Haberimdesin2.Models;
using Haberimdesin2.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace Haberimdesin2.Controllers
{
    public class HaberimdesinController : Controller
    {
        private ApplicationDbContext _context;
        private IHostingEnvironment _environment;
        private UserManager<ApplicationUser> _userManager;
        public HaberimdesinController(ApplicationDbContext context, IHostingEnvironment env, UserManager<ApplicationUser> sgn)
        {
            _context = context;
            _environment = env;
            _userManager = sgn;
        }
        // GET: Haberimdesin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Haberimdesin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Haberimdesin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Haberimdesin/Create
        [HttpPost]
        public async Task<JsonResult> CreateNews()
        {

            var files = Request.Form.Files;
            string title = Request.Form["haberHeader"];
            string headline = Request.Form["haberHeadline"];
            string detail = Request.Form["haberDetail"];
            float latitude = float.Parse(Request.Form["latitude"]);
            float longitude = float.Parse(Request.Form["longitude"]);
            int categoryId = int.Parse(Request.Form["CategoryID"]);
            DateTime time = DateTime.Now;

            string userId = _userManager.GetUserId(User);
            HaberEntity haber = new HaberEntity { Id = userId, Title = title, HeadLine = headline, Detail = detail, Latitude = latitude, Longitude = longitude, TimeStamp = time, Like = 0, Dislike = 0, CategoryID = categoryId };

            _context.Haber.Add(haber);
            _context.SaveChanges();

            int haberId = haber.HaberID;

            string haberImgURL = Path.Combine(new string[] { _environment.WebRootPath, "images", "haber" + haberId });

            if (!Directory.Exists(haberImgURL))
                Directory.CreateDirectory(haberImgURL);
            for (int i = 0; i < files.Count; i++)
            {
                IFormFile file = files.ElementAt(i);
                if (i == 0 && file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(haberImgURL, file.FileName), FileMode.Create))
                    {
                        haber.PrimaryImgURL =  "/images/" +"haber"+haberId+"/"+ file.FileName;
                        await file.CopyToAsync(fileStream);
                    }

                }
                else
                {
                    using (var fileStream = new FileStream(Path.Combine(haberImgURL, file.FileName), FileMode.Create))
                    {
                       string imgURL  = "/images/" + "haber" + haberId + "/" + file.FileName;
                        await file.CopyToAsync(fileStream);
                        ImageEntity image = new ImageEntity { HaberID = haberId, UserID = userId, ImageURL = imgURL };
                        _context.Image.Add(image);
                    }


                }
            }
            _context.SaveChanges();
            return Json(new { });
        }
        [HttpGet]
        public JsonResult getCategories()
        {
            var categories = _context.Category.ToList();
            return Json(new { categories });
        }

        [HttpGet]
        public JsonResult getNewsByID(int id)
        {

            var newsList = _context.Haber.Where(h => h.CategoryID == id).Include(h => h.user).ToList();
            

            return Json(new { newsList });
        }

        // GET: Haberimdesin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Haberimdesin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Haberimdesin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Haberimdesin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




    }
}
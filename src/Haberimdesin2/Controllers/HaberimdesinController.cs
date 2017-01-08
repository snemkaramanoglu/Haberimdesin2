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
using System.Globalization;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

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
        public JsonResult UpdateHaber()
        {
            int haberID = int.Parse(Request.Form["id"]);
            string haberTitle = Request.Form["title"];
            string haberHeadline = Request.Form["headline"];
            string haberDetail = Request.Form["detail"];
            int haberCategoryID = int.Parse(Request.Form["categoryID"]);
            var result = _context.Haber.SingleOrDefault(h => h.HaberID == haberID);
            if (result != null)
            {
                result.HeadLine = haberHeadline;
                result.Title = haberTitle;
                result.Detail = haberDetail;
                result.CategoryID = haberCategoryID;
                _context.SaveChanges();
            }
            return Json(new { });
        }


        [HttpPost]
        public JsonResult LikeComment()
        {


            int commentId = int.Parse(Request.Form["id"]);
            string userId = _userManager.GetUserId(User);
            LikeCommentEntity lComment = new LikeCommentEntity
            {
                UserID = userId,
                CommentID = commentId,
            };
            _context.LikeComment.Add(lComment);
            _context.SaveChanges();
            return Json(new { });
        }
        [HttpPost]
        public JsonResult DislikeComment()
        {


            int commentId = int.Parse(Request.Form["id"]);
            string userId = _userManager.GetUserId(User);
            DislikeCommentEntity dComment = new DislikeCommentEntity
            {
                UserID = userId,
                CommentID = commentId,
            };
            _context.DislikeComment.Add(dComment);
            _context.SaveChanges();
            return Json(new { });
        }
        [HttpPost]
        public JsonResult CancelLikeComment()
        {
            int commentId = int.Parse(Request.Form["id"]);
            string userId = _userManager.GetUserId(User);
            var itemToRemove = _context.LikeComment.SingleOrDefault(x => x.UserID == userId && x.CommentID == commentId);
            if (itemToRemove != null)
            {
                _context.LikeComment.Remove(itemToRemove);
                _context.SaveChanges();

            }
            return Json(new { });
        }
        [HttpPost]
        public JsonResult CancelDislikeComment()
        {
            int commentId = int.Parse(Request.Form["id"]);
            string userId = _userManager.GetUserId(User);
            var itemToRemove = _context.DislikeComment.SingleOrDefault(x => x.UserID == userId && x.CommentID == commentId);
            if (itemToRemove != null)
            {
                _context.DislikeComment.Remove(itemToRemove);
                _context.SaveChanges();

            }
            return Json(new { });
        }

        [HttpPost]
        public JsonResult DislikeNews()
        {


            int haberId = int.Parse(Request.Form["HaberId"]);
            string userId = Request.Form["UserId"];
            DislikeHaberEntity dHaber = new DislikeHaberEntity
            {
                UserID = userId,
                HaberID = haberId,
            };
            _context.DislikeHaber.Add(dHaber);
            _context.SaveChanges();
            return Json(new { });
        }
        [HttpPost]
        public JsonResult LikeNews()
        {


            int haberId = int.Parse(Request.Form["HaberId"]);
            string userId = Request.Form["UserId"];
            LikeHaberEntity lHaber = new LikeHaberEntity
            {
                UserID = userId,
                HaberID = haberId,
            };
            var itemsAlreadyIn = _context.LikeHaber.Where(x => x.UserID == userId && x.HaberID == haberId).ToList();
            if (itemsAlreadyIn.Count > 0) return Json(new { });
            _context.LikeHaber.Add(lHaber);
            _context.SaveChanges();
            return Json(new { });
        }

        [HttpPost]
        public JsonResult CancelHaberById()
        {
            int haberId = int.Parse(Request.Form["id"]);
            var itemToRemove = _context.Haber.SingleOrDefault(x => x.HaberID == haberId);
            if (itemToRemove != null)
            {
                _context.Haber.Remove(itemToRemove);
                _context.SaveChanges();

            }
            return Json(new { });
        }


        [HttpPost]
        public JsonResult CancelDislikeNews()
        {
            int haberId = int.Parse(Request.Form["HaberId"]);
            string userId = Request.Form["UserId"];
            var itemsToRemove = _context.DislikeHaber.Where(x => x.UserID == userId && x.HaberID == haberId).ToList();
            for (int i = 0; i < itemsToRemove.Count; i++)
            {
                _context.DislikeHaber.Remove(itemsToRemove[i]);
            }
            if (itemsToRemove.Count > 0) _context.SaveChanges();
            return Json(new { });
        }

        [HttpPost]
        public JsonResult CancelLikeNews()
        {
            int haberId = int.Parse(Request.Form["HaberId"]);
            string userId = Request.Form["UserId"];
            var itemsToRemove = _context.LikeHaber.Where(x => x.UserID == userId && x.HaberID == haberId).ToList();
            for (int i = 0; i < itemsToRemove.Count; i++)
            {
                _context.LikeHaber.Remove(itemsToRemove[i]);
            }
            if (itemsToRemove.Count > 0) _context.SaveChanges();
            return Json(new { });
        }
        [HttpPost]
        public JsonResult CreateComment()
        {
            string icerik = Request.Form["yorumIcerik"];
            int haberId = int.Parse(Request.Form["haberID"]);

            DateTime time = DateTime.Now;
            string userId = Request.Form["UserId"];
            CommentEntity comment = new CommentEntity
            {
                UserID = userId,
                HaberID = haberId,
                Content = icerik,
                TimeStamp = time,
            };
            _context.Comment.Add(comment);
            _context.SaveChanges();
            return Json(new { });
        }
        [HttpPost]
        public async Task<JsonResult> CreateNews()
        {

            var files = Request.Form.Files;
            string title = Request.Form["haberHeader"];
            string headline = Request.Form["haberHeadline"];
            string detail = Request.Form["haberDetail"];
            string userId = Request.Form["name"];

            float latitude = float.Parse(Request.Form["latitude"], CultureInfo.InvariantCulture);
            float longitude = float.Parse(Request.Form["longitude"], CultureInfo.InvariantCulture);
            int categoryId = int.Parse(Request.Form["CategoryID"]);
            DateTime time = DateTime.Now;

            if (String.IsNullOrEmpty(userId)) userId = _userManager.GetUserId(User);
            HaberEntity haber = new HaberEntity { Id = userId, Title = title, HeadLine = headline, Detail = detail, Latitude = latitude, Longitude = longitude, TimeStamp = time, CategoryID = categoryId };

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
                        haber.PrimaryImgURL = "/images/" + "haber" + haberId + "/" + file.FileName;
                        await file.CopyToAsync(fileStream);
                    }

                }
                else
                {
                    using (var fileStream = new FileStream(Path.Combine(haberImgURL, file.FileName), FileMode.Create))
                    {
                        string imgURL = "/images/" + "haber" + haberId + "/" + file.FileName;
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
        public JsonResult getUserID()
        {
            var usID = _userManager.GetUserId(User);
            return Json(new { usID });
        }
        [HttpGet]
        public JsonResult getUserDetail(string id)
        {
            var user = _context.ApplicationUser.Where(x => x.Id == id).Single();
            if (user != null)
                return Json(new { user });
            else return Json(new { });
        }
        [HttpPost]
        public JsonResult sendEmail()
        {
            string email = Request.Form["email"];
            var sifre = _context.ApplicationUser.Where(u => u.Email == email).Select(u => u.UserPass).ToList()[0];
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Haberimdesin", "webmaster@haberimdesin.com"));
            emailMessage.To.Add(new MailboxAddress("Alici", email));
            emailMessage.Subject = "Sifre hatirlatma";
            // here your metion body type "Plane"  "html" type. i am using "html"
            emailMessage.Body = new TextPart("plain") { Text = @"Sifreniz : " + sifre };

            SmtpClient smtp = new SmtpClient();
            // here you mention that your email port, email host & ssl enable-disable(true & false) 
          
            smtp.Connect("smtpout.europe.secureserver.net", 3535, false);
            // here your mention your sending address and password 
            smtp.Authenticate("webmaster@haberimdesin.com", "Haber@26");
            smtp.Send(emailMessage);
            // discount your connecton
            smtp.Disconnect(true);
            return Json(new { });
        }


        [HttpGet]
        public JsonResult getCategories()
        {
            var categories = _context.Category.ToList();
            return Json(new { categories });
        }

        [HttpGet]
        public JsonResult getAllHaberLikes()
        {


            var haberLikeList = _context.LikeHaber.ToList();


            return Json(new { haberLikeList });
        }
        [HttpGet]
        public JsonResult getAllHaberDislikes()
        {

            var haberDislikeList = _context.DislikeHaber.ToList();


            return Json(new { haberDislikeList });
        }
        [HttpGet]
        public JsonResult getAllCommentDislikes()
        {

            var commentDislikeList = _context.DislikeComment.ToList();


            return Json(new { commentDislikeList });
        }
        [HttpGet]
        public JsonResult getAllCommentLikes()
        {

            var commentLikeList = _context.LikeComment.ToList();


            return Json(new { commentLikeList });
        }
        [HttpGet]
        public JsonResult getAllNews()
        {

            var newsList = _context.Haber.Include(h => h.user).ToList(); //niye saçmaladý


            return Json(new { newsList });
        }
        [HttpGet]
        public JsonResult getNewsByUserID(string id)
        {

            var newsList = _context.Haber.Where(h => h.user.Id == id).ToList();
            string[] newsURLs = new string[newsList.Count];
            List<int> haberLikes = new List<int>();
            List<int> haberDislikes = new List<int>();
            List<int> yorumCounter = new List<int>();
            for (int i = 0; i < newsList.Count; i++)
            {
                int likeCount = _context.LikeHaber.Where(h => h.HaberID == newsList[i].HaberID).ToArray().Length;
                int dislikeCount = _context.DislikeHaber.Where(h => h.HaberID == newsList[i].HaberID).ToArray().Length;
                int yorumCount = _context.Comment.Where(c => c.HaberID == newsList[i].HaberID).ToArray().Length;
                newsURLs[i] = "https://www.haberimdesin.com/#?hID=" + newsList[i].HaberID;
                haberLikes.Add(likeCount);
                haberDislikes.Add(dislikeCount);
                yorumCounter.Add(yorumCount);
            }

            return Json(new { newsList, haberLikes, haberDislikes, newsURLs, yorumCounter });
        }




        [HttpGet]
        public JsonResult getNewsByID(int id)
        {
            //List<FeaturedHaber> haberList = new List<FeaturedHaber>();

            var haberList = _context.Haber.Where(h => h.CategoryID == id).Include(h => h.user).ToArray();
            string[] newsURLs = new string[haberList.Length];
            List<int> haberLikes = new List<int>();
            List<int> haberDislikes = new List<int>();
            List<int> yorumCounter = new List<int>();
            for (int i = 0; i < haberList.Length; i++)
            {
                int likeCount = _context.LikeHaber.Where(h => h.HaberID == haberList[i].HaberID).ToArray().Length;
                int dislikeCount = _context.DislikeHaber.Where(h => h.HaberID == haberList[i].HaberID).ToArray().Length;
                int yorumCount = _context.Comment.Where(c => c.HaberID == haberList[i].HaberID).ToArray().Length;
                newsURLs[i] = "https://www.haberimdesin.com/#?hID=" + haberList[i].HaberID;
                haberLikes.Add(likeCount);
                haberDislikes.Add(dislikeCount);
                yorumCounter.Add(yorumCount);
            }
            //for (int i = 0; i < newsList.Length; i++)
            //{
            //    int like = _context.LikeHaber.Where(l => l.HaberID == newsList[i].HaberID).Count();
            //    int dislike = _context.DislikeHaber.Where(d => d.HaberID == newsList[i].HaberID).Count();
            //    string[] newsImgAddressArray = _context.Image.Where(im => im.HaberID == newsList[i].HaberID).Select(item => item.ImageURL).ToArray<string>();
            //    FeaturedHaber hbr = new FeaturedHaber();
            //    hbr.LikeCount = like;
            //    hbr.DislikeCount = dislike;
            //    hbr.Images = newsImgAddressArray;
            //    hbr.HaberId = newsList[i].HaberID;
            //    hbr.Detail = newsList[i].Detail;
            //    hbr.HeadLine = newsList[i].HeadLine;
            //    hbr.Id = newsList[i].Id;
            //    hbr.PrimaryImgURL = newsList[i].PrimaryImgURL;
            //    hbr.Title = newsList[i].Title;
            //    hbr.TimeStamp = newsList[i].TimeStamp;
            //    hbr.Latitude = newsList[i].Latitude;
            //    hbr.Longitude = newsList[i].Longitude;
            //    hbr.CategoryID = newsList[i].CategoryID;

            //    if (hbr.Id != null)
            //    {
            //        hbr.UserName = newsList[i].user.Name;
            //        hbr.UserSurname = newsList[i].user.Surname;
            //        hbr.UserImageURL = newsList[i].user.ProfileImgURL;
            //    }else
            //    {
            //        hbr.UserName = "Anonim";
            //        hbr.UserSurname = "Anonim";
            //        hbr.UserImageURL = "Deneme/images/4.jpg";
            //    }

            //    haberList.Add(hbr);
            //}

            return Json(new { haberList, haberLikes, haberDislikes, newsURLs, yorumCounter });
        }

        [HttpGet]
        public JsonResult getNewsDetail(int id)
        {

            var newsList = _context.Haber.Where(h => h.HaberID == id).Include(h => h.user).ToList();
            var yorumList = _context.Comment.Where(c => c.HaberID == id).Include(c => c.user).ToList();
            string newsURL = "https://www.haberimdesin.com/#?hID=" + id;
            var userList = new List<ApplicationUser>();
            for (int i = 0; i < yorumList.Count; i++)
            {
                yorumList[i].haber = null;
                var usr = _context.Users.Single(u => u.Id == yorumList[i].UserID);
                userList.Add(usr);
            }
            return Json(new { yorumList, newsList, userList, newsURL });
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
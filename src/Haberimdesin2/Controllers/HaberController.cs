
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Haberimdesin2.Data;
using Haberimdesin2.Models;
using System.Xml;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Haberimdesin2.Controllers
{
    public class HaberController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IHostingEnvironment _environment;
        public UserManager<ApplicationUser> _user;
        public HaberController(ApplicationDbContext context, IHostingEnvironment environment, UserManager<ApplicationUser> user)
        {
            _context = context;
            _environment = environment;
            _user = user;
        }

        // GET: HaberEntities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Haber.Include(h => h.category).Include(h => h.user);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HaberEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var haberEntity = await _context.Haber.SingleOrDefaultAsync(m => m.HaberID == id);
            if (haberEntity == null)
            {
                return NotFound();
            }

            return View(haberEntity);
        }

        // GET: HaberEntities/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryID");
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HaberEntity haberEntity)
        {
            XmlDocument ipInfoXML = new XmlDocument();
            string resXML = await GetCountryByIP("81.213.90.207");
            ipInfoXML.LoadXml(resXML);
            XmlNodeList responseXMLLat = ipInfoXML.GetElementsByTagName("lat");
            float lat = float.Parse(responseXMLLat[0].InnerText.Replace("\"", ""));

            XmlNodeList responseXMLLon = ipInfoXML.GetElementsByTagName("lon");
            float lon = float.Parse(responseXMLLon[0].InnerText.Replace("\"", ""));

            haberEntity.Longitude = lon;
            haberEntity.Latitude = lat;
            haberEntity.Detail = Request.Form["Detail"].ToString();
            haberEntity.HeadLine = Request.Form["HeadLine"].ToString();
            haberEntity.Title = Request.Form["Title"].ToString();
            haberEntity.Id = _user.GetUserId(User);

            var file = Request.Form.Files[0];

            string haberImgUrl = Path.Combine(new string[] { _environment.WebRootPath, "haberimage" });
            if (!Directory.Exists(haberImgUrl))
                Directory.CreateDirectory(haberImgUrl);
            using (var fileStream = new FileStream(Path.Combine(haberImgUrl, file.FileName), FileMode.Create))
            {
                haberEntity.PrimaryImgURL = "/haberimage/" + file.FileName;
                await file.CopyToAsync(fileStream);
            }
            if (ModelState.IsValid)
            {
                _context.Add(haberEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", haberEntity.Id);
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryID", haberEntity.CategoryID);
            return View(haberEntity);
        }


        public async Task<string> IPRequestHelper(string url)
        {
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            WebResponse objResponse = await objRequest.GetResponseAsync();

            StreamReader responseStream = new StreamReader(objResponse.GetResponseStream());
            string responseRead = responseStream.ReadToEnd();

            responseStream.Dispose();

            return responseRead;
        }

        public async Task<string> GetCountryByIP(string ipAddress)
        {
            string ipResponse = await IPRequestHelper("http://ip-api.com/xml/" + ipAddress);
            return ipResponse;
        }





        // POST: HaberEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      

        // GET: HaberEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var haberEntity = await _context.Haber.SingleOrDefaultAsync(m => m.HaberID == id);
            if (haberEntity == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryID", haberEntity.CategoryID);
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", haberEntity.Id);
            return View(haberEntity);
        }

        // POST: HaberEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HaberID,CategoryID,Detail,Dislike,HeadLine,Id,Latitude,Like,Longitude,PrimaryImgURL,TimeStamp,Title")] HaberEntity haberEntity)
        {
            if (id != haberEntity.HaberID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(haberEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HaberEntityExists(haberEntity.HaberID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryID", haberEntity.CategoryID);
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", haberEntity.Id);
            return View(haberEntity);
        }

        // GET: HaberEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var haberEntity = await _context.Haber.SingleOrDefaultAsync(m => m.HaberID == id);
            if (haberEntity == null)
            {
                return NotFound();
            }

            return View(haberEntity);
        }

        // POST: HaberEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var haberEntity = await _context.Haber.SingleOrDefaultAsync(m => m.HaberID == id);
            _context.Haber.Remove(haberEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool HaberEntityExists(int id)
        {
            return _context.Haber.Any(e => e.HaberID == id);
        }
    }
}

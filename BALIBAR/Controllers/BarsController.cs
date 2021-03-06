﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BALIBAR.Data;
using BALIBAR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BALIBAR.Handlers;
using System.IO;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;
using BALIBAR.Services;
using Microsoft.AspNetCore.Http;

namespace BALIBAR.Controllers
{
    public class BarsController : Controller
    {
        private readonly BALIBARContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _env;
        private readonly RecommendationService _rc;

        public BarsController(BALIBARContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _env = env;
            _rc = new RecommendationService();
        }

        public async Task<IActionResult> Index()
        {
            if (String.Compare(HttpContext.Session.GetString("navigatedFrom"), "Type") == 0)
            {
                HttpContext.Session.SetString("navigatedFrom", "");
                ViewBag.typeName = HttpContext.Session.GetString("barTypeName");
            }
            else HttpContext.Session.SetString("barTypeName", "");

            return View();
        }

        // GET: Bars/Admin
        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        // GET: Bars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return new NotFoundViewResult("NotFoundError", "Bar wasn't found");

            var Bar = await _context.Bar.Include("Type").SingleOrDefaultAsync(b => b.Id == id);

            if (Bar == null) return new NotFoundViewResult("NotFoundError", "Bar wasn't found");

            return View(Bar);
        }

        // GET: Bars/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,Description,MaxParticipants,InOut,Kosher,Accessible,OpeningTime,ClosingTime,MinAge,ImgUrl,Type")] Bar bar)
        {
            var type = _context.Type.Where(t => t.Name == bar.Type.Name);
            if (type.Count() != 0)
            {
                bar.Type = type.ToList()[0];
                ModelState.Remove("Type.MusicType");
                ModelState.Remove("Type.Description");
                if (ModelState.IsValid)
                {
                    if (!String.IsNullOrEmpty(bar.ImgUrl) && System.IO.File.Exists(bar.ImgUrl))
                    {
                        string copyImagePath = _env.WebRootPath + "\\content\\" + Regex.Replace(bar.Name, @"\s+", "") + ".jpg";
                        System.IO.File.Copy(bar.ImgUrl, copyImagePath, true);
                        bar.ImgUrl = "/content/" + Regex.Replace(bar.Name, @"\s+", "") + ".jpg";
                    }
                    else bar.ImgUrl = "/content/NoImage.jpg";

                    _context.Add(bar);
                    await _context.SaveChangesAsync();
                    FaceBookHandler.PostMessage(bar);
                    return RedirectToAction(nameof(Index));
                }                

                return View(bar);
            }
            else
            {
                return new NotFoundViewResult("NotFoundError", "Bar type wasn't found");
            }
        }

        // GET: Bars/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return new NotFoundViewResult("NotFoundError", "Bar wasn't found");

            var bar = _context.Bar.Include(b => b.Type).Where(b => b.Id == id);
            if (bar.Count() == 0) return new NotFoundViewResult("NotFoundError", "Bar wasn't found");
            return View(bar.ToList()[0]);
        }

        // POST: Bars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Description,MaxParticipants,InOut,Kosher,Accessible,OpeningTime,ClosingTime,MinAge,ImgUrl,Type")] Bar bar)
        {
            if (id != bar.Id) return new NotFoundViewResult("NotFoundError", "Bar wasn't found");

            ModelState.Remove("Type.MusicType");
            ModelState.Remove("Type.Description");
            if (ModelState.IsValid)
            {
                try
                {
                    var type = _context.Type.Where(t => t.Name == bar.Type.Name);
                    if (type.Count() != 0)
                    {
                        var tempBar = _context.Bar.Include(b => b.Type).First(b => b.Id == bar.Id);

                        if (!String.IsNullOrEmpty(bar.ImgUrl))
                        {
                            if (tempBar != null && 
                                tempBar.ImgUrl != bar.ImgUrl) {

                                if (System.IO.File.Exists(bar.ImgUrl))
                                {
                                    string copyImagePath = _env.WebRootPath + "\\content\\" + Regex.Replace(bar.Name, @"\s+", "")  + ".jpg";
                                    System.IO.File.Copy(bar.ImgUrl, copyImagePath, true);
                                    bar.ImgUrl = "/content/" + Regex.Replace(bar.Name, @"\s+", "") + ".jpg";
                                }
                                else bar.ImgUrl = "/content/NoImage.jpg";
                            }
                        }
                        bar.Type = type.ToList()[0];

                        _context.Entry(tempBar).CurrentValues.SetValues(bar);
                        tempBar.Type = bar.Type;
                        await _context.SaveChangesAsync();
                    }
                    else return new NotFoundViewResult("NotFoundError", "Bar type wasn't found");
                }
                catch (DbUpdateConcurrencyException) {
                    return new NotFoundViewResult("NotFoundError", "Bar wasn't found");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bar);
        }

        // GET: Bars/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return new NotFoundViewResult("NotFoundError", "Bar wasn't found");

            var bar = await _context.Bar.Include("Type").SingleOrDefaultAsync(b => b.Id == id);

            if (bar == null) return new NotFoundViewResult("NotFoundError", "Bar wasn't found");

            return View(bar);
        }

        // POST: Bars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var bar = await _context.Bar.FindAsync(id);
                _context.Bar.Remove(bar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e) {
                return new UnableToDeleteViewResult("UnableToDeleteError", "Unable to delete specified bar");
            }
            
        }

        public IActionResult Search(string barName, string typeName, int minAge)
        {
            ViewBag.typeName = "";
            var bars = from bar in _context.Bar
                       select bar;
            bars = bars.OrderBy(b => b.Name);

            if (!String.IsNullOrEmpty(barName))
                bars = bars.Where(b => b.Name.ToUpper().Contains(barName.ToUpper()));

            if (minAge > 0)
                bars = bars.Where(b => (b.MinAge >= minAge));

            if (!String.IsNullOrEmpty(typeName))
                bars = bars.Where(b => b.Type.Name.Equals(typeName));

            if (bars.Count() > 0)
                return PartialView("List", bars.Include(b => b.Type).ToList());
            else return PartialView("List", new List<Bar>());
        }

        private bool BarExists(int id)
        {
            return _context.Bar.Any(e => e.Id == id);
        }

        public IActionResult GetTypesList()
        {
            var types = (from type in _context.Type
                        select new { type.Name }).Distinct();

            return Json(types.ToList());
            //return Json(_context.Type.Select(t => t.Name).ToList());
        }

        [HttpGet]
        public async Task<JsonResult> UserRecommendedBars()
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
                var all_bars = _context.Bar.Include(x => x.Type).ToList();
                var all_reservations = _context.Reservation.Include(x => x.Bar.Type).ToList();
                var user_interests = _context.Reservation.Where(r => r.Customer.Id == user.Id).Select(x => x.Bar.Type).Distinct().ToList();
                this._rc.Train(all_reservations, user_interests);
                var recommended = this._rc.PredictRecommendedRooms(all_bars).Take(3).ToList();

                return Json(recommended.Select(x => x.Id));
            }
            return Json(new int[0]);
            
        }

        [Authorize(Roles = "Admin")]
        public JsonResult MostPopularBars()
        {
            var result = this._context.Reservation.Include(x => x.Bar).GroupBy(x => x.Bar.Name).Select(x =>
            new
            {
                BarName = x.Key,
                Reservations = x.ToList().Count()
            }).OrderByDescending(x => x.Reservations).Take(5).ToList();

            return Json(result);
        }

        [Authorize(Roles = "Admin")]
        public JsonResult MostPopularTypes()
        {
            var result = this._context.Reservation.Include(x => x.Bar).GroupBy(x => x.Bar.Type.Name).Select(x =>
            new
            {
                TypeName = x.Key,
                Reservations = x.ToList().Count()
            }).OrderByDescending(x => x.Reservations).Take(5).ToList();

            return Json(result);
        }

        [Authorize(Roles = "Admin")]
        public JsonResult RoomOrdersCountByMonth()
        {
            var result = this._context.Reservation.Where(r => r.DateTime.Year == DateTime.Now.Year).GroupBy(x => x.DateTime.Month).Select(s =>
              new { Month = s.Key, Reservations = s.ToList().Count() }).ToList();

            for (int i = 1; i <= 12; i++)
            {
                var found = false;
                for (int j = 0; j < result.Count; j++)
                    if (result[j].Month == i)
                        found = true;

                if (found == false)
                {
                    var emptyMonth = new
                    {
                        Month = i,
                        Reservations = 0
                    };
                    result.Add(emptyMonth);
                }
            }

            result = result.OrderBy(o => o.Month).ToList();

            return Json(result);
        }
    }
}

using System;
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

namespace BALIBAR.Controllers
{
    public class BarsController : Controller
    {
        private readonly BALIBARContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _env;



        public BarsController(BALIBARContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _env = env;
        }

        // GET: Bars
        [Authorize]
        public async Task<IActionResult> Index(string barName, string typeName, int minAge)
        {
            var bars = from bar in _context.Bar
                        select bar;

            if (!String.IsNullOrEmpty(barName))
                bars = bars.Where(b => b.Name.Contains(barName));

            if (minAge > 0)
                bars = bars.Where(b => (b.MinAge >= minAge ));

            if (!String.IsNullOrEmpty(typeName))
                bars = bars.Where(b => b.Type.Name.Equals(typeName));

            return View(await bars.Include(b => b.Type).ToListAsync());
        }

        // GET: Bars/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return new NotFoundViewResult("NotFoundError", "Bar wasn't found");

            var Bar = await _context.Bar.FirstOrDefaultAsync(m => m.Id == id);

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
            string typeName = ModelState.ToList().First(x => x.Key == "Type.Name").Value.RawValue.ToString();
            var type = _context.Type.Where(t => t.Name == typeName);
            if (type.Count() != 0)
            {
                if (ModelState.IsValid)
                {

                    if (!String.IsNullOrEmpty(bar.ImgUrl)) {
                        string copyImagePath = _env.WebRootPath + "\\content\\" + bar.Name + ".jpg";
                        System.IO.File.Copy(bar.ImgUrl, copyImagePath, true);
                        bar.ImgUrl = "/content/" + bar.Name + ".jpg";
                    }


                    bar.Type = type.ToList()[0];
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

            if (ModelState.IsValid)
            {
                try
                {
                    string typeName = ModelState.ToList().First(x => x.Key == "Type.Name").Value.RawValue.ToString();
                    var type = _context.Type.Where(t => t.Name == typeName);
                    if (type.Count() != 0)
                    {
                        if (!String.IsNullOrEmpty(bar.ImgUrl))
                        {
                            string copyImagePath = _env.WebRootPath + "\\content\\" + bar.Name + ".jpg";
                            System.IO.File.Copy(bar.ImgUrl, copyImagePath, true);
                            bar.ImgUrl = "/content/" + bar.Name + ".jpg";
                        }
                        bar.Type = type.ToList()[0];
                        _context.Update(bar);
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

            var bar = await _context.Bar.FirstOrDefaultAsync(b => b.Id == id);
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
                return new UnableToDeleteViewResult("UnableToDeleteError", "Unable to delete specified room. There are reservations associated with that room!");
            }
            
        }

        private bool BarExists(int id)
        {
            return _context.Bar.Any(e => e.Id == id);
        }

        public IActionResult GetTypesList()
        {
            return Json(_context.Type.Select(t => t.Name).ToList());
        }
    }
}

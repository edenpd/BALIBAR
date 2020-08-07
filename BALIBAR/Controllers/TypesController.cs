using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BALIBAR.Data;
using BALIBAR.Models;
using Microsoft.AspNetCore.Http;

namespace BALIBAR.Controllers
{
    public class TypesController : Controller
    {
        private readonly BALIBARContext _context;

        public TypesController(BALIBARContext context)
        {
            _context = context;
        }

        // GET: Types
        public async Task<IActionResult> Index()
        {
            return View(await _context.Type.ToListAsync());
        }

        // GET: Types/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @type = await _context.Type
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@type == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetString("barTypeId", type.Id.ToString());
            HttpContext.Session.SetString("barTypeName", type.Name);

            return View(@type);
        }

        // GET: Types/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Types/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,MusicType")] BALIBAR.Models.Type @type)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@type);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@type);
        }

        // GET: Types/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @type = await _context.Type.FindAsync(id);
            if (@type == null)
            {
                return NotFound();
            }
            return View(@type);
        }

        // POST: Types/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,MusicType")] BALIBAR.Models.Type @type)
        {
            if (id != @type.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@type);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeExists(@type.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@type);
        }

        // GET: Types/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @type = await _context.Type
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@type == null)
            {
                return NotFound();
            }

            return View(@type);
        }

        // POST: Types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @type = await _context.Type.FindAsync(id);
            _context.Type.Remove(@type);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeExists(int id)
        {
            return _context.Type.Any(e => e.Id == id);
        }

        public IActionResult GetTypeDetails()
        {
            int typeId = Int32.Parse(HttpContext.Session.GetString("barTypeId"));

            int numOfBars = (   from bar in _context.Bar
                                where bar.Type.Id == typeId
                                select bar.Type.Id).Distinct().Count();

            int numOfReservations = (   from type in _context.Type
                                        join bar in _context.Bar
                                        on type.Id equals bar.Type.Id
                                        join reservation in _context.Reservation
                                        on bar.Id equals reservation.Bar.Id
                                        where type.Id == typeId
                                        select type.Id).Count();

            return Json(new { numofbars = numOfBars, numofreservations = numOfReservations });
        }

        [HttpPost]
        public ActionResult NavToBars()
        {
            HttpContext.Session.SetString("barTypeName", HttpContext.Session.GetString("barTypeName"));
            HttpContext.Session.SetString("navigatedFrom", "Type");
            return Json(Url.Action("Index", "Bars"));
        }

        public IActionResult Search(string barTypeName, string musicTypeName)
        {
            ViewBag.typeName = "";
            var types = from Type in _context.Type
                       select Type;
            types = types.OrderBy(b => b.Name);

            if (!String.IsNullOrEmpty(barTypeName))
                types = types.Where(b => b.Name.ToUpper().Contains(barTypeName.ToUpper()));

            if (!String.IsNullOrEmpty(musicTypeName))
                types = types.Where(b => b.MusicType.ToUpper().Contains(musicTypeName.ToUpper()));

            if (types.Count() > 0)
                return PartialView("List", types.ToList());
            else return PartialView("List", new List<BALIBAR.Models.Type>());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BALIBAR.Data;
using BALIBAR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace internetiot.Controllers
{
    public class AdminController : Controller
    {
        private readonly BALIBARContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(BALIBARContext context, UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
            this._context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
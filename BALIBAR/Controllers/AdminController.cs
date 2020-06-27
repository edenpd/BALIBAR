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

        [Authorize(Roles = "Admin")]
        public IActionResult MostProfitable()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public JsonResult RoomOrdersCountByMonth(int? id)
        {
            //var result = this._context.Reservations.Where(r=>r.EscapeRoom.Id == id).GroupBy(x => x.StartingTime.Month).Select(s => 
            //new { Month = s.First().StartingTime.Month, Count = s.Count(), Profit = s.Sum(p => p.TotalPrice) }).ToList();

            return Json(null);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ResetData()
        {
           // _context.Reservations.RemoveRange(_context.Reservations);
           // _context.EscapeRooms.RemoveRange(_context.EscapeRooms);
           // _context.Genre.RemoveRange(_context.Genre);
           // _context.Users.RemoveRange(_context.Users.Where(u=>u.Email != "Admin@gmail.com"));

           // _context.SaveChanges();

           // List<Genre> lstg = new List<Genre>()
           // {
           //     new Genre()
           //     {
           //         Description = "Thrille",
           //         Name = "Thrille",
           //         MinAge = 16
           //     },
           //     new Genre()
           //     {
           //         Description = "Funny rooms",
           //         Name = "Comedy",
           //         MinAge = 10
           //     },
           //     new Genre()
           //     {
           //         Description ="Scary rooms",
           //         Name = "Horror",
           //         MinAge = 18
           //     },
           //     new Genre()
           //     {
           //         Description = "Riddles",
           //         Name = "Riddles",
           //         MinAge = 10
           //     },
           //     new Genre()
           //     {
           //         Description = "Family friendly",
           //         Name = "Family friendly",
           //         MinAge = 6
           //     },
           //     new Genre()
           //     {
           //         Description = "Adults only",
           //         Name = "Adults only",
           //         MinAge = 18
           //     }
           // };
           // List<EscapeRoom> lst = new List<EscapeRoom>()
           //{
           //    new EscapeRoom()
           //    {
           //        Address = "Derekh HaYam 5, Haifa",
           //        Description = "matrix themed escape room",
           //        Duration = 1,
           //        Genre = lstg[0],
           //        ImgUrl = @"https://www.escaperoom.co.il/uploads/content/494/w430/escape-room-the-matrix-tel-aviv-233.jpg",
           //        MaxParticipants = 8,
           //        MinParticipants = 2,
           //        Name= "Matrix",
           //        PricePerParticipant = 80
           //    },
           //    new EscapeRoom()
           //    {
           //        Address = "Hilel 30, Jerusalem",
           //        Description = "Trapped in jerusalem",
           //        Duration = 1,
           //        Genre = lstg[2],
           //        ImgUrl = @"https://www.escaperoom.co.il/uploads/content/496/w430/escape-room-trapped-jerusalem-234.jpg",
           //        MaxParticipants = 8,
           //        MinParticipants = 2,
           //        Name= "Trapped",
           //        PricePerParticipant = 80
           //    },
           //    new EscapeRoom()
           //    {
           //        Address = "Ibn Gabirol 34, Tel Aviv-Yafo",
           //        Description = "Shabak Challenges",
           //        Duration = 2,
           //        Genre = lstg[0],
           //        ImgUrl = @"https://www.escaperoom.co.il/uploads/content/344/w430/escape-room-doomsday-tel-aviv-199.jpg",
           //        MaxParticipants = 2,
           //        MinParticipants = 2,
           //        Name= "Shabak",
           //        PricePerParticipant = 120
           //    },
           //    new EscapeRoom()
           //    {
           //        Address = "Remez 38, Rishon Letsiyon",
           //        Description = "De vinchi code",
           //        Duration = 1,
           //        Genre = lstg[3],
           //        ImgUrl = @"https://www.escaperoom.co.il/uploads/content/491/w430/escape-room-da-vinci-code-eilat-229.jpg",
           //        MaxParticipants = 8,
           //        MinParticipants = 4,
           //        Name= "De vinchi code",
           //        PricePerParticipant = 100
           //    },
           //    new EscapeRoom()
           //    {
           //        Address = "Krinitzi 16, Ramat Gan",
           //        Description = "Spy kids - the nocular program",
           //        Duration = 1,
           //        Genre = lstg[4],
           //        ImgUrl = @"https://www.escaperoom.co.il/uploads/content/474/w430/escape-room-spy-kids-petah-tikva-246.jpg",
           //        MaxParticipants = 6,
           //        MinParticipants = 2,
           //        Name= "Spy kids",
           //        PricePerParticipant = 90
           //    },
           //    new EscapeRoom()
           //    {
           //        Address = "HaMasger 52, Tel Aviv-Yafo",
           //        Description = "Tomb Raider - The secret tomb",
           //        Duration = 1,
           //        Genre = lstg[3],
           //        ImgUrl = @"https://www.escaperoom.co.il/uploads/content/504/w430/tomb-raider-the-secret-temple-299.jpg",
           //        MaxParticipants = 4,
           //        MinParticipants = 2,
           //        Name= "Tomb raider",
           //        PricePerParticipant = 120
           //    },
           //    new EscapeRoom()
           //    {
           //        Address = "Sokolov 20, Netanya",
           //        Description = "The Saw",
           //        Duration = 2,
           //        Genre = lstg[2],
           //        ImgUrl = @"https://www.escaperoom.co.il/uploads/content/397/w430/escape-room-the-saw-rishon-lezion-184.jpg",
           //        MaxParticipants = 7,
           //        MinParticipants = 2,
           //        Name= "The Saw",
           //        PricePerParticipant = 140
           //    },
           //    new EscapeRoom()
           //    {
           //        Address = "Brener 8, Tiberias",
           //        Description = "The Titanic",
           //        Duration = 1,
           //        Genre = lstg[3],
           //        ImgUrl = @"https://www.escaperoom.co.il/uploads/content/169/w430/escape-room-titanic-tel-aviv-223.jpg",
           //        MaxParticipants = 8,
           //        MinParticipants = 4,
           //        Name= "The Titanic",
           //        PricePerParticipant = 100
           //    },
           //    new EscapeRoom()
           //    {
           //        Address = "Hertsel 24, Dimona",
           //        Description = "Prison break",
           //        Duration = 1,
           //        Genre = lstg[3],
           //        ImgUrl = @"https://www.escaperoom.co.il/uploads/content/156/w430/escape-room-prison-break-rishon-lezion-224.jpg",
           //        MaxParticipants = 8,
           //        MinParticipants = 2,
           //        Name= "Prison break",
           //        PricePerParticipant = 110
           //    },
           //    new EscapeRoom()
           //    {
           //        Address = "Shoham 12, Eilat",
           //        Description = "The Hangover",
           //        Duration = 1,
           //        Genre = lstg[1],
           //        ImgUrl = @"https://www.escaperoom.co.il/uploads/content/192/w430/escape-room-the-hangover-139.jpg",
           //        MaxParticipants = 4,
           //        MinParticipants = 3,
           //        Name= "The Hangover",
           //        PricePerParticipant = 100
           //    },
           //     new EscapeRoom()
           //    {
           //        Address = "Rashbi 21, Ashdod",
           //        Description = "The Amazonas",
           //        Duration = 1,
           //        Genre = lstg[3],
           //        ImgUrl = @"https://www.escaperoom.co.il/uploads/content/445/w430/escape-room-amazonas-ashkelon-216.jpg",
           //        MaxParticipants = 6,
           //        MinParticipants = 2,
           //        Name= "The Amazonas",
           //        PricePerParticipant = 100
           //    }
           //};
        
            // Create new users
            var Eden = new ApplicationUser { UserName = "Edenpd", Email = "edenpd@gmail.com" };
            var resultYuval = await  _userManager.CreateAsync(Eden, "123456");

            //List<Reservation> lstr = new List<Reservation>()
            //{
            //    new Reservation()
            //    {
            //        Participants = 3,
            //        TotalPrice = 270,
            //        StartingTime = new DateTime(2018,11,15,11,00,00),
            //        EscapeRoom = lst[0],
            //        User = OmerL
            //    },
            //    new Reservation()
            //    {
            //        Participants = 4,
            //        TotalPrice = 360,
            //        StartingTime = new DateTime(2018,12,21,09,00,00),
            //        EscapeRoom = lst[0],
            //        User = Yuval
            //    },
            //    new Reservation()
            //    {
            //        Participants = 2,
            //        TotalPrice = 180,
            //        StartingTime = new DateTime(2018,12,10,16,00,00),
            //        EscapeRoom = lst[0],
            //        User = OmerS
            //    },
            //    new Reservation()
            //    {
            //        Participants = 5,
            //        TotalPrice = 450,
            //        StartingTime = new DateTime(2019,02,20,18,00,00),
            //        EscapeRoom = lst[0],
            //        User = Tomer
            //    },

            //    new Reservation()
            //    {
            //        Participants = 2,
            //        TotalPrice = 160,
            //        StartingTime = new DateTime(2018,11,24,19,00,00),
            //        EscapeRoom = lst[1],
            //        User = Tomer
            //    },
            //    new Reservation()
            //    {
            //        Participants = 8,
            //        TotalPrice = 640,
            //        StartingTime = new DateTime(2018,12,03,12,00,00),
            //        EscapeRoom = lst[1],
            //        User = Barak
            //    },
            //    new Reservation()
            //    {
            //        Participants = 3,
            //        TotalPrice = 240,
            //        StartingTime = new DateTime(2019,01,08,14,00,00),
            //        EscapeRoom = lst[1],
            //        User = Joni
            //    },

            //    new Reservation()
            //    {
            //        Participants = 2,
            //        TotalPrice = 240,
            //        StartingTime = new DateTime(2018,11,12,21,00,00),
            //        EscapeRoom = lst[2],
            //        User = OmerS
            //    },
            //    new Reservation()
            //    {
            //        Participants = 2,
            //        TotalPrice = 240,
            //        StartingTime = new DateTime(2019,01,23,13,00,00),
            //        EscapeRoom = lst[2],
            //        User = Ravit
            //    },

            //    new Reservation()
            //    {
            //        Participants = 4,
            //        TotalPrice = 400,
            //        StartingTime = new DateTime(2018,11,12,21,00,00),
            //        EscapeRoom = lst[3],
            //        User = Yuval
            //    },
            //    new Reservation()
            //    {
            //        Participants = 7,
            //        TotalPrice = 700,
            //        StartingTime = new DateTime(2019,03,04,17,00,00),
            //        EscapeRoom = lst[3],
            //        User = Shani
            //    },
            //    new Reservation()
            //    {
            //        Participants = 7,
            //        TotalPrice = 700,
            //        StartingTime = new DateTime(2019,03,08,10,00,00),
            //        EscapeRoom = lst[3],
            //        User = Hodaya
            //    },
            //    new Reservation()
            //    {
            //        Participants = 7,
            //        TotalPrice = 700,
            //        StartingTime = new DateTime(2019,04,07,19,00,00),
            //        EscapeRoom = lst[3],
            //        User = Joni
            //    },
            //    new Reservation()
            //    {
            //        Participants = 7,
            //        TotalPrice = 700,
            //        StartingTime = new DateTime(2019,04,07,20,00,00),
            //        EscapeRoom = lst[3],
            //        User = Maayan
            //    },

            //    new Reservation()
            //    {
            //        Participants = 3,
            //        TotalPrice = 270,
            //        StartingTime = new DateTime(2019,01,18,09,00,00),
            //        EscapeRoom = lst[4],
            //        User = Yuval
            //    },
            //    new Reservation()
            //    {
            //        Participants = 4,
            //        TotalPrice = 360,
            //        StartingTime = new DateTime(2018,11,29,18,00,00),
            //        EscapeRoom = lst[4],
            //        User = Maayan
            //    },
            //    new Reservation()
            //    {
            //        Participants = 5,
            //        TotalPrice = 450,
            //        StartingTime = new DateTime(2018,12,20,16,00,00),
            //        EscapeRoom = lst[4],
            //        User = Joni
            //    },
            //    new Reservation()
            //    {
            //        Participants = 6,
            //        TotalPrice = 540,
            //        StartingTime = new DateTime(2018,11,17,20,00,00),
            //        EscapeRoom = lst[4],
            //        User = Hodaya
            //    },

            //    new Reservation()
            //    {
            //        Participants = 2,
            //        TotalPrice = 240,
            //        StartingTime = new DateTime(2018,11,30,13,00,00),
            //        EscapeRoom = lst[5],
            //        User = Maayan
            //    },
            //    new Reservation()
            //    {
            //        Participants = 4,
            //        TotalPrice = 480,
            //        StartingTime = new DateTime(2019,01,05,22,00,00),
            //        EscapeRoom = lst[5],
            //        User = Maayan
            //    },
            //    new Reservation()
            //    {
            //        Participants = 3,
            //        TotalPrice = 360,
            //        StartingTime = new DateTime(2019,02,09,20,00,00),
            //        EscapeRoom = lst[5],
            //        User = Tomer
            //    },
            //    new Reservation()
            //    {
            //        Participants = 2,
            //        TotalPrice = 240,
            //        StartingTime = new DateTime(2019,03,05,19,00,00),
            //        EscapeRoom = lst[5],
            //        User = OmerL
            //    },
            //    new Reservation()
            //    {
            //        Participants = 3,
            //        TotalPrice = 360,
            //        StartingTime = new DateTime(2019,01,23,16,00,00),
            //        EscapeRoom = lst[5],
            //        User = Adee
            //    },

            //    new Reservation()
            //    {
            //        Participants = 3,
            //        TotalPrice = 420,
            //        StartingTime = new DateTime(2018,12,19,20,00,00),
            //        EscapeRoom = lst[6],
            //        User = Ravit
            //    },

            //    new Reservation()
            //    {
            //        Participants = 4,
            //        TotalPrice = 400,
            //        StartingTime = new DateTime(2018,12,17,17,00,00),
            //        EscapeRoom = lst[7],
            //        User = OmerS
            //    },
            //    new Reservation()
            //    {
            //        Participants = 7,
            //        TotalPrice = 700,
            //        StartingTime = new DateTime(2019,01,03,19,00,00),
            //        EscapeRoom = lst[7],
            //        User = Shani
            //    },
            //    new Reservation()
            //    {
            //        Participants = 6,
            //        TotalPrice = 600,
            //        StartingTime = new DateTime(2019,02,07,19,00,00),
            //        EscapeRoom = lst[7],
            //        User = Yuval
            //    },
            //    new Reservation()
            //    {
            //        Participants = 7,
            //        TotalPrice = 700,
            //        StartingTime = new DateTime(2018,11,13,13,00,00),
            //        EscapeRoom = lst[7],
            //        User = Maayan
            //    },

            //    new Reservation()
            //    {
            //        Participants = 5,
            //        TotalPrice = 550,
            //        StartingTime = new DateTime(2018,12,02,10,00,00),
            //        EscapeRoom = lst[8],
            //        User = Adee
            //    },
            //    new Reservation()
            //    {
            //        Participants = 8,
            //        TotalPrice = 880,
            //        StartingTime = new DateTime(2018,11,14,13,00,00),
            //        EscapeRoom = lst[8],
            //        User = Hodaya
            //    },

            //    new Reservation()
            //    {
            //        Participants = 3,
            //        TotalPrice = 300,
            //        StartingTime = new DateTime(2019,01,20,19,00,00),
            //        EscapeRoom = lst[9],
            //        User = Barak
            //    },
            //    new Reservation()
            //    {
            //        Participants = 4,
            //        TotalPrice = 400,
            //        StartingTime = new DateTime(2019,03,17,12,00,00),
            //        EscapeRoom = lst[9],
            //        User = Tomer
            //    },
            //    new Reservation()
            //    {
            //        Participants = 4,
            //        TotalPrice = 400,
            //        StartingTime = new DateTime(2019,03,17,14,00,00),
            //        EscapeRoom = lst[9],
            //        User = Joni
            //    },

            //    new Reservation()
            //    {
            //        Participants = 6,
            //        TotalPrice = 600,
            //        StartingTime = new DateTime(2019,01,17,11,00,00),
            //        EscapeRoom = lst[10],
            //        User = Adee
            //    },
            //    new Reservation()
            //    {
            //        Participants = 2,
            //        TotalPrice = 200,
            //        StartingTime = new DateTime(2018,11,15,15,00,00),
            //        EscapeRoom = lst[10],
            //        User = Hodaya
            //    },
            //    new Reservation()
            //    {
            //        Participants = 5,
            //        TotalPrice = 500,
            //        StartingTime = new DateTime(2018,12,01,19,00,00),
            //        EscapeRoom = lst[10],
            //        User = Barak
            //    },
            //    new Reservation()
            //    {
            //        Participants = 5,
            //        TotalPrice = 500,
            //        StartingTime = new DateTime(2019,04,27,21,00,00),
            //        EscapeRoom = lst[10],
            //        User = Ravit
            //    },
            //};

            //_context.Genre.AddRange(lstg);
            //_context.EscapeRooms.AddRange(lst);
            //_context.Reservations.AddRange(lstr);

            _context.SaveChanges();

            return Json("Data was successfuly saved!");
        }
    }
}
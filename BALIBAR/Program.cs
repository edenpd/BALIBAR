using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BALIBAR.Data;
using BALIBAR.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Type = BALIBAR.Models.Type;

namespace BALIBAR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var host = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(host);
            host.Run();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<BALIBARContext>();
                    context.Database.EnsureCreated();
                    AddData(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB");
                }
            }
        }

        private static void AddData(BALIBARContext context)
        {

            var Eden = new ApplicationUser { UserName = "eden", Email = "eden@gmail.com" };

            var Types = new List<Type>
                {
                    new Type
                    {
                        Name = "Irish pub",
                        Description = "Pub with irish design",
                        MusicType = "All kinds"
                    },

                    new Type
                    {
                        Name = "Pizza bar",
                        Description = "Pizza and beer",
                        MusicType = "Pop"
                    },

                    new Type
                    {
                        Name = "Beach bar",
                        Description = "Bar on the beach with the sound of the waves",
                        MusicType = "Chill"
                    },

                    new Type
                    {
                        Name = "Beer factory",
                        Description = "The beer you get is made in our place",
                        MusicType = "All kinds"
                    },

                    new Type
                    {
                        Name = "Dance bar",
                        Description = "Come to dance with us",
                        MusicType = "All"
                    },

                    new Type
                    {
                        Name = "Open stage bar",
                        Description = "Get on the stage and perdorm or sit back and enjoy the show",
                        MusicType = "Live music"
                    }
                };
            var Bars = new List<Bar> {

                new Bar
                {
                    Name = "Murphys",
                    Address = "Rehov Giborey Israel 17 Netanya",
                    Description = "Murphys is bar with irish design with all kinds of music and all kinds of alcohol",
                    MaxParticipants = 100,
                    InOut = BALIBAR.Models.InOutEnum.Both,
                    Kosher = true,
                    Accessible = true,
                    OpeningTime = DateTime.Today.Add(TimeSpan.Parse("20:00:00")),
                    ClosingTime = DateTime.Today.Add(TimeSpan.Parse("02:00:00")),
                    MinAge = 21,
                    ImgUrl = "/content/Murphys.jpg",
                    Type = context.Type.FirstOrDefault(t => t.Name == "Irish pub")

                },

                new Bar
                {
                    Name = "Goons",
                    Address = "David HaMelekh, 42000 Netanya, Israel",
                    Description = "Goons is pizza bar with all kinds of music and all kinds of pizzas",
                    MaxParticipants = 70,
                    InOut = BALIBAR.Models.InOutEnum.Both,
                    Kosher = true,
                    Accessible = true,
                    OpeningTime = DateTime.Today.Add(TimeSpan.Parse("18:00:00")),
                    ClosingTime = DateTime.Today.Add(TimeSpan.Parse("01:00:00")),
                    MinAge = 16,
                    ImgUrl = "/content/Goons.jpg",
                    Type = context.Type.FirstOrDefault(t => t.Name == "Pizza bar")
                },

                new Bar
                {
                    Name = "Mikes place",
                    Address = "Abba Eban Avenue 12, 46725 Herzliya, Israel",
                    Description = "Mikes place is open stage bar",
                    MaxParticipants = 120,
                    InOut = BALIBAR.Models.InOutEnum.Both,
                    Kosher = false,
                    Accessible = true,
                    OpeningTime = DateTime.Today.Add(TimeSpan.Parse("20:00:00")),
                    ClosingTime = DateTime.Today.Add(TimeSpan.Parse("01:00:00")),
                    MinAge = 18,
                    ImgUrl = "/content/Mikesplace.jpg",
                    Type = context.Type.FirstOrDefault(t => t.Name == "Open stage bar")
                },

                new Bar
                {
                    Name = "Jems",
                    Address = "Maskit 21, 46733 Herzliya, Israel",
                    Description = "Jems is factory bar",
                    MaxParticipants = 100,
                    InOut = BALIBAR.Models.InOutEnum.In,
                    Kosher = true,
                    Accessible = true,
                    OpeningTime = DateTime.Today.Add(TimeSpan.Parse("17:00:00")),
                    ClosingTime = DateTime.Today.Add(TimeSpan.Parse("01:00:00")),
                    MinAge = 18,
                    ImgUrl = "/content/Jems.jpg",
                    Type = context.Type.FirstOrDefault(t => t.Name == "Beer factory")
                },

                new Bar
                {
                    Name = "Lima Lima",
                    Address = "Lilenblum 42, 67132 Tel Aviv-Yafo, Israel",
                    Description = "Lima Lima is salsa dance bar",
                    MaxParticipants = 150,
                    InOut = BALIBAR.Models.InOutEnum.In,
                    Kosher = false,
                    Accessible = false,
                    OpeningTime = DateTime.Today.Add(TimeSpan.Parse("22:00:00")),
                    ClosingTime = DateTime.Today.Add(TimeSpan.Parse("03:00:00")),
                    MinAge = 18,
                    ImgUrl = "/content/LimaLima.jpg",
                    Type = context.Type.FirstOrDefault(t => t.Name == "Dance bar")
                }


            };
            var Reservations = new List<Reservation> {

                new Reservation()
                {
                    Customer = Eden,
                    DateTime = new DateTime(2020,11,15,11,00,00),
                    Bar = Bars[0],
                    AttendeesNum = 5
                },

                new Reservation()
                {
                    Customer = Eden,
                    DateTime = new DateTime(2020,11,15,11,00,00),
                    Bar = Bars[1],
                    AttendeesNum = 4
                },

                new Reservation()
                {
                    Customer = Eden,
                    DateTime = new DateTime(2020,11,15,11,00,00),
                    Bar = Bars[2],
                    AttendeesNum = 2
                },

                new Reservation()
                {
                    Customer = Eden,
                    DateTime = new DateTime(2020,11,15,11,00,00),
                    Bar = Bars[0],
                    AttendeesNum = 2
                },
            };

            // If bar type list is empty
            if (!context.Type.Any())
            {
                context.Type.AddRange(Types);
                context.SaveChanges();
            }

            // If bar list is empty
            if (!context.Bar.Any())
            {
                context.Bar.AddRange(Bars);
                context.SaveChanges();
            }

            if (!context.Reservation.Any())
            {
                context.Reservation.AddRange(Reservations);
                context.SaveChanges();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

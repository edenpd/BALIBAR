using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BALIBAR.Data;
using BALIBAR.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
            if (!context.Bar.Any())
            {
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
                    Type = context.Type.FirstOrDefault(t => t.Name == "Irish pub")
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
                    Type = context.Type.FirstOrDefault(t => t.Name == "Irish pub")
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
                    Type = context.Type.FirstOrDefault(t => t.Name == "Irish pub")
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
                    Type = context.Type.FirstOrDefault(t => t.Name == "Irish pub")
                }


            };
                context.AddRange(Bars);
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

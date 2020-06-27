using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BALIBAR.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BALIBAR.Data
{
    public class BALIBARContext : IdentityDbContext<ApplicationUser>
    {

        public BALIBARContext (DbContextOptions<BALIBARContext> options)
            : base(options)
        {
        }

        public DbSet<BALIBAR.Models.Bar> Bar { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using CRVS.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRVS.EF
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>option) : base(option)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}

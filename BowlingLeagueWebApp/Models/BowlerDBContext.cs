using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeagueWebApp.Models
{
    public class BowlerDBContext : DbContext
    {
        public BowlerDBContext(DbContextOptions<BowlerDBContext> options) : base (options)
        {

        }

        public DbSet<Bowler> bowlers { get; set; }
        public DbSet<Team> teams { get; set; }
    }
}

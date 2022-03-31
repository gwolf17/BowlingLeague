using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Models
{
    public class BowlersDbContext : DbContext
    {
        //Constructor
        public BowlersDbContext(DbContextOptions<BowlersDbContext> options) : base (options)
        {

        }

        //Bundle each bowler/team into a list of Bowlers/Teams
        public DbSet<Bowler> Bowlers { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}

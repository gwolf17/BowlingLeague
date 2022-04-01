using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Models
{
    public interface IBowlerRepository
    {
        //Get the data from the Bowlers model
        IQueryable<Bowler> Bowlers { get; }
        //Get data from the Teams model
        IQueryable<Team> Teams { get; }

        //CRUD functionality
        public void SaveBowler(Bowler b);
        public void CreateBowler(Bowler b);
        public void DeleteBowler(Bowler b);
    }
}

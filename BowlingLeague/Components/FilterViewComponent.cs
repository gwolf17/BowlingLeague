using BowlingLeague.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Components
{
    public class FilterViewComponent : ViewComponent  //Inherit from ViewComponent class
    {
        //Instance of the repository
        private IBowlerRepository _repo { get; set; }

        //Constructor
        public FilterViewComponent(IBowlerRepository temp)
        {
            _repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.Filtered = RouteData?.Values["filter"];  //Get any team names being passed in

            //Pull in all the team names from our database
            var teams = _repo.Teams
                .Select(x => x.TeamName)
                .Distinct();

            //Return the Filter component view along with the list of teams
            return View(teams);
        }
    }
}

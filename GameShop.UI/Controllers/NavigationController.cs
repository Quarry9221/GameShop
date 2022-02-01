using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameShop.Domain.Repositories;

namespace GameShop.UI.Controllers
{
    public class NavigationController : Controller
    {
        private IGameRepository rep;
        public NavigationController(IGameRepository rep_)
        {
            rep = rep_;
        }
        public PartialViewResult Menu(string genre = null)
        {
            ViewBag.SelectedGenre = genre;
            IEnumerable<string> genres = rep.Games
                .Select(game => game.Genre)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(genres);
        }
    }
}
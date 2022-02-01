using GameShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameShop.UI.Models
{
    public class GamesListViewModel
    {
        public IEnumerable<Game> Games { get; set; }
        public PageInfo PageInfo { get; set; }
        public string CurrentGenre { get; set; }
    }
}
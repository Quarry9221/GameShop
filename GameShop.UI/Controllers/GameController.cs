using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameShop.Domain.Repositories;
using GameShop.Domain.Entities;
using GameShop.UI.Models;

namespace GameShop.UI.Controllers
{
    public class GameController : Controller
    {
        private IGameRepository repository;
        public int pageSize = 4;

        public GameController(IGameRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string genre,int page = 1)
        {
            GamesListViewModel model = new GamesListViewModel
            {
                Games = repository.Games
                    .Where(p => genre == null || p.Genre == genre)
                    .OrderBy(game => game.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = genre == null ?
                    repository.Games.Count() :
                    repository.Games.Where(game => game.Genre == genre).Count()
                },
                CurrentGenre = genre
            };
            return View(model);
        }
        public FileContentResult GetImage(int Id)
        {
            Game game = repository.Games
                .FirstOrDefault(g => g.Id == Id);

            if (game != null)
            {
                return File(game.ImageData, game.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameShop.Domain.Repositories;
using GameShop.Domain.Entities;

namespace GameShop.UI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IGameRepository repository;
        public AdminController (IGameRepository rep)
        {
            repository = rep;
        }
        public ViewResult Index()
        {
            return View(repository.Games);
        }
        public ViewResult Edit (int Id)
        {
            Game game = repository.Games.FirstOrDefault(g => g.Id == Id);
            return View(game);
        }
        [HttpPost]
        public ActionResult Edit(Game game, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    game.ImageMimeType = image.ContentType;
                    game.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(game.ImageData, 0, image.ContentLength);
                }
                repository.SaveGame(game);
                TempData["message"] = string.Format("Edits was \"{0}\" changed", game.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(game);
            }
        }
        public ViewResult Create()
        {
            return View("Edit", new Game());
        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            Game deletedGame = repository.DeleteGame(Id);
            if (deletedGame != null)
            {
                TempData["message"] = string.Format("Game \"{0}\" was deleted",
                    deletedGame.Name);
            }
            return RedirectToAction("Index");
        }
    }
}
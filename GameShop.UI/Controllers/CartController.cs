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
    public class CartController : Controller
    {
        // GET: Cart
        private IGameRepository repository;
        private IOrderProcessor orderProcessor;
        public CartController(IGameRepository repo, IOrderProcessor order)
        {
            repository = repo;
            orderProcessor = order;
        }
        public ViewResult Index(Cart cart,string returnURL)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnURL
            });
        }
        public RedirectToRouteResult Add(Cart cart,int Id, string returnUrl)
        {
            Game game = repository.Games
                .FirstOrDefault(g => g.Id == Id);

            if (game != null)
            {
                cart.Add(game, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult Remove(Cart cart,int Id, string returnUrl)
        {
            Game game = repository.Games
                .FirstOrDefault(g => g.Id == Id);

            if (game != null)
            {
                cart.Remove(game);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }

    }
}
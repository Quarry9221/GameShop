using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameShop.Domain.Entities;
using System.Web.Mvc;

namespace GameShop.UI.Infrastructure.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";
        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            Cart cart = null;
            if (controllerContext.HttpContext.Session != null)
            {
                cart = (Cart)controllerContext.HttpContext.Session[sessionKey];
            }

            // Создать объект Cart если он не обнаружен в сеансе
            if (cart == null)
            {
                cart = new Cart();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }

            // Возвратить объект Cart
            return cart;
        }
    }
}
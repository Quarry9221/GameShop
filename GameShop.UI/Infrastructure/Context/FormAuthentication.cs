using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using GameShop.UI.Infrastructure.Repository;
namespace GameShop.UI.Infrastructure.Context
{
    public class FormAuthentication : IAuthentication
    {
        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return result;
        }
    }
}
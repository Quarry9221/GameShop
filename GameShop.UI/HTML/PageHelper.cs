using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using GameShop.UI.Models;

namespace GameShop.UI.HTML
{
    public static class PageHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfo pageinfo, Func<int, string> pageURL)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageinfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageURL(i));
                tag.InnerHtml = i.ToString();
                if (i == pageinfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}
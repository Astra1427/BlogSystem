using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogSystem.MVCSite.Filters
{
    public class BlogSystemAuthorizeAttribute:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.Cookies["loginName"] != null && filterContext.HttpContext.Request.Cookies["userId"] != null && filterContext.HttpContext.Session["loginName"] == null)
            {
                //当用户存储在cookie中且session数据为空时，把cookie的数据同步到session中
                filterContext.HttpContext.Session["loginName"] = filterContext.HttpContext.Request.Cookies["loginName"].Value;
                filterContext.HttpContext.Session["userId"] = filterContext.HttpContext.Request.Cookies["userId"].Value;
            }

            if (filterContext.HttpContext.Request.Cookies["loginName"] == null && filterContext.HttpContext.Session["loginName"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary {
                    { "controller","home"},
                    { "action","login"}
                });

                //if (filterContext.HttpContext.Request.UrlReferrer != null)
                //{
                //    filterContext.HttpContext.Response.Redirect(filterContext.HttpContext.Request.UrlReferrer.ToString());
                //}
                //else
                //{
                //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary() {
                //        {"controller","home" },
                //        {"action","index" }
                //    });
                //}

            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



    public class SessionAuth: ActionFilterAttribute
    {
    public ISession _contextSession { get; set; }
    public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            var session = filterContext.HttpContext.Session;
        _contextSession = session;
            if (session.GetString("user")  == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                                { "Controller", "User" },
                                { "Action", "Login" }
                                });
            }
        }
        public string  GetUser(){
        return _contextSession.GetString("user");
        
        }
    }



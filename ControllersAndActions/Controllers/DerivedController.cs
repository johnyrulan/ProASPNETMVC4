using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControllersAndActions.Controllers
{
    public class DerivedController : Controller
    {
        /*
         * The Base Controller class gives you access to the following variables.
         * Request, Response, RouteData, HttpContext, and Server.
         * 
         * Controllers have three parts to them:
         * 1) Action Methods
         * 2) Action Results
         * 3) Filters
         * 
         * For actions, value type parameters are required and cannot be null unless nullable.
         * Reference types however, can be null and must be checked within the action method.
         * Default parameter values can also be set as a safe guard.
         * 
         */
        public string ShowWeatherForecast()
        {
            string city = (string) this.RouteData.Values["city"];
            string forDate = (string) Request.Form["forDate"];

            return String.Format("City: {0}, Date: {1}", city, forDate);
        }
	}
}
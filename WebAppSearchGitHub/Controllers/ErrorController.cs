using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppSearchGitHub.Controllers
{
    /// <summary>    
    /// controller for error actions    
    /// </summary> 
    public class ErrorController : Controller
    {
        /// <summary>    
        /// Return error page of httpError with code 404    
        /// </summary> 
        public ActionResult Error404()
        {
            return View();
        }

        /// <summary>    
        /// Return error page of httpError with code 500    
        /// </summary> 
        public ActionResult Error500()
        {
            return View();
        }

        /// <summary>    
        /// Return error page of httpError other    
        /// </summary> 
        public ActionResult Error()
        {
            return View();
        }
    }
}
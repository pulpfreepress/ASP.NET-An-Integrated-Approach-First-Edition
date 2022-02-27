using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Code;

namespace Web.Controllers {

    [HandleError]
    public class HelpController : BaseController {

        public ActionResult About() {
            ViewData["Message"] = "Hello World!";
           return View();
           
        }

        

    } // end HelpController class
} // end namespace
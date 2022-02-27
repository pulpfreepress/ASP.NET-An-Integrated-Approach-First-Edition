using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Code;

namespace Web.Controllers {
    public class MasterViewPageController : BaseController {


        public ActionResult HomePage() {
            return View();
        }
        
        public ActionResult ListEmployees() {
           
            return View();
        }

        public ActionResult CreateEmployee() {
            return View();
        }

        public ActionResult EditEmployee() {
            return View();
        }

        public ActionResult HelpPage() {
            return View();
        }

    }
}
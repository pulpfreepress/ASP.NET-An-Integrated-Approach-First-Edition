using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Code;
using System.Web.Security;

using BusinessLogic.BO;
using Infrastructure.Exceptions;
using Infrastructure.ValueObjects;

namespace Web.Controllers {
    public class MasterViewPageController : BaseController {


        protected override void OnActionExecuting(ActionExecutingContext filterContext) {
            base.OnActionExecuting(filterContext);

            if (Session[WebConstants.CURRENT_USER] == null) {
                Response.Redirect("~/Default.aspx");
            }
        }

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

        public void Default() {
            Response.Redirect(WebConstants.DEFAULT_PAGE);
        }

    }
}
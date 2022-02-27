using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Code;
using BusinessLogic.BO;
using Infrastructure.ValueObjects;
using System.Text.RegularExpressions;

namespace Web.Controllers {

    [HandleError]
    public class AdminController : BaseController {


        public AdminController() :
            base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) { }


        public ActionResult EmailTypeMaintenance() {
            LogDebug("EmailTypeMaintenance method called...!");
            AdminBO bo = new AdminBO();
            ViewData["EmailTypes"] = bo.GetEmailTypes();
            return View();
        }

        public ActionResult Edit(int id) {

            LogDebug("Edit method called...!");

            AdminBO bo = new AdminBO();
            return View(bo.GetEmailType(id));
        }


        public ActionResult Save(EmailTypeVO vo) {
            LogDebug("Save method called - EmailTypeID: "
                     + vo.EmailTypeID + " Description: " + vo.Description);

            if (ModelState.IsValid) {
                AdminBO bo = new AdminBO();
                bo.UpdateEmailType(vo);
                ViewData["EmailTypes"] = bo.GetEmailTypes();
                return View("EmailTypeMaintenance");
            }
            else {
                return View("Edit");
            }
        }


        public ActionResult New() {
            return View("Create");
        }


        public ActionResult Create(EmailTypeVO vo) {
            LogDebug("Create method called - EmailTypeID: "
                     + vo.EmailTypeID + " Description: " + vo.Description);

            if (ModelState.IsValid) {
                AdminBO bo = new AdminBO();
                bo.InsertEmailType(vo);
                ViewData["EmailTypes"] = bo.GetEmailTypes();
                return View("EmailTypeMaintenance");
            }
            else {
                return View();
            }
        }

    } // End class
} // end namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Web.App_Code;
using System.Web.Security;

namespace Web {
    public partial class MasterPage : System.Web.UI.MasterPage {
        protected void Page_Load(object sender, EventArgs e) {
            if (Session[WebConstants.CURRENT_USER] == null) {
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        public string Message {
            set { messageLabel.Text = value; }
        }


        public void ListEmployees_Click(object sender, EventArgs e) {
            Response.Redirect(WebConstants.LIST_EMPLOYEES_PAGE);
        }

        public void CreateEmployee_Click(object sender, EventArgs e) {
            Response.Redirect(WebConstants.CREATE_EMPLOYEE_PAGE);
        }

        public void Home_Click(object sender, EventArgs e) {
            Response.Redirect(WebConstants.DEFAULT_PAGE);
        }

        public void EditEmployee_Click(object sender, EventArgs e) {
            Response.Redirect(WebConstants.EDIT_EMPLOYEE_PAGE);
        }

        public void Help_Click(object sender, EventArgs e) {
            String appServerName = null;

            // Get the app server name from the properties file, if it was supplied
            if (!String.IsNullOrEmpty(WebConfigurationManager.AppSettings[WebConstants.APPLICATION_SETTING_APP_SERVER_NAME])) {
                appServerName = WebConfigurationManager.AppSettings[WebConstants.APPLICATION_SETTING_APP_SERVER_NAME];
            }
            else // Get the app server name from the application path in the request
        {
                appServerName = Request.ApplicationPath;
            }

            Page.ClientScript.RegisterStartupScript(typeof(Page),
                                                    (new Guid().ToString()),
                                                    "<script>window.open('" + appServerName + WebConstants.HELP_PAGE + "')</script>");
        }



        public void LogOff(object sender, EventArgs e) {
            Session.Clear();
            FormsAuthentication.SignOut();
        }

    }
}
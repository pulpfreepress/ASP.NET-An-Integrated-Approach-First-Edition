using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Web.App_Code;

namespace Web.Views.MasterViewPage {
    public partial class HelpPage : BaseView {
        protected void Page_Load(object sender, EventArgs e) {
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
    }
}
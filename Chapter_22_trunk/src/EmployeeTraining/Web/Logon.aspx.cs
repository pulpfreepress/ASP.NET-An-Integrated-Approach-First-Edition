using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using Infrastructure.ValueObjects;
using Infrastructure.Exceptions;
using BusinessLogic.BO;
using Web.App_Code;


namespace Web {
    public partial class Logon :BasePage {
        protected void Page_Load(object sender, EventArgs e) { }


        public void Authenticate(object sender, AuthenticateEventArgs e) {
            string username = passwordLogin.UserName.Trim();
            string password = passwordLogin.Password.Trim();
            LogDebug("Logging in user with username: " + username + " password: " + password);
            try {
                LoginBO bo = new LoginBO();
                EmployeeVO user = bo.LoginUser(username, password);
                LogDebug("Login successfull for username: " + username);
                Session.Add(WebConstants.CURRENT_USER, user);
                FormsAuthentication.RedirectFromLoginPage(username, false);
            }
            catch (Exception ex) {
                LogDebug("Problem logging in user with username: " + username + " password: "
                           + password, ex);
            }
        }
    }
}
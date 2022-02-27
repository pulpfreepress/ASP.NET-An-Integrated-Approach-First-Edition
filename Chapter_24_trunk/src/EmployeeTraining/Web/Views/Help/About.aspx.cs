using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Mvc;

using Web.App_Code;
using BusinessLogic.BO;
using Infrastructure.Exceptions;
using Infrastructure.ValueObjects;

namespace Web.Views.Help {
    public partial class About : BaseView {

        protected void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                MessageLabel.Text = "Welcome to the world of MVC!";
                LogDebug("Loading Help\\About.aspx page");
            }
            

        }
    }
}
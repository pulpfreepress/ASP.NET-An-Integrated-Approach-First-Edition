using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.App_Code;

namespace Web.Views.MasterViewPage {
    public partial class HomePage : BaseView {
        protected void Page_Load(object sender, EventArgs e) {
            Response.Redirect(WebConstants.LIST_EMPLOYEES_PAGE);
        }
    }
}
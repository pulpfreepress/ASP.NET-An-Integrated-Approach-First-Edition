using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Specialized;
using System.Collections;
using System.Drawing;

using Web.App_Code;
//using BusinessLogic.BO;
//using Infrastructure.ValueObjects;
//using BusinessLogic.Utils;

namespace Web.Pages.Employee {
    public partial class ListEmployees : BasePage {
        protected void Page_Load(object sender, EventArgs e) {
            
        }

        /**************************************************************
        * IMPORTANT -- MUST HAVE IN CODE BEHIND TO SUPPORT EXPORT
        * ************************************************************/
        public override void VerifyRenderingInServerForm(Control control) {
            // empty method for export button...
        }

    }
}
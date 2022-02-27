using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Specialized;
using System.Collections;

using Web.App_Code;
using BusinessLogic.BO;
using Infrastructure.ValueObjects;

namespace Web {
    public partial class Default : Web.App_Code.BasePage {

        protected void Page_Load(object sender, EventArgs e) {

            HandlePageNavigation(WebConstants.LIST_EMPLOYEES_PAGE);
          
        } // end Page_Load() method
    } // end Default codebehind
} // end namespace
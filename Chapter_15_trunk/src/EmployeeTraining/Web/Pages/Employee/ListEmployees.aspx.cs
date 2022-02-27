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

namespace Web.Pages.Employee {
    public partial class ListEmployees : BasePage {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                EmployeeManagementBO bo = new EmployeeManagementBO();
                GridView1.DataSource = bo.GetAllEmployees();
                GridView1.DataBind();
            }
        }


  
        protected void IndexChanged_Handler(Object sender, GridViewSelectEventArgs e) {
           
            DataKey employeeDK = GridView1.DataKeys[e.NewSelectedIndex];
            Session.Add(WebConstants.EMPLOYEE_ID, (int)employeeDK.Value );

            HandlePageNavigation(WebConstants.EMPLOYEE_DETAILS_PAGE);

        }

    }
}
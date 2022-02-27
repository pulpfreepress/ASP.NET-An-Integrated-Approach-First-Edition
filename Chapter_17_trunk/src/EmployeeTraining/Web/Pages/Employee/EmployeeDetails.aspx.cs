using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Web.App_Code;
using BusinessLogic.BO;
using Infrastructure.ValueObjects;

namespace Web.Pages.Employee {
    public partial class EmployeeDetails : BasePage {

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                if (Session[WebConstants.EMPLOYEE_ID] == null) {
                    HandlePageNavigation(WebConstants.DEFAULT_PAGE);
                }
                else {
                    int employeeID = (int)Session[WebConstants.EMPLOYEE_ID];
                    EmployeeManagementBO bo = new EmployeeManagementBO();
                    EmployeeVO vo = bo.GetEmployee(employeeID);
                    firstNameTextBox.Text = vo.FirstName;
                    middleNameTextBox.Text = vo.MiddleName;
                    lastNameTextBox.Text = vo.LastName;
                    birthdayCalendar.VisibleDate = vo.Birthday;
                    birthdayCalendar.SelectedDate = birthdayCalendar.VisibleDate;
                    hiredateCalendar.VisibleDate = vo.HireDate;
                    hiredateCalendar.SelectedDate = hiredateCalendar.VisibleDate;
                   
                    isActiveCheckbox.Checked = vo.IsActive;
                }
            }
        } // end Page_Load() method

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Web.App_Code;
using BusinessLogic.BO;
using Infrastructure.Exceptions;
using Infrastructure.ValueObjects;

namespace Web.Pages.Employee {
    public partial class EditEmployee : BasePage {

        protected delegate void MethodDelegate();

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                employeeDropDown.PopulateControl();
                editEmployeeControl.Enabled = false;
                editEmployeeControl.Clear();
                editEmployeeControl.Visible = false;
                editUpdateCancelControl.Visible = false;
            }
            else {
                editUpdateCancelControl.SaveMethod = new MethodDelegate(SaveMethod_Handler);
                editUpdateCancelControl.CancelMethod = new MethodDelegate(CancelMethod_Handler);
                editUpdateCancelControl.EditMethod = new MethodDelegate(EditMethod_Handler);
                editUpdateCancelControl.UpdateMethod = new MethodDelegate(UpdateMethod_Handler);
            }

        }


        protected void EmployeeIndexChanged_Handler(Object sender, EventArgs e) {
            LogDebug("Selected EmployeeID = " + employeeDropDown.SelectedValue);
            EmployeeManagementBO bo = new EmployeeManagementBO();
            editEmployeeControl.Clear();
            editEmployeeControl.Employee = bo.GetEmployee(Int32.Parse(employeeDropDown.SelectedValue));
            editEmployeeControl.Visible = true;
            editEmployeeControl.Enabled = false;
            editUpdateCancelControl.Visible = true;
            editUpdateCancelControl.ShowEditButton();

        }

        protected void SaveMethod_Handler() {
            // Should never call the save method in this page...
        }


        protected void CancelMethod_Handler() {
            editEmployeeControl.Clear();
            employeeDropDown.PopulateControl();
            editUpdateCancelControl.Visible = false;
            editEmployeeControl.Visible = false;
        }


        protected void EditMethod_Handler() {
            editUpdateCancelControl.ShowUpdateButton();
            editEmployeeControl.Enabled = true;
        }

        protected void UpdateMethod_Handler() {
            if (Page.IsValid) {
                EmployeeManagementBO bo = new EmployeeManagementBO();
                EmployeeVO vo = bo.UpdateEmployee(editEmployeeControl.Employee);
                ((MasterPage)Master).Message = "Employee number " + vo.EmployeeID + " successfully updated!";
                editEmployeeControl.Clear();
                employeeDropDown.PopulateControl();
                editUpdateCancelControl.Visible = false;
                editEmployeeControl.Visible = false;
            }
        }
    }
}
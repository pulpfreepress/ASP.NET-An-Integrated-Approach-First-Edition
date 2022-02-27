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
    public partial class CreateEmployee : BasePage {

        protected delegate void MethodDelegate();

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                editUpdateCancelControl.ShowSaveButton();
                editEmployeeControl.Clear();
            }
            else {
                editUpdateCancelControl.SaveMethod = new MethodDelegate(SaveMethod_Handler);
                editUpdateCancelControl.CancelMethod = new MethodDelegate(CancelMethod_Handler);
            }

        }


        public void SaveMethod_Handler() {
            if (Page.IsValid) {
                try {
                    EmployeeManagementBO bo = new EmployeeManagementBO();
                    EmployeeVO vo = editEmployeeControl.Employee;
                    LogDebug("Address Count: " + vo.Addresses.Count);
                    LogDebug("Phone Count: " + vo.PhoneNumbers.Count);
                    LogDebug("Email Count: " + vo.EmailAddresses.Count);

                    vo = bo.CreateEmployee(vo);
                    ((MasterPage)Master).Message = "Employee number " + vo.EmployeeID + " successfully created!";
                    editEmployeeControl.Clear();
                }

                catch (BLException) {
                    ((MasterPage)Master).Message = "Problem creating employee! See log file.";
                }
            }

        }


        public void CancelMethod_Handler() {
            HandlePageNavigation(WebConstants.LIST_EMPLOYEES_PAGE);
        }





    }
}
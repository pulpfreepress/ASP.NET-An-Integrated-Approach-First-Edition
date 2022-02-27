using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Web.App_Code;
using Infrastructure.ValueObjects;

namespace Web.Controls {
    public partial class EditEmployeeControl : BaseControl {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                birthdayCalendar.PopulateDropDowns();
                hiredateCalendar.PopulateDropDowns();
                birthdayCalendar.SetSelectedAndVisibleDate(DateTime.Now);
                hiredateCalendar.SetSelectedAndVisibleDate(DateTime.Now);
            }

        }

        public EmployeeVO Employee {
            get {
                EmployeeVO vo = new EmployeeVO();
                vo.FirstName = firstNameTextBox.Text;
                vo.MiddleName = middleNameTextBox.Text;
                vo.LastName = lastNameTextBox.Text;
                vo.Birthday = birthdayCalendar.SelectedDate;
                vo.HireDate = hiredateCalendar.SelectedDate;
                vo.IsActive = isActiveCheckBox.Checked;
                return vo;
            }

            set {
                firstNameTextBox.Text = value.FirstName;
                middleNameTextBox.Text = value.MiddleName;
                lastNameTextBox.Text = value.LastName;
                birthdayCalendar.SetSelectedAndVisibleDate(value.Birthday);
                hiredateCalendar.SetSelectedAndVisibleDate(value.HireDate);
                isActiveCheckBox.Checked = value.IsActive;
            }
        }


        public bool Enabled {
            set {
                firstNameTextBox.Enabled = value;
                middleNameTextBox.Enabled = value;
                lastNameTextBox.Enabled = value;
                birthdayCalendar.Enabled = value;
            }
        }


        public void Clear() {
            firstNameTextBox.Text = string.Empty;
            middleNameTextBox.Text = string.Empty;
            lastNameTextBox.Text = string.Empty;
            birthdayCalendar.SetSelectedAndVisibleDate(DateTime.Now);
            hiredateCalendar.SetSelectedAndVisibleDate(DateTime.Now);
            isActiveCheckBox.Checked = true;
        }


    }
}
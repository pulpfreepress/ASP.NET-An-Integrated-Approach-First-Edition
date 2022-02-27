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
using BusinessLogic.BO;
using Infrastructure.ValueObjects;
using BusinessLogic.Utils;

namespace Web.Pages.Employee {
    public partial class ListEmployees : BasePage {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                InitializeListEmployeesGridView();
            }

        }

        protected List<EmployeeVO> Employees {
            get;
            set;
        }

        /* ***************************************************************************
        protected void IndexChanging_Handler(Object sender, GridViewSelectEventArgs e) {
            DataKey employeeDK = ListEmployeesGridView.DataKeys[e.NewSelectedIndex];
            Session.Add(WebConstants.EMPLOYEE_ID, (int)employeeDK.Value);
            HandlePageNavigation(WebConstants.EMPLOYEE_DETAILS_PAGE);    
        }
         ****************************************************************************/

        /****************************************************************************
        protected void HighlightBirthdays() {
            foreach (GridViewRow row in ListEmployeesGridView.Rows) {
                if (Employees[row.RowIndex].Birthday.Month == 4) {
                    row.Cells[4].BackColor = Color.Blue;
                    row.Cells[4].ForeColor = Color.White;
                }
            }
        }
        ****************************************************************************/

        public void RowDataBound_Handler(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                //if (((EmployeeVO)e.Row.DataItem).Birthday.Month == DateTime.Now.Month) {
                if (((EmployeeVO)e.Row.DataItem).Birthday.Month == 4) {
                    e.Row.Cells[4].BackColor = Color.Blue;
                    e.Row.Cells[4].ForeColor = Color.White;
                }
            }
        }


        protected string GetBirthday(object dataItem) {
            EmployeeVO employee = (EmployeeVO)dataItem;
            return employee.Birthday.ToString(WebConstants.EXPANDED_DATE_FORMAT);
        }


        protected string GetHiredate(object dataItem) {
            EmployeeVO employee = (EmployeeVO)dataItem;
            return employee.HireDate.ToString(WebConstants.EXPANDED_DATE_FORMAT);
        }

        protected bool GetIsActive(object dataItem) {
            EmployeeVO employee = (EmployeeVO)dataItem;
            return employee.IsActive;
        }

        protected void EditingRow_Handler(object sender, GridViewEditEventArgs e) {
            ListEmployeesGridView.EditIndex = e.NewEditIndex;
            ListEmployeesGridView.SelectRow(e.NewEditIndex);
            InitializeListEmployeesGridView();
            DataKey employeeDK = ListEmployeesGridView.DataKeys[e.NewEditIndex];
            EmployeeManagementBO bo = new EmployeeManagementBO();
            EmployeeVO employee = bo.GetEmployee((int)employeeDK.Value);
            ((Calendar)ListEmployeesGridView.SelectedRow.FindControl("birthdayCalendar")).SelectedDate = employee.Birthday;
            ((Calendar)ListEmployeesGridView.SelectedRow.FindControl("birthdayCalendar")).VisibleDate = employee.Birthday;
            ((Calendar)ListEmployeesGridView.SelectedRow.FindControl("hiredateCalendar")).SelectedDate = employee.HireDate;
            ((Calendar)ListEmployeesGridView.SelectedRow.FindControl("hiredateCalendar")).VisibleDate = employee.HireDate;

        }

        protected void UpdatingRow_Handler(object sender, GridViewUpdateEventArgs e) {
            EmployeeManagementBO bo = new EmployeeManagementBO();
            DataKey employeeDK = ListEmployeesGridView.DataKeys[e.RowIndex];
            EmployeeVO employee = bo.GetEmployee((int)employeeDK.Value);
            employee.FirstName = (String)e.NewValues[0];
            employee.MiddleName = (String)e.NewValues[1];
            employee.LastName = (String)e.NewValues[2];
            employee.Birthday = ((Calendar)ListEmployeesGridView.SelectedRow.FindControl("birthdayCalendar")).SelectedDate;
            employee.HireDate = ((Calendar)ListEmployeesGridView.SelectedRow.FindControl("hiredateCalendar")).SelectedDate;
            employee.IsActive = ((CheckBox)ListEmployeesGridView.SelectedRow.FindControl("editIsActiveCheckBox")).Checked;
            bo.UpdateEmployee(employee);
            ListEmployeesGridView.EditIndex = -1;
            InitializeListEmployeesGridView();
        }

        protected void EditCanceling_Handler(object sender, GridViewCancelEditEventArgs e) {
            ListEmployeesGridView.EditIndex = -1;
            InitializeListEmployeesGridView();

        }

        protected void PageIndexChanging_Handler(object sender, GridViewPageEventArgs e) {
            ListEmployeesGridView.PageIndex = e.NewPageIndex;
            InitializeListEmployeesGridView();
        }


        protected void OnSorting_Handler(object sender, GridViewSortEventArgs e) {
            base.SortExpression = e.SortExpression;
            base.ToggleSortDirection();
            this.InitializeListEmployeesGridView();
        }


        protected void InitializeListEmployeesGridView() {
            EmployeeManagementBO bo = new EmployeeManagementBO();
            Employees = bo.GetAllEmployees();

            if (Employees != null) {
                // First, sort it into a known unique order, which is by EmployeeID
                GenericItemComparer<EmployeeVO> comparePersonByExpression =
                      new GenericItemComparer<EmployeeVO>("EmployeeID", BusinessConstants.SORT_ASCENDING);

                Employees.Sort(comparePersonByExpression);

                // Then, sort it as it was asked
                if (!String.IsNullOrEmpty(base.SortExpression)) {
                    comparePersonByExpression =
                          new GenericItemComparer<EmployeeVO>(base.SortExpression, base.SortDirection);
                    Employees.Sort(comparePersonByExpression);
                }

            }

            ListEmployeesGridView.DataSource = Employees;
            ListEmployeesGridView.DataBind();
        }


        public void ExportDataGrid(object sender, EventArgs e) {
            Exporter exporter = new Exporter();

            GridView exportGV = ExportOnlyGridView;
            exportGV.Visible = true;

            EmployeeManagementBO bo = new EmployeeManagementBO();
            exportGV.DataSource = bo.GetAllEmployees();

            exportGV.DataBind();

            exporter.ExportReportData(exportGV, "Employee List");
        }


        /**************************************************************
        * IMPORTANT -- MUST HAVE IN CODE BEHIND TO SUPPORT EXPORT
        * ************************************************************/
        public override void VerifyRenderingInServerForm(Control control) {
            // empty method for export button...
        }

    }
}
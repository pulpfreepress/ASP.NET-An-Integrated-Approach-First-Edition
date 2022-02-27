using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

using Web.App_Code;
using Infrastructure.ValueObjects;
using BusinessLogic.BO;
using BusinessLogic.Utils;

namespace Web.Controls {
    public partial class EmployeesGridViewControl : BaseControl {

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                InitializeListEmployeesGridView();
            }
        }

        public List<EmployeeVO> Employees {
            get;
            set;
        }

        private SmartCalendar BirthdayCalendar {
            get { 
               return ((SmartCalendar)ListEmployeesGridView.SelectedRow.FindControl("birthdayCalendar")); 
            }
        }

        private SmartCalendar HiredateCalendar {
            get { 
               return ((SmartCalendar)ListEmployeesGridView.SelectedRow.FindControl("hiredateCalendar")); 
            }
        }

        private CheckBox IsActiveCheckBox {
            get { 
               return ((CheckBox)ListEmployeesGridView.SelectedRow.FindControl("editIsActiveCheckBox")); 
            }
        }

        private bool Editing {
            get { return (bool)ViewState["EDITING"]; }
            set { ViewState.Add("EDITING", value); }
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


        protected void RowDataBound_Handler(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                if (((EmployeeVO)e.Row.DataItem).Birthday.Month == DateTime.Now.Month) {
                    e.Row.Cells[4].BackColor = Color.LightGray;
                    e.Row.Cells[4].ForeColor = Color.Black;
                }
            }
        }


        protected void EditingRow_Handler(object sender, GridViewEditEventArgs e) {
            Editing = true;
            ListEmployeesGridView.EditIndex = e.NewEditIndex;
            ListEmployeesGridView.SelectRow(e.NewEditIndex);
            InitializeListEmployeesGridView();
            DataKey employeeDK = ListEmployeesGridView.DataKeys[e.NewEditIndex];
            EmployeeManagementBO bo = new EmployeeManagementBO();
            EmployeeVO employee = bo.GetEmployee((int)employeeDK.Value);
            BirthdayCalendar.PopulateDropDowns();
            HiredateCalendar.PopulateDropDowns();
            BirthdayCalendar.SetSelectedAndVisibleDate(employee.Birthday);
            HiredateCalendar.SetSelectedAndVisibleDate(employee.HireDate);
           
        }

        protected void UpdatingRow_Handler(object sender, GridViewUpdateEventArgs e) {
            EmployeeManagementBO bo = new EmployeeManagementBO();
            DataKey employeeDK = ListEmployeesGridView.DataKeys[e.RowIndex];
            EmployeeVO employee = bo.GetEmployee((int)employeeDK.Value);
            employee.FirstName = (String)e.NewValues[0];
            employee.MiddleName = (String)e.NewValues[1];
            employee.LastName = (String)e.NewValues[2];
            employee.Birthday = BirthdayCalendar.SelectedDate;
            employee.HireDate = HiredateCalendar.SelectedDate;
            employee.IsActive = IsActiveCheckBox.Checked;
            bo.UpdateEmployee(employee);
            ListEmployeesGridView.EditIndex = -1;
            InitializeListEmployeesGridView();
            Editing = false;
        }

        protected void EditCanceling_Handler(object sender, GridViewCancelEditEventArgs e) {
            ListEmployeesGridView.EditIndex = -1;
            LogDebug("Cancel Edit: " + Editing + " " + Session[WebConstants.NEW_EMPLOYEE]);
            if ((!Editing) && (Session[WebConstants.NEW_EMPLOYEE] != null)) {
       
              EmployeeManagementBO bo = new EmployeeManagementBO();
              bo.DeleteEmployee((EmployeeVO)Session[WebConstants.NEW_EMPLOYEE]);
              Session.Add(WebConstants.NEW_EMPLOYEE, null);
            }
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

        protected void AddNewRecord_Handler(object sender, EventArgs e) {
            Editing = false;
            EmployeeVO vo = new EmployeeVO();
            vo.Birthday = DateTime.Now;
            vo.HireDate = DateTime.Now;
            EmployeeManagementBO bo = new EmployeeManagementBO();
            vo = bo.CreateEmployee(vo);
            Session.Add(WebConstants.NEW_EMPLOYEE, vo);
            InitializeListEmployeesGridView();
            ListEmployeesGridView.SetPageIndex(ListEmployeesGridView.PageCount - 1);
            ListEmployeesGridView.EditIndex = ListEmployeesGridView.Rows.Count - 1;
            ListEmployeesGridView.SelectRow(ListEmployeesGridView.EditIndex);
            ListEmployeesGridView.DataBind();
            BirthdayCalendar.PopulateDropDowns();
            HiredateCalendar.PopulateDropDowns();
            BirthdayCalendar.SetSelectedAndVisibleDate(DateTime.Now);
            HiredateCalendar.SetSelectedAndVisibleDate(DateTime.Now);
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
    }
}
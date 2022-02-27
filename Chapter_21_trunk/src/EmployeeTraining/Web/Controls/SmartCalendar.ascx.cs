using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

using Web.App_Code;

namespace Web.Controls {
    public partial class SmartCalendar : BaseControl {
        protected void Page_Load(object sender, EventArgs e) { }

        public Calendar Calendar {
            get { return smartCalendar; }
        }


        public DateTime SelectedDate {

            get { return smartCalendar.SelectedDate; }
            set { smartCalendar.SelectedDate = value; }
        }


        public DateTime VisibleDate {
            get { return smartCalendar.VisibleDate; }
            set { smartCalendar.VisibleDate = value; }
        }


        public bool Enabled {
            set {
                monthList.Enabled = value;
                yearList.Enabled = value;
                smartCalendar.Enabled = value;
            }
        }


        private void PopulateMonthList() {
            monthList.ClearSelection();
            monthList.Items.Add("January");
            monthList.Items.Add("February");
            monthList.Items.Add("March");
            monthList.Items.Add("April");
            monthList.Items.Add("May");
            monthList.Items.Add("June");
            monthList.Items.Add("July");
            monthList.Items.Add("August");
            monthList.Items.Add("September");
            monthList.Items.Add("October");
            monthList.Items.Add("November");
            monthList.Items.Add("December");

            monthList.SelectedIndex = DateTime.Now.Month - 1;
        }


        private void PopulateYearList() {
            yearList.ClearSelection();
            for (int i = (DateTime.Now.Year - 80); i < (DateTime.Now.Year + 2); i++) {
                yearList.Items.Add(i.ToString());
            }
            yearList.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = true;
        }


        public void PopulateDropDowns() {
            PopulateMonthList();
            PopulateYearList();
        }


        public void SetSelectedAndVisibleDate(DateTime date) {
            smartCalendar.SelectedDate = date;
            smartCalendar.VisibleDate = date;
        }


        public void SetCalendar(object sender, EventArgs e) {
            DateTime validDate;
            try {
                validDate = new DateTime(Int32.Parse(yearList.SelectedValue), monthList.SelectedIndex + 1, DateTime.Now.Day);
            }
            catch (Exception ex) {
                validDate = new DateTime(Int32.Parse(yearList.SelectedValue), monthList.SelectedIndex + 1, 1);
                LogError("Invalid date!", ex);
            }
            smartCalendar.SelectedDate = validDate;
            smartCalendar.VisibleDate = validDate;
        }


        protected void SetDropDowns(object sender, EventArgs e) {
            yearList.ClearSelection();
            monthList.ClearSelection();
            yearList.Items.FindByValue(smartCalendar.SelectedDate.Year.ToString()).Selected = true;
            monthList.SelectedIndex = smartCalendar.SelectedDate.Month - 1;
        }

        
        protected void DayRender(object sender, DayRenderEventArgs e) {
            if ((e.Day.Date.Day == SelectedDate.Day) && (e.Day.Date.Month == SelectedDate.Month)) {
                e.Cell.BackColor = Color.Gray; ;
                e.Cell.ForeColor = Color.White;
            }
        }
        
    }
}
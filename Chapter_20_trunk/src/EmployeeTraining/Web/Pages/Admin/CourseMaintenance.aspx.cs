using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Pages.Admin {
    public partial class CourseMaintenance : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                CoursesGridView.SelectedIndex = 0;
                CourseDetailsView.ChangeMode(DetailsViewMode.ReadOnly);
            }
        }


        protected void SelectedIndexChangedHandler(object sender, EventArgs args) {
            CourseDetailsView.Visible = true;
            detailsViewDiv.Visible = true;
        }

        protected void ItemInsertedHandler(object sender, DetailsViewInsertedEventArgs args) {
            CoursesGridView.DataBind();  
        }

        protected void ItemUpdatedHandler(object sender, DetailsViewUpdatedEventArgs args) {
            CoursesGridView.DataBind();
        }

        protected void NewCourseHandler(object sender, EventArgs e) {
            CourseDetailsView.ChangeMode(DetailsViewMode.Insert);
        }  
    }
}
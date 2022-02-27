using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Web.App_Code {
    public class BaseControl : System.Web.UI.UserControl {
        public BaseControl() {
        }

        #region Logging 

        protected void LogDebug(String msg) {
            BasePage bp = (BasePage)this.Page;
            bp.LogDebug(msg);
        }

        protected void LogDebug(String msg, Exception e) {
            BasePage bp = (BasePage)this.Page;
            bp.LogDebug(msg, e);
        }

        protected void LogWarn(String msg) {
            BasePage bp = (BasePage)this.Page;
            bp.LogWarn(msg);
        }

        protected void LogWarn(String msg, Exception e) {
            BasePage bp = (BasePage)this.Page;
            bp.LogWarn(msg, e);
        }

        protected void LogError(String msg) {
            BasePage bp = (BasePage)this.Page;
            bp.LogError(msg);
        }

        protected void LogError(String msg, Exception e) {
            BasePage bp = (BasePage)this.Page;
            bp.LogError(msg, e);
        }
        #endregion Logging

        #region Sorting

        public string SortExpression {
            get {
                BasePage bp = (BasePage)this.Page;
                return bp.SortExpression;
            }
            set {
                BasePage bp = (BasePage)this.Page;
                bp.SortExpression = value;
            }
        }

       
        public string SortDirection {
            get {
                BasePage bp = (BasePage)this.Page;
                return bp.SortDirection;
            }
        }

        
        public void ToggleSortDirection() {
            BasePage bp = (BasePage)this.Page;
            bp.ToggleSortDirection();
        }
        #endregion Sorting
    }
}
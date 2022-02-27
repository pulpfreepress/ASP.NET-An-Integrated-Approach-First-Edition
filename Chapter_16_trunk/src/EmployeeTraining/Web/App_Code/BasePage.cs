using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;

//log4net specific 
using log4net;
using log4net.Config;

using BusinessLogic.Utils;

namespace Web.App_Code {
    public class BasePage : System.Web.UI.Page {

       //logging utility
        private ILog _logger;
        private ILog _userAccessLoger;

        public BasePage()
            : this(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) {
        }

        public BasePage(Type loggingClassType) {
            _logger = LogManager.GetLogger(loggingClassType);
            _userAccessLoger = LogManager.GetLogger("UserAccessLogger");
            log4net.Config.XmlConfigurator.Configure();
            
        }


        public static void LogDebug(System.Type type, string message) {
            ILog logger = LogManager.GetLogger(type);
            logger.Debug(message);
        }


        protected void LogDebug(String msg) {
            _logger.Debug(msg);
        }


        protected void LogDebug(String msg, Exception e) {
            _logger.Debug(msg, e);
        }


        protected void LogWarn(String msg) {
            _logger.Warn(msg);
        }


        protected void LogWarn(String msg, Exception e) {
            _logger.Warn(msg, e);
        }


        protected void LogError(String msg) {
            _logger.Error(msg);
        }


        protected void LogError(String msg, Exception e) {
            _logger.Error(msg, e);
        }

        protected void LogUserAccess(String msg) {
            _userAccessLoger.Info(msg);
        }

        protected void LogUserAccess(String msg, Exception e) {
            _userAccessLoger.Info(msg, e);
        }


        #region PageNavigation
        /// <summary>
        /// Redirects to target URL passed as argument.
        /// </summary>
        /// <param name="navigationConstant">A absolute or relative URL string</param>
        public void HandlePageNavigation(String navigationConstant) {
            Response.Redirect(navigationConstant, false);
        }

        /// <summary>
        /// Redirects to target URL passed as argument and inserts a message string into 
        /// the Session object using the key "SESSION_NAV_MESSAGE_KEY" which is defined
        /// in the WebConstants class.
        /// </summary>
        /// <param name="navigationConstant">Absolute or relative URL string</param>
        /// <param name="displayMessage">Message string inserted into the Session object</param>
        public void HandlePageNavigation(String navigationConstant, string displayMessage) {
            Session.Add(WebConstants.SESSION_NAV_MESSAGE_KEY, displayMessage);
            HandlePageNavigation(navigationConstant);
        }


        #endregion PageNavigation

        #region Sorting
        /// <summary>
        /// This is a property to keep track of the sort expression on a given page.
        /// It is used to store the sort expression across method calls.  Otherwise,
        /// .net might undo the sort when the page_load is called again after the sort
        /// routine.  Essentially, the page_load needs to also do a sort, and this
        /// sort expression is stored for the sake of the page_load.
        /// </summary>
        public string SortExpression {
            get {
                object o = ViewState[WebConstants.SORT_EXPRESSION];
                return o == null ? String.Empty : (string)o;
            }
            set {
                ViewState[WebConstants.SORT_EXPRESSION] = value;
            }
        }

        /// <summary>
        /// This is a property to keep track of the sort direction on a given page
        /// It is a read only property, which uses the ViewState to keep track of the last
        /// request for a sort, which was set by the ToggleSortDirection method.  This was not
        /// made a set, since the value did not make sense to set.
        /// </summary>
        public string SortDirection {
            get {
                object o = ViewState[WebConstants.SORT_DIRECTION];
                return o == null ? String.Empty : (string)o;
            }
        }

        /// <summary>
        /// Change the sort direction for the next sort attempt, so it can toggle between ascending
        /// and descending sorts.  (Otherwise, .net will always sort things ascending.)  By default,
        /// the first sort will be ascending.
        /// </summary>
        public void ToggleSortDirection() {
            object o = ViewState[WebConstants.SORT_DIRECTION];

            // Figure out if a sort direction is stored in the ViewState and initialize
            // it to ascending if there isn't one stored yet
            if (o == null) {
                ViewState[WebConstants.SORT_DIRECTION] = BusinessConstants.SORT_ASCENDING;
            }
            // Toggle the sort to the other direction for the next time the get is called
            else {
                if (SortDirection.Equals(BusinessConstants.SORT_ASCENDING)) {
                    ViewState[WebConstants.SORT_DIRECTION] = BusinessConstants.SORT_DESCENDING;
                }
                else {
                    ViewState[WebConstants.SORT_DIRECTION] = BusinessConstants.SORT_ASCENDING;
                }
            }
        }

        #endregion Sorting

    }
}
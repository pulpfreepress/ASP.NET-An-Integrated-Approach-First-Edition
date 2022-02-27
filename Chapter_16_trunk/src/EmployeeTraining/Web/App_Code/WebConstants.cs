using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.App_Code {
    public class WebConstants {

        //Session keys
        public const string SESSION_NAV_MESSAGE_KEY = "SessionNavMessage";
        public const string EMPLOYEE_ID = "EmployeeID";

        //Page navigation constants
        public const string LIST_EMPLOYEES_PAGE = "~/Pages/Employee/ListEmployees.aspx";
        public const string CREATE_EMPLOYEE_PAGE = "~/Pages/Employee/CreateEmployee.aspx";
        public const string EDIT_EMPLOYEE_PAGE = "~/Pages/Employee/EditEmployee.aspx";
        public const string DEFAULT_PAGE = "~/Default.aspx";
        public const string EMPLOYEE_DETAILS_PAGE = "~/Pages/Employee/EmployeeDetails.aspx";

        //Help page URL constant
        public const string HELP_PAGE = "/Pages/Help/UserManual.html";

        //Application Settings (defined in the Web.Config file)
        public const String APPLICATION_SETTING_APP_SERVER_NAME = "AppServerName";

        //Formatting constants
        public const String ABBREVIATED_DATE_FORMAT = "dd MMM yyyy";
        public const String EXPANDED_DATE_FORMAT = "dd MMMMMMMMM yyyy";

        //Common ViewState constants
        public const String SORT_EXPRESSION = "SortExpression";
        public const String SORT_DIRECTION = "SortDirection";
        
    }
}
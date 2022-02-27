using System;
using System.Web;
using System.Text;
using Web.App_Code;


namespace Web.Modules {
    public class RequestLoggerModule : IHttpModule {
       
        #region IHttpModule Members

        public void Dispose() {
            //clean-up code here.
        }

        public void Init(HttpApplication context) {
           
            context.LogRequest += new EventHandler(OnLogRequest);
        }

        #endregion

        public void OnLogRequest(Object source, EventArgs e) {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            StringBuilder sb = new StringBuilder();
            sb.Append(context.Request.LogonUserIdentity.Name + " " + context.Request.RequestType + " ");
            sb.Append(context.Request.Url + " " + context.Request.UserHostAddress + " ");
            BasePage.LogRequest(sb.ToString());
        }
    }
}

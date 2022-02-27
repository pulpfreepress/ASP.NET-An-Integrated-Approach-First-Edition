﻿using System;
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

    }
}
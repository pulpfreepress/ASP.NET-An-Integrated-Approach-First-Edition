using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

//log4net specific 
using log4net;
using log4net.Config;

using BusinessLogic.Utils;

namespace Web.App_Code {
    
    [HandleError]
    public class BaseController : System.Web.Mvc.Controller {

         //logging utility
        private ILog _logger;
        private ILog _userAccessLoger;

        public BaseController()
            : this(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) {
        }

        public BaseController(Type loggingClassType) {
            _logger = LogManager.GetLogger(loggingClassType);
            _userAccessLoger = LogManager.GetLogger("UserAccessLogger");
            log4net.Config.XmlConfigurator.Configure();
            
        }


        public static void LogDebug(System.Type type, string message) {
            ILog logger = LogManager.GetLogger(type);
            logger.Debug(message);
        }


        public void LogDebug(String msg) {
            _logger.Debug(msg);
        }


        public void LogDebug(String msg, Exception e) {
            _logger.Debug(msg, e);
        }


        public void LogWarn(String msg) {
            _logger.Warn(msg);
        }


        public void LogWarn(String msg, Exception e) {
            _logger.Warn(msg, e);
        }


        public void LogError(String msg) {
            _logger.Error(msg);
        }


        public void LogError(String msg, Exception e) {
            _logger.Error(msg, e);
        }

        public void LogUserAccess(String msg) {
            _userAccessLoger.Info(msg);
        }

        public void LogUserAccess(String msg, Exception e) {
            _userAccessLoger.Info(msg, e);
        }
    }
}
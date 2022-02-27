/**
 * Project: Employee Training
 * Author:  Rick Miller
 * Creation: 11 November 2012
 *
 * Change Log:
 * Date		Author		Description
 *--------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Text;


//log4net specific 
using log4net;
using log4net.Config;


namespace Infrastructure {
    /// <summary>
    /// BaseObject provides common functionality to all derived classes. It mostly provides 
    /// logging behavior via log4net.
    /// </summary>
    public class BaseObject {
        //logging utility
        private ILog _logger;
        private ILog _userAccessLoger;


        /// <summary>
        /// BaseObject default constructor
        /// </summary>
        public BaseObject()
            : this(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) {
        }


        /// <summary>
        /// BaseObject constructor
        /// </summary>
        /// <param name="loggingClassType"></param>
        public BaseObject(Type loggingClassType) {
            _logger = LogManager.GetLogger(loggingClassType);
            _userAccessLoger = LogManager.GetLogger("UserAccessLogger");
            log4net.Config.XmlConfigurator.Configure();
            
        }

        /// <summary>
        /// Writes debug level logging information to the log file
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        public static void LogDebug(System.Type type, string message) {
            ILog logger = LogManager.GetLogger(type);
            logger.Debug(message);
        }


        /// <summary>
        /// Writes debug level logging information to the log file
        /// </summary>
        /// <param name="msg"></param>
        protected void LogDebug(String msg) {
            _logger.Debug(msg);
        }

        /// <summary>
        /// Writes debug level logging information to the log file
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="e"></param>
        protected void LogDebug(String msg, Exception e) {
            _logger.Debug(msg, e);
        }


        /// <summary>
        /// Writes warning level logging information to the log file
        /// </summary>
        /// <param name="msg"></param>
        protected void LogWarn(String msg) {
            _logger.Warn(msg);
        }

        /// <summary>
        /// Writes warning level logging information to the log file
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="e"></param>
        protected void LogWarn(String msg, Exception e) {
            _logger.Warn(msg, e);
        }

        /// <summary>
        /// Writes error level logging information to the log file
        /// </summary>
        /// <param name="msg"></param>
        protected void LogError(String msg) {
            _logger.Error(msg);
        }

        /// <summary>
        /// Writes error level logging information to the log file
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="e"></param>
        protected void LogError(String msg, Exception e) {
            _logger.Error(msg, e);
        }


        /// <summary>
        /// Writes user access logging information to the log file
        /// </summary>
        /// <param name="msg"></param>
        protected void LogUserAccess(String msg) {
            _userAccessLoger.Info(msg);
        }

        /// <summary>
        /// Writes user access logging information to the log file
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="e"></param>
        protected void LogUserAccess(String msg, Exception e) {
            _userAccessLoger.Info(msg, e);
        }

    } // end BaseObject class

} // end namespace


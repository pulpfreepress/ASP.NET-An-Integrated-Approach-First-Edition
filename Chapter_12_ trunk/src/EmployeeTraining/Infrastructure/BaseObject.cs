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


namespace Infrastructure
{
    public class BaseObject
    {
        //logging utility
        private ILog _logger;

        public BaseObject()
            : this(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
        {
        }

        public BaseObject(Type loggingClassType)
        {
            _logger = LogManager.GetLogger(loggingClassType);
            log4net.Config.XmlConfigurator.Configure();
        }


        public static void LogDebug(System.Type type, string message)
        {
            ILog logger = LogManager.GetLogger(type);
            logger.Debug(message);
        }


        protected void LogDebug(String msg)
        {
            _logger.Debug(msg);
        }


        protected void LogDebug(String msg, Exception e)
        {
            _logger.Debug(msg, e);
        }


        protected void LogWarn(String msg)
        {
            _logger.Warn(msg);
        }


        protected void LogWarn(String msg, Exception e)
        {
            _logger.Warn(msg, e);
        }


        protected void LogError(String msg)
        {
            _logger.Error(msg);
        }


        protected void LogError(String msg, Exception e)
        {
            _logger.Error(msg, e);
        }

    } // end BaseObject class

} // end namespace


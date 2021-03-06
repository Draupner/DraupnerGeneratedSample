using System.Collections;
using log4net;
using Elmah;

/*
 * A hook-in logger for ELMAH Exception reporting
 * 
 * Extends the XML logger but does not produce any XML files.
 * Uses instead Log4Net to log the exception and circumvents 
 * the ELMAH logging to file
 */
namespace Library.Web.Common.Logging
{
    public class Log4NetElmahErrorLog : XmlFileErrorLog 
    {
        /**
         * "Empty" Constructor required by ELMAH - Forwards call to XmlFileErrorLog
         */
        public Log4NetElmahErrorLog(IDictionary config) : base(config)
        {
        }

        /**
         * "Empty" Constructor required by ELMAH - Forwards call to XmlFileErrorLog
         */
        public Log4NetElmahErrorLog(string logpath) : base(logpath)
        {
        }

        /**
         * Catches the logging of the exception and redirects it to Log4Net
         */
        readonly ILog log = LogManager.GetLogger(typeof(Log4NetElmahErrorLog));
        public override string Log(Error error)
        {
            if (error != null)
            {
                log.Error(error.Message, error.Exception);
            } else
            {
                log.Error("The error supplied by ELMAH was null");
            }

            return base.Log(error);
        }
    }
}
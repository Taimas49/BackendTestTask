using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Utility
{
    public class AppLogger : ILogger
    {
        private static AppLogger instance;
        private static Logger logger;

        private AppLogger()
        {

        }

        public static AppLogger GetInstance()
        {
            if (instance == null)
            {
                instance = new AppLogger();
            }
            return instance;
        }

        private Logger GetLogger(string theLogger)
        {
            if (AppLogger.logger == null)
            {
                AppLogger.logger = LogManager.GetLogger(theLogger);
            }
            return AppLogger.logger;
        }


        public void Debug(string message, string arg = null)
        {
            if (arg == null)
            {
                GetLogger("MyParserLoggerRule").Debug(message);
            }
            else
            {
                GetLogger("MyParserLoggerRule").Debug(message, arg);
            }
        }

        public void Error(string message, string arg = null)
        {
            if (arg == null)
            {
                GetLogger("MyParserLoggerRule").Error(message);
            }
            else
            {
                GetLogger("MyParserLoggerRule").Error(message, arg);
            }
        }

        public void Info(string message, string arg = null)
        {
            if (arg == null)
            {
                GetLogger("MyParserLoggerRule").Info(message);
            }
            else
            {
                GetLogger("MyParserLoggerRule").Info(message, arg);
            }
        }

        public void Warning(string message, string arg = null)
        {
            if (arg == null)
            {
                GetLogger("MyParserLoggerRule").Warn(message);
            }
            else
            {
                GetLogger("MyParserLoggerRule").Warn(message, arg);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace DevFramework.Northwind.Business.Logging.Log4Net.Loggers
{
    public class DatabaseLogger : LoggerService
    {
        public DatabaseLogger() : base(LogManager.GetLogger(typeof(DatabaseLogger)))
        {
        }
    }
}

using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFramework.Northwind.Business.Logging.Log4Net.Loggers
{
    public class FileLogger : LoggerService
    {
        public FileLogger() : base(LogManager.GetLogger(typeof(FileLogger)))
        {

        }
    }
}

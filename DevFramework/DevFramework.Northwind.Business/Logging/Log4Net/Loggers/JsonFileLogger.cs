using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFramework.Northwind.Business.Logging.Log4Net.Loggers
{
    public class JsonFileLogger : LoggerService
    {
        public JsonFileLogger() : base(LogManager.GetLogger(typeof(JsonFileLogger)))
        {

        }
    }
}

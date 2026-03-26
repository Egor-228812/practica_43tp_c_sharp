using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class LoggerManager<T>
    {
        private ILogger<T> logger;

        public LoggerManager(ILogger<T> logger)
        {
            this.logger = logger;
        }

        public void LogError(T message)
        {
            logger.Log(message);
        }

        public void LogWarning(T message)
        {
            logger.Log(message);
        }
    }

}

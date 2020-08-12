using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SpeedControl.Controllers
{
    public abstract class SpeedController : ApiController
    {
        private const string START_SERVICE_TIME_APP_SETTINGS_KEY = "startTime";

        private const string END_SERVICE_TIME_APP_SETTINGS_KEY = "endTime";

        /// <summary>
        /// Method determining if service is working now and it can process data requests
        /// </summary>
        /// <returns>True if data request can be processed; otherwise false</returns>
        protected bool IsRequestCanBeProcessed()
        {
            TimeSpan startTime = TimeSpan.Parse(ConfigurationManager.AppSettings[START_SERVICE_TIME_APP_SETTINGS_KEY]);

            if (DateTime.Now.TimeOfDay > startTime)
            {
                TimeSpan endTime = TimeSpan.Parse(ConfigurationManager.AppSettings[END_SERVICE_TIME_APP_SETTINGS_KEY]);

                if (DateTime.Now.TimeOfDay < endTime)
                {
                    // If data request is being made in a time span between configured start time and end time, then it can be processed
                    return true;
                }
            }

            return false;
        }
    }
}

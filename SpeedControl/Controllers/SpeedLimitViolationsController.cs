using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Newtonsoft.Json;
using SpeedControl.Models;

namespace SpeedControl.Controllers
{
    [System.Web.Http.RoutePrefix("api/SpeedLimitViolations")]
    public class SpeedLimitViolationsController : SpeedController
    {
        private const string SPEED_REGISTRATION_DATA_FILE_PATH_KEY = "speedRegistrationDataFilePath";

        private const string DATA_REQUESTS_INFORMATION_FILE_PATH_KEY = "dataRequestsInformationFilePath";

        private readonly string SPEED_REGISTRATION_NOTES_FILE_NAME =
            ConfigurationManager.AppSettings[SPEED_REGISTRATION_DATA_FILE_PATH_KEY];

        private readonly string DATA_REQUESTS_FILE_NAME =
            ConfigurationManager.AppSettings[DATA_REQUESTS_INFORMATION_FILE_PATH_KEY];

        private const string LIMIT_EXCEEDING_NOTES_DATA_REQUEST_TEMPLATE = "Request for registered speed notes exceeding limit {0} km/h for date: {1}";

        private void SaveDataRequest(double limit, DateTime date)
        {
            // Read data requests
            string fileContent = File.ReadAllText(DATA_REQUESTS_FILE_NAME);

            // Deserialize
            DataRequest[] dataRequests = JsonConvert.DeserializeObject<DataRequest[]>(fileContent);

            // Transform array to list
            List<DataRequest> dataRequestsList = dataRequests.ToList();
            
            // Create data request object
            var request = new DataRequest()
            {
                RequestType = String.Format(LIMIT_EXCEEDING_NOTES_DATA_REQUEST_TEMPLATE, limit, date),
                RequestDate = DateTime.Now
            };

            // Add new object to the list
            dataRequestsList.Add(request);

            // Transform list back to array and serialize it
            string jsonToSaveInFile = JsonConvert.SerializeObject(dataRequestsList.ToArray());

            // Save json in the same file (instead of previous file content)
            File.WriteAllText(DATA_REQUESTS_FILE_NAME, jsonToSaveInFile);
        }

        // GET: api/Speed
        public JsonResult<SpeedRegistrationNote[]> Get(double limit, DateTime date)
        {
            if (IsRequestCanBeProcessed())
            {
                // Save information about current request in file
                SaveDataRequest(limit, date);

                // Get all registration notes from text file
                string text = System.IO.File.ReadAllText(SPEED_REGISTRATION_NOTES_FILE_NAME);

                // Deserialize
                SpeedRegistrationNote[] notes = JsonConvert.DeserializeObject<SpeedRegistrationNote[]>(text);

                // Get limit exceeding notes
                SpeedRegistrationNote[] limitExceedingNotes = notes
                    .Where(n => n.RegistrationTime.Date == date && n.Speed > limit).ToArray();

                return new JsonResult<SpeedRegistrationNote[]>(
                    limitExceedingNotes, new JsonSerializerSettings(), Encoding.Unicode, this);
            }

            return null;
        }
    }
}

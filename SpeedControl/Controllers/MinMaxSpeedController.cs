using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using SpeedControl.Models;

namespace SpeedControl.Controllers
{
    [RoutePrefix("api/MinMaxSpeed")]
    public class MinMaxSpeedController : SpeedController
    {
        private const string SPEED_REGISTRATION_DATA_FILE_PATH_KEY = "speedRegistrationDataFilePath";

        private const string DATA_REQUESTS_INFORMATION_FILE_PATH_KEY = "dataRequestsInformationFilePath";

        private readonly string SPEED_REGISTRATION_NOTES_FILE_NAME =
            ConfigurationManager.AppSettings[SPEED_REGISTRATION_DATA_FILE_PATH_KEY];

        private readonly string DATA_REQUESTS_FILE_NAME =
            ConfigurationManager.AppSettings[DATA_REQUESTS_INFORMATION_FILE_PATH_KEY];

        private const string MIN_MAX_SPEED_NOTES_DATA_REQUEST_TEMPLATE = "Request for minimal and maximal registered speeds for date: {0}";

        private void SaveDataRequest(DateTime date)
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
                RequestType = String.Format(MIN_MAX_SPEED_NOTES_DATA_REQUEST_TEMPLATE, date),
                RequestDate = DateTime.Now
            };

            // Add new object to the list
            dataRequestsList.Add(request);

            // Transform list back to array and serialize it
            string jsonToSaveInFile = JsonConvert.SerializeObject(dataRequestsList.ToArray());

            // Save json in the same file (instead of previous file content)
            File.WriteAllText(DATA_REQUESTS_FILE_NAME, jsonToSaveInFile);
        }

        public JsonResult<MinMaxSpeedModel> Get(DateTime date)
        {
            //var date = DateTime.Parse(dateStr);

            if (IsRequestCanBeProcessed())
            {
                // Save information about current data request
                SaveDataRequest(date);

                // Get all registration notes from text file
                string text = System.IO.File.ReadAllText(SPEED_REGISTRATION_NOTES_FILE_NAME);

                // Deserialize
                SpeedRegistrationNote[] notes = JsonConvert.DeserializeObject<SpeedRegistrationNote[]>(text);

                // Filter notes for a given date
                SpeedRegistrationNote[] notesForDate = notes
                    .Where(n => n.RegistrationTime.Date == date.Date).ToArray();

                // Max speed note
                SpeedRegistrationNote maxSpeedNote = notesForDate.FirstOrDefault();

                // Min speed note
                SpeedRegistrationNote minSpeedNote = maxSpeedNote;

                if (maxSpeedNote != null)
                {
                    foreach (SpeedRegistrationNote note in notesForDate)
                    {
                        if (note.Speed > maxSpeedNote.Speed)
                        {
                            maxSpeedNote = note;
                        }
                        else if (note.Speed < minSpeedNote.Speed)
                        {
                            minSpeedNote = note;
                        }
                    }
                }

                // Create model
                var modelToReturn = new MinMaxSpeedModel()
                {
                    MinSpeedNote = minSpeedNote,
                    MaxSpeedNote = maxSpeedNote
                };

                return new JsonResult<MinMaxSpeedModel>(
                    modelToReturn, new JsonSerializerSettings(), Encoding.Unicode, this);
            }

            return null;
        }
    }
}

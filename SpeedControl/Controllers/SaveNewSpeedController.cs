using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Http;
using Newtonsoft.Json;
using SpeedControl.Models;

namespace SpeedControl.Controllers
{
    [RoutePrefix("api/SaveNewSpeed")]
    public class SaveNewSpeedController : ApiController
    {
        private const string SPEED_REGISTRATION_DATA_FILE_PATH_KEY = "speedRegistrationDataFilePath";

        // Physical path to the file containing speed registration data
        private readonly string SPEED_REGISTRATION_DATA_FILE_PATH =
            ConfigurationManager.AppSettings[SPEED_REGISTRATION_DATA_FILE_PATH_KEY];

        public IHttpActionResult Post(SpeedRegistrationNote model)
        {
            // Read from file.
            string fileContent = File.ReadAllText(SPEED_REGISTRATION_DATA_FILE_PATH);

            // Deserialize array of SpeedRegistrationNote objects
            SpeedRegistrationNote[] notes = JsonConvert.DeserializeObject<SpeedRegistrationNote[]>(fileContent);

            // Add new object to the deserialized array
            List<SpeedRegistrationNote> notesList = new List<SpeedRegistrationNote>(notes) { model };

            // Serialize array with the added element
            String json = JsonConvert.SerializeObject(notesList.ToArray());

            // Save array in the same file
            File.WriteAllText(SPEED_REGISTRATION_DATA_FILE_PATH, json);

            return Ok();
        }
    }
}

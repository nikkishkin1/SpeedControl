using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using SpeedControl_Core.Entities;
using SpeedControl_Core.Interfaces;

namespace SpeedControl_DAL
{
    public class SpeedNotesRepository: ISpeedNotesRepository
    {
        private const string SPEED_REGISTRATION_NOTES_FILE_PATH_KEY = "speedRegistrationNotesFilePath";

        public SpeedRegistrationNote[] GetSpeedRegistrationNotes()
        {
            string speedRegistrationNotesFilePath = ConfigurationManager.AppSettings[SPEED_REGISTRATION_NOTES_FILE_PATH_KEY];

            // Read file content
            string fileContent = File.ReadAllText(speedRegistrationNotesFilePath);

            // Deserialize
            return JsonConvert.DeserializeObject<SpeedRegistrationNote[]>(fileContent);
        }

        public void SaveSpeedRegistrationNote(SpeedRegistrationNote note)
        {
            string speedRegistrationNotesFilePath = ConfigurationManager.AppSettings[SPEED_REGISTRATION_NOTES_FILE_PATH_KEY];

            // Read file content
            string fileContent = File.ReadAllText(speedRegistrationNotesFilePath);

            // Deserialize array of SpeedRegistrationNote objects
            SpeedRegistrationNote[] notes = JsonConvert.DeserializeObject<SpeedRegistrationNote[]>(fileContent);

            // Add new object to the deserialized array
            List<SpeedRegistrationNote> notesList = new List<SpeedRegistrationNote>(notes) { note };

            // Serialize array with the added element
            String json = JsonConvert.SerializeObject(notesList.ToArray());

            // Save array in the same file
            File.WriteAllText(speedRegistrationNotesFilePath, json);
        }
    }
}

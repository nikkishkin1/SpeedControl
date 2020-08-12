using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SpeedControl_Core.Entities;
using SpeedControl_Core.Interfaces;

namespace SpeedControl_DAL
{
    public class DataRequestsRepository: IDataRequestsRepository
    {
        private const string DATA_REQUESTS_FILE_PATH_KEY = "dataRequestsFilePath";

        public DataRequest[] GetDataRequests()
        {
            string dataRequestsFilePath = ConfigurationManager.AppSettings[DATA_REQUESTS_FILE_PATH_KEY];

            // Read file content
            string fileContent = File.ReadAllText(dataRequestsFilePath);

            // Deserialize
            return JsonConvert.DeserializeObject<DataRequest[]>(fileContent);
        }

        public void SaveDataRequest(DataRequest dataRequest)
        {
            string dataRequestsFilePath = ConfigurationManager.AppSettings[DATA_REQUESTS_FILE_PATH_KEY];

            // Read file content
            string fileContent = File.ReadAllText(dataRequestsFilePath);

            // Deserialize
            DataRequest[] dataRequests = JsonConvert.DeserializeObject<DataRequest[]>(fileContent);

            // Transform array to list
            List<DataRequest> dataRequestsList = dataRequests.ToList();

            // Add new object to the list
            dataRequestsList.Add(dataRequest);

            // Transform list back to array and serialize it
            string jsonToSaveInFile = JsonConvert.SerializeObject(dataRequestsList.ToArray());

            // Save json in the same file (instead of previous file content)
            File.WriteAllText(dataRequestsFilePath, jsonToSaveInFile);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Newtonsoft.Json;
using SpeedControl.Models;

namespace SpeedControl.Controllers
{
    public class DataRequestController : ApiController
    {
        private const string DATA_REQUESTS_INFORMATION_FILE_PATH_KEY = "dataRequestsInformationFilePath";

        private readonly string DATA_REQUESTS_FILE_NAME =
            ConfigurationManager.AppSettings[DATA_REQUESTS_INFORMATION_FILE_PATH_KEY];

        // GET: api/DataRequest
        public JsonResult<DataRequest[]> Get()
        {
            // Read data requests from file
            string fileContent = System.IO.File.ReadAllText(DATA_REQUESTS_FILE_NAME);

            // Deserialize
            DataRequest[] dataRequests = JsonConvert.DeserializeObject<DataRequest[]>(fileContent);

            return new JsonResult<DataRequest[]>(
                dataRequests, new JsonSerializerSettings(), Encoding.Unicode, this);
        }
    }
}

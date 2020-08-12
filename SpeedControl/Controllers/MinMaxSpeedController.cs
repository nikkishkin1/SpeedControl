using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Metadata;
using System.Web.Http.Results;
using Newtonsoft.Json;
using SpeedControl_BLL;
using SpeedControl_Core.Entities;
using SpeedControl_Core.Interfaces;
using SpeedControl_Core.Models;
using SpeedControl_DAL;

namespace SpeedControl.Controllers
{
    public class MinMaxSpeedController : SpeedController
    {
        private const string MIN_MAX_SPEED_NOTES_DATA_REQUEST_TEMPLATE = 
            "Request for minimal and maximal registered speeds for date: {0}";

        private IDataRequestsRepository _dataRequestsRepository;

        private ISpeedNotesProcessor _speedNotesProcessor;

        public MinMaxSpeedController()
        {
            _dataRequestsRepository = new DataRequestsRepository();

            _speedNotesProcessor = new SpeedNotesProcessor();
        }

        // GET: api/MinMaxSpeed
        public JsonResult<MinMaxSpeedModel> Get(DateTime date)
        {
            if (IsRequestCanBeProcessed())
            {
                // Create data request object
                var dataRequest = new DataRequest()
                {
                    RequestType = String.Format(MIN_MAX_SPEED_NOTES_DATA_REQUEST_TEMPLATE, date),
                    RequestDate = DateTime.Now
                };

                // Save information about current data request
                _dataRequestsRepository.SaveDataRequest(dataRequest);

                // Get model with information about min and max speed
                MinMaxSpeedModel modelToReturn = _speedNotesProcessor.GetMinMaxSpeedSpeedRegistrationNotes(date);

                return new JsonResult<MinMaxSpeedModel>(
                    modelToReturn, new JsonSerializerSettings(), Encoding.Unicode, this);
            }

            return null;
        }
    }
}

using System;
using System.Text;
using System.Web.Http.Results;
using System.Web.Mvc;
using Newtonsoft.Json;
using SpeedControl_BLL;
using SpeedControl_Core.Entities;
using SpeedControl_Core.Interfaces;
using SpeedControl_DAL;

namespace SpeedControl.Controllers
{
    [System.Web.Http.RoutePrefix("api/SpeedLimitViolations")]
    public class SpeedLimitViolationsController : SpeedController
    {
        private const string LIMIT_EXCEEDING_NOTES_DATA_REQUEST_TEMPLATE = 
            "Request for registered speed notes exceeding limit {0} km/h for date: {1}";

        private readonly IDataRequestsRepository _dataRequestsRepository;

        private readonly ISpeedNotesProcessor _speedNotesProcessor;

        public SpeedLimitViolationsController()
        {
            _dataRequestsRepository = new DataRequestsRepository();

            _speedNotesProcessor = new SpeedNotesProcessor();
        }

        // GET: api/Speed
        public JsonResult<SpeedRegistrationNote[]> Get(double limit, DateTime date)
        {
            if (IsRequestCanBeProcessed())
            {
                // Create data request object
                var dataRequest = new DataRequest()
                {
                    RequestType = String.Format(LIMIT_EXCEEDING_NOTES_DATA_REQUEST_TEMPLATE, limit, date),
                    RequestDate = DateTime.Now
                };

                // Save information about current data request
                _dataRequestsRepository.SaveDataRequest(dataRequest);

                // Get notes with limit violations
                SpeedRegistrationNote[] limitExceedingNotes =
                    _speedNotesProcessor.GetSpeedLimitExceedingSpeedRegistrationNotes(limit, date);

                return new JsonResult<SpeedRegistrationNote[]>(
                    limitExceedingNotes, new JsonSerializerSettings(), Encoding.Unicode, this);
            }

            return null;
        }
    }
}

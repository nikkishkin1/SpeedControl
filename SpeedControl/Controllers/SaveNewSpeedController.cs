using System.Configuration;
using System.Web.Http;
using SpeedControl_Core.Entities;
using SpeedControl_Core.Interfaces;
using SpeedControl_DAL;

namespace SpeedControl.Controllers
{
    [RoutePrefix("api/SaveNewSpeed")]
    public class SaveNewSpeedController : ApiController
    {
        private const string SPEED_REGISTRATION_DATA_FILE_PATH_KEY = "speedRegistrationDataFilePath";

        // Physical path to the file containing speed registration data
        private readonly string SPEED_REGISTRATION_DATA_FILE_PATH =
            ConfigurationManager.AppSettings[SPEED_REGISTRATION_DATA_FILE_PATH_KEY];

        private readonly ISpeedNotesRepository _speedNotesRepository;

        public SaveNewSpeedController()
        {
            _speedNotesRepository = new SpeedNotesRepository();
        }

        public IHttpActionResult Post(SpeedRegistrationNote model)
        {
            _speedNotesRepository.SaveSpeedRegistrationNote(model);

            return Ok();
        }
    }
}

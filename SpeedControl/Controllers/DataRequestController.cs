using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Newtonsoft.Json;
using SpeedControl_Core.Entities;
using SpeedControl_Core.Interfaces;
using SpeedControl_DAL;

namespace SpeedControl.Controllers
{
    public class DataRequestController : ApiController
    {
        private readonly IDataRequestsRepository _dataRequestsRepository;

        public DataRequestController()
        {
            _dataRequestsRepository = new DataRequestsRepository();
        }

        // GET: api/DataRequest
        public JsonResult<DataRequest[]> Get()
        {
            // Get data requests
            DataRequest[] dataRequests = _dataRequestsRepository.GetDataRequests();

            return new JsonResult<DataRequest[]>(dataRequests, new JsonSerializerSettings(), Encoding.Unicode, this);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeedControl_Core.Entities;

namespace SpeedControl_Core.Interfaces
{
    public interface IDataRequestsRepository
    {
        DataRequest[] GetDataRequests();

        void SaveDataRequest(DataRequest dataRequest);
    }
}

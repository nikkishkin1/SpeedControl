using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedControl.Models
{
    // Model containing information about registered notes with maximal and minimal speeds
    public class MinMaxSpeedModel
    {
        public SpeedRegistrationNote MinSpeedNote { get; set; }
        public SpeedRegistrationNote MaxSpeedNote { get; set; }
    }
}
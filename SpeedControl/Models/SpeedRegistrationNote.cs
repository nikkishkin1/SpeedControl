using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedControl.Models
{
    // Model of a speed registration note
    public class SpeedRegistrationNote
    {
        public double Speed { get; set; }

        public string LicensePlate { get; set; }

        public DateTime RegistrationTime { get; set; }
    }
}
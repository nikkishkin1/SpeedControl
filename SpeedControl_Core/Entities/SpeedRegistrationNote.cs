using System;

namespace SpeedControl_Core.Entities
{
    // Model of a speed registration note
    public class SpeedRegistrationNote
    {
        public double Speed { get; set; }

        public string LicensePlate { get; set; }

        public DateTime RegistrationTime { get; set; }
    }
}
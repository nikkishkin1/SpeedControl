using SpeedControl_Core.Entities;

namespace SpeedControl_Core.Models
{
    // Model containing information about registered notes with maximal and minimal speeds
    public class MinMaxSpeedModel
    {
        public SpeedRegistrationNote MinSpeedNote { get; set; }
        public SpeedRegistrationNote MaxSpeedNote { get; set; }
    }
}
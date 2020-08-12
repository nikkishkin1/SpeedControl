using System;
using SpeedControl_Core.Entities;
using SpeedControl_Core.Models;

namespace SpeedControl_Core.Interfaces
{
    public interface ISpeedNotesProcessor
    {
        MinMaxSpeedModel GetMinMaxSpeedSpeedRegistrationNotes(DateTime date);

        SpeedRegistrationNote[] GetSpeedLimitExceedingSpeedRegistrationNotes(double limit, DateTime date);
    }
}

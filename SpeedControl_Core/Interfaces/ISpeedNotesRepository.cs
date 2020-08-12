
using SpeedControl_Core.Entities;

namespace SpeedControl_Core.Interfaces
{
    public interface ISpeedNotesRepository
    {
        SpeedRegistrationNote[] GetSpeedRegistrationNotes();

        void SaveSpeedRegistrationNote(SpeedRegistrationNote note);
    }
}

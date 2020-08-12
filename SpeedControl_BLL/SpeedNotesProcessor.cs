using System;
using System.Linq;
using SpeedControl_Core.Entities;
using SpeedControl_Core.Interfaces;
using SpeedControl_Core.Models;
using SpeedControl_DAL;

namespace SpeedControl_BLL
{
    public class SpeedNotesProcessor: ISpeedNotesProcessor
    {
        private readonly ISpeedNotesRepository _speedNotesRepository;

        public SpeedNotesProcessor()
        {
            _speedNotesRepository = new SpeedNotesRepository();
        }

        public MinMaxSpeedModel GetMinMaxSpeedSpeedRegistrationNotes(DateTime date)
        {
            // Get all registration notes
            SpeedRegistrationNote[] notes = _speedNotesRepository.GetSpeedRegistrationNotes();

            // Max and min speed notes
            SpeedRegistrationNote maxSpeedNote = null, minSpeedNote = null;

            if (notes != null)
            {
                // Filter notes for a given date
                SpeedRegistrationNote[] notesForDate = notes
                    .Where(n => n.RegistrationTime.Date == date.Date).ToArray();

                maxSpeedNote = notesForDate.FirstOrDefault();

                minSpeedNote = maxSpeedNote;

                if (maxSpeedNote != null)
                {
                    foreach (SpeedRegistrationNote note in notesForDate)
                    {
                        if (note.Speed > maxSpeedNote.Speed)
                        {
                            maxSpeedNote = note;
                        }
                        else if (note.Speed < minSpeedNote.Speed)
                        {
                            minSpeedNote = note;
                        }
                    }
                }
            }

            // Create model and return it
            return new MinMaxSpeedModel
            {
                MinSpeedNote = minSpeedNote,
                MaxSpeedNote = maxSpeedNote
            };
        }

        public SpeedRegistrationNote[] GetSpeedLimitExceedingSpeedRegistrationNotes(double limit, DateTime date)
        {
            // Get all registration notes
            SpeedRegistrationNote[] notes = _speedNotesRepository.GetSpeedRegistrationNotes();

            if (notes != null)
            {
                // Return limit exceeding notes
                return notes.Where(n => n.RegistrationTime.Date == date && n.Speed > limit).ToArray();
            }

            return null;
        }
    }
}

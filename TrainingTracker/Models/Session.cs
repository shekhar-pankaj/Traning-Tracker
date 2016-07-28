using System;

namespace TrainingTracker.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Presenter { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime SessionDate { get; set; }
    }
}
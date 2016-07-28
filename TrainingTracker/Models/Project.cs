using System;

namespace TrainingTracker.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public string ProjectType { get; set; }
        public string ClientName { get; set; }
        public DateTime AddedOn { get; set; }

        public int UserProjectId { get; set; }
    }
}
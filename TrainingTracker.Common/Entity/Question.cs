using System.Collections.Generic;

namespace TrainingTracker.Common.Entity
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string Description { get; set; }
        public int SkillId { get; set; }
        public short Level { get; set; }
        public int AddedBy { get; set; }
        public System.DateTime AddedDate { get; set; }
        public List<QuestionLevelMapping> LevelMapping { get; set; }

        public Skill Skill { get; set; }
        public User User { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrainingTracker.DAL.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Question
    {
        public Question()
        {
            this.QuestionLevelMappings = new HashSet<QuestionLevelMapping>();
        }
    
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string Description { get; set; }
        public int SkillId { get; set; }
        public int AddedBy { get; set; }
        public System.DateTime AddedDate { get; set; }
    
        public virtual ICollection<QuestionLevelMapping> QuestionLevelMappings { get; set; }
        public virtual Skill Skill { get; set; }
        public virtual User User { get; set; }
    }
}
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
    
    public partial class SurveyAnswer
    {
        public SurveyAnswer()
        {
            this.SurveyResponses = new HashSet<SurveyResponse>();
        }
    
        public int SurveyAnswerId { get; set; }
        public int SurveyQuestionId { get; set; }
        public string OptionText { get; set; }
        public bool IsDeleted { get; set; }
        public int SortOrder { get; set; }
        public System.DateTime DateCreated { get; set; }
    
        public virtual SurveyQuestion SurveyQuestion { get; set; }
        public virtual ICollection<SurveyResponse> SurveyResponses { get; set; }
    }
}

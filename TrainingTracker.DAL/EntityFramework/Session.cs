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
    
    public partial class Session
    {
        public Session()
        {
            this.UserSessionMappings = new HashSet<UserSessionMapping>();
        }
    
        public int SessionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> Presenter { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public Nullable<System.DateTime> SessionDate { get; set; }
    
        public virtual User User { get; set; }
        public virtual ICollection<UserSessionMapping> UserSessionMappings { get; set; }
    }
}

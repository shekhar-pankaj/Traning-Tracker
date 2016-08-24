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
    
    public partial class User
    {
        public User()
        {
            this.Feedbacks = new HashSet<Feedback>();
            this.Feedbacks1 = new HashSet<Feedback>();
            this.Sessions = new HashSet<Session>();
            this.UserSessionMappings = new HashSet<UserSessionMapping>();
            this.UserSessionMappings1 = new HashSet<UserSessionMapping>();
            this.Questions = new HashSet<Question>();
        }
    
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public string ProfilePictureName { get; set; }
        public Nullable<bool> IsFemale { get; set; }
        public Nullable<bool> IsAdministrator { get; set; }
        public Nullable<bool> IsTrainer { get; set; }
        public Nullable<bool> IsTrainee { get; set; }
        public Nullable<bool> IsManager { get; set; }
        public Nullable<System.DateTime> DateAddedToSystem { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Feedback> Feedbacks1 { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<UserSessionMapping> UserSessionMappings { get; set; }
        public virtual ICollection<UserSessionMapping> UserSessionMappings1 { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}

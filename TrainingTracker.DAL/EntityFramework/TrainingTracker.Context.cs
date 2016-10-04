﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TrainingTrackerEntities : DbContext
    {
        public TrainingTrackerEntities()
            : base("name=TrainingTrackerEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<FeedbackType> FeedbackTypes { get; set; }
        public DbSet<LearningSource> LearningSources { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanSkillMapping> PlanSkillMappings { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectPlanMapping> ProjectPlanMappings { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProjectMapping> UserProjectMappings { get; set; }
        public DbSet<UserSessionMapping> UserSessionMappings { get; set; }
        public DbSet<UserSkillMapping> UserSkillMappings { get; set; }
        public DbSet<QuestionLevelMapping> QuestionLevelMappings { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<UserNotificationMapping> UserNotificationMappings { get; set; }
        public DbSet<FeedbackThread> FeedbackThreads { get; set; }
    }
}

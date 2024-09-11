using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ProfileCategory> ProfileCategories { get; set; }
        public DbSet<ProfileCategoryList> ProfileCategoryLists { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<DialyActivity> DialyActivities { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<ModuleTopic> ModuleTopics { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<TestCategory> TestCategories { get; set; }
        public DbSet<TestSheet> TestSheets { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<TrainingCategory> TrainingCategories { get; set; }
        public DbSet<TrainingFacilitator> TrainingFacilitators { get; set; }
        public DbSet<TrainingParticipant> TrainingParticipants { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<TrainingTest> TrainingTests { get; set; }
        public DbSet<UserTest> UserTests { get; set; }
        public DbSet<EvaluationQuestion> EvaluationQuestions { get; set; }
        public DbSet<DialyUserEvaluation> DialyUserEvaluations { get; set; }
        public DbSet<EvaluationQuestionCategory> EvaluationQuestionCategories { get; set; }
        public DbSet<DialyEvaluationQuestion> DialyEvaluationQuestions { get; set; }
        public DbSet<TimeTable> TimeTables { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
    }
}

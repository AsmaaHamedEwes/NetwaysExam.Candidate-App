using Domain.Entities.Entity;
using Domain.Persistance.EntitiesProperties;
using Domain.Persistance.EntitiesProperties.Candidate;
using Domain.Persistance.EntitiesProperties.Employer;
using Domain.Persistance.EntitiesProperties.Skills;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Configuration;

namespace Domain.Persistance
{
    public partial class AppDbContext : IdentityDbContext<User>
    {
       private readonly string DataBaseConnection ;

        public AppDbContext(DbContextOptions options ) : base(options)
        {
            DataBaseConnection = "Server=DESKTOP-4ATD4FS;Database=CandidateDB;Trusted_Connection=True;MultipleActiveResultSets=true";
            //ConfigurationManager.AppSettings["DefaultConnection"]; 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DataBaseConnection, x => { x.MigrationsAssembly("CandidateManagment.API"); });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserProperties());
            builder.ApplyConfiguration(new CandidateProperties());
            builder.ApplyConfiguration(new EmployerProperites());
            builder.ApplyConfiguration(new SkillsProperties());

            base.OnModelCreating(builder);
        }

        public class ApplicationContextDbFactory : IDesignTimeDbContextFactory<AppDbContext>
        {
            AppDbContext IDesignTimeDbContextFactory<AppDbContext>.CreateDbContext(string[] args)
            {
                var options = new DbContextOptionsBuilder<AppDbContext>();


                return new AppDbContext(options.Options );
            }
        }
    }
}

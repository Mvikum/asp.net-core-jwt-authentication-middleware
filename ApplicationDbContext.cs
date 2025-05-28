using Microsoft.EntityFrameworkCore;
using StudentService.Middlewares.helpers.utils;
using StudentService.Models;

namespace StudentService
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { 
        }
        
            
       
        public ApplicationDbContext()
        {
            
        }
        public virtual DbSet<StudentModel> Students { get; set; }
        public virtual DbSet<LoginDetailModel> LoginDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(GlobalAttributes.mysqlConfiguration.connectionString, ServerVersion.AutoDetect(GlobalAttributes.mysqlConfiguration.connectionString));

            }
        }
    }
}

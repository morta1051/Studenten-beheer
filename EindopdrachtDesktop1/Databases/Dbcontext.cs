using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EindopdrachtDesktop1.Model; 
using System.Configuration;
using System.ComponentModel.DataAnnotations.Schema;

namespace EindopdrachtDesktop1.Databases
{
    internal class Dbcontext : DbContext
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string connstr = ConfigurationManager.ConnectionStrings["MyConnStr"].ConnectionString;
            optionsBuilder.UseMySQL(connstr);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.CourseID);

            base.OnModelCreating(modelBuilder);
        }




    }
}

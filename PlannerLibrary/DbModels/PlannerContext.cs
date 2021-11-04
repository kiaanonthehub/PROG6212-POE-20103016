using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PlannerLibrary.DbModels
{
    public partial class PlannerContext : DbContext
    {
        public PlannerContext()
        {
        }

        public PlannerContext(DbContextOptions<PlannerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblModule> TblModules { get; set; }
        public virtual DbSet<TblSemester> TblSemesters { get; set; }
        public virtual DbSet<TblStudent> TblStudents { get; set; }
        public virtual DbSet<TblStudentModule> TblStudentModules { get; set; }
        public virtual DbSet<TblTrackStudy> TblTrackStudies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=KIAAN;Database=Planner;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblModule>(entity =>
            {
                entity.HasKey(e => e.ModuleId)
                    .HasName("PK__tblModul__1A2D065390FC65A0");

                entity.ToTable("tblModule");

                entity.Property(e => e.ModuleId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("module_id");

                entity.Property(e => e.ModuleClassHours).HasColumnName("module_class_hours");

                entity.Property(e => e.ModuleCredits).HasColumnName("module_credits");

                entity.Property(e => e.ModuleName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("module_name");
            });

            modelBuilder.Entity<TblSemester>(entity =>
            {
                entity.HasKey(e => e.SemesterId)
                    .HasName("PK__tblSemes__CBC81B013DD9ED48");

                entity.ToTable("tblSemester");

                entity.Property(e => e.SemesterId).HasColumnName("semester_id");

                entity.Property(e => e.NumberOfWeeks).HasColumnName("number_of_weeks");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");
            });

            modelBuilder.Entity<TblStudent>(entity =>
            {
                entity.HasKey(e => e.StudentNumber)
                    .HasName("PK__tblStude__0E749A78786C0851");

                entity.ToTable("tblStudent");

                entity.Property(e => e.StudentNumber)
                    .ValueGeneratedNever()
                    .HasColumnName("student_number");

                entity.Property(e => e.StudentEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("student_email");

                entity.Property(e => e.StudentHashPassword)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("student_hash_password");

                entity.Property(e => e.StudentName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("student_name");

                entity.Property(e => e.StudentSurname)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("student_surname");
            });

            modelBuilder.Entity<TblStudentModule>(entity =>
            {
                entity.HasKey(e => e.StudentModuleId)
                    .HasName("PK__tblStude__EC62D1BA31B12063");

                entity.ToTable("tblStudentModule");

                entity.Property(e => e.StudentModuleId).HasColumnName("student_module_id");

                entity.Property(e => e.ModuleId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("module_id");

                entity.Property(e => e.ModuleSelfStudyHour)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("module_self_study_hour");

                entity.Property(e => e.StudentNumber).HasColumnName("student_number");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.TblStudentModules)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK__tblStuden__modul__2B3F6F97");

                entity.HasOne(d => d.StudentNumberNavigation)
                    .WithMany(p => p.TblStudentModules)
                    .HasForeignKey(d => d.StudentNumber)
                    .HasConstraintName("FK__tblStuden__stude__2A4B4B5E");
            });

            modelBuilder.Entity<TblTrackStudy>(entity =>
            {
                entity.HasKey(e => e.TrackStudiesId)
                    .HasName("PK__tblTrack__92A79430EAB871A4");

                entity.ToTable("tblTrackStudies");

                entity.Property(e => e.TrackStudiesId).HasColumnName("track_studies_id");

                entity.Property(e => e.DateWorked)
                    .HasColumnType("date")
                    .HasColumnName("date_worked");

                entity.Property(e => e.HoursWorked)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("hours_worked");

                entity.Property(e => e.ModuleId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("module_id");

                entity.Property(e => e.SemesterId).HasColumnName("semester_id");

                entity.Property(e => e.StudentNumber).HasColumnName("student_number");

                entity.Property(e => e.WeekNumber).HasColumnName("week_number");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.TblTrackStudies)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK__tblTrackS__modul__2F10007B");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.TblTrackStudies)
                    .HasForeignKey(d => d.SemesterId)
                    .HasConstraintName("FK__tblTrackS__semes__2E1BDC42");

                entity.HasOne(d => d.StudentNumberNavigation)
                    .WithMany(p => p.TblTrackStudies)
                    .HasForeignKey(d => d.StudentNumber)
                    .HasConstraintName("FK__tblTrackS__stude__300424B4");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

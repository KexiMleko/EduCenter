using EduCenter.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EduCenter.API.Data;
public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    { }

    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<TeacherSubject> TeacherSubjects { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<PaymentPlan> PaymentPlans { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<LevelOfStudy> LevelsOfStudy { get; set; }
    public DbSet<IndividualSession> IndividualSessions { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<GroupSession> GroupSessions { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigureUserRole(modelBuilder);
        ConfigureRefreshToken(modelBuilder);
        ConfigureStudent(modelBuilder);
        ConfigureGroupSession(modelBuilder);
        ConfigureAttendance(modelBuilder);
        ConfigureGroup(modelBuilder);
        ConfigureTeacherSubject(modelBuilder);
        ConfigureIndividualSession(modelBuilder);
        ConfigureEnrollment(modelBuilder);
        ConfigurePayment(modelBuilder);
    }
    public void ConfigureUserRole(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasOne<User>()
                  .WithMany()
                  .HasForeignKey(ur => ur.UserId).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<Role>()
                  .WithMany()
                  .HasForeignKey(ur => ur.RoleId).OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(ur => new { ur.UserId, ur.RoleId })
                  .IsUnique();
        });
    }
    public void ConfigureRefreshToken(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasOne<User>().WithMany().HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(rt => new { rt.UserId, rt.Token }).IsUnique();
        });
    }
    public void ConfigureStudent(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasOne<LevelOfStudy>().WithMany().HasForeignKey(r => r.LevelId).OnDelete(DeleteBehavior.NoAction);
        });
    }
    public void ConfigureTeacherSubject(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TeacherSubject>(entity =>
            {
                entity.HasOne<User>()
                      .WithMany()
                      .HasForeignKey(ts => ts.TeacherId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne<Subject>()
                      .WithMany()
                      .HasForeignKey(ts => ts.SubjectId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<LevelOfStudy>()
                      .WithMany()
                      .HasForeignKey(ts => ts.LevelId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(ts => new { ts.TeacherId, ts.SubjectId, ts.LevelId })
                      .IsUnique();
            });
    }
    public void ConfigureGroup(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasOne<User>()
                      .WithMany()
                      .HasForeignKey(g => g.TeacherId)
                      .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne<Subject>()
                  .WithMany()
                  .HasForeignKey(g => g.SubjectId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
    public void ConfigureGroupSession(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GroupSession>(entity =>
        {
            entity.HasOne<User>()
                      .WithMany()
                      .HasForeignKey(g => g.TeacherId)
                      .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne<Group>()
                  .WithMany()
                  .HasForeignKey(g => g.GroupId)
                  .OnDelete(DeleteBehavior.NoAction);
        });
    }
    public void ConfigureAttendance(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasOne<Student>()
                      .WithMany()
                      .HasForeignKey(g => g.StudentId)
                      .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne<GroupSession>()
                  .WithMany()
                  .HasForeignKey(a => a.SessionId)
                  .OnDelete(DeleteBehavior.NoAction);
        });
    }
    public void ConfigureIndividualSession(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IndividualSession>(entity =>
        {
            entity.HasOne<Student>().WithMany().HasForeignKey(s => s.StudentId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne<User>().WithMany().HasForeignKey(s => s.TeacherId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne<Subject>().WithMany().HasForeignKey(s => s.SubjectId).OnDelete(DeleteBehavior.NoAction);
        });

    }
    public void ConfigureEnrollment(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasOne<Group>().WithMany().HasForeignKey(e => e.GroupId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne<Student>().WithMany().HasForeignKey(e => e.StudentId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne<PaymentPlan>().WithMany().HasForeignKey(e => e.PaymentPlanId).OnDelete(DeleteBehavior.NoAction);
        });
    }
    public void ConfigurePayment(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasOne<PaymentPlan>().WithMany().HasForeignKey(e => e.PaymentPlanId).OnDelete(DeleteBehavior.NoAction);
        });
    }
}


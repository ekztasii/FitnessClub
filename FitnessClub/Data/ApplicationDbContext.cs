using FitnessClub.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();
        public DbSet<WorkoutType> WorkoutTypes => Set<WorkoutType>();
        public DbSet<Workout> Workouts => Set<Workout>();
        public DbSet<WorkoutRegistration> WorkoutRegistrations => Set<WorkoutRegistration>();
        public DbSet<MembershipPlan> MembershipPlans => Set<MembershipPlan>();
        public DbSet<ClientMembership> ClientMemberships => Set<ClientMembership>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Roles ──────────────────────────────────────────────────────────
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.RoleName).IsRequired().HasMaxLength(100);
            });

            // ── Users ──────────────────────────────────────────────────────────
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.FullName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PhoneNumber).HasMaxLength(30);
                entity.Property(u => u.Email).HasMaxLength(100);
                entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);

                entity.HasOne(u => u.Role)
                      .WithMany(r => r.Users)
                      .HasForeignKey(u => u.RoleId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ── WorkoutType ────────────────────────────────────────────────────
            modelBuilder.Entity<WorkoutType>(entity =>
            {
                entity.HasKey(wt => wt.Id);
                entity.Property(wt => wt.TypeName).IsRequired().HasMaxLength(100);
                entity.Property(wt => wt.Description).HasMaxLength(500);

                // тренер (User) отвечает за тип тренировки
                entity.HasOne(wt => wt.User)
                      .WithMany(u => u.WorkoutTypes)
                      .HasForeignKey(wt => wt.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ── Workouts ───────────────────────────────────────────────────────
            modelBuilder.Entity<Workout>(entity =>
            {
                entity.HasKey(w => w.Id);
                entity.Property(w => w.WorkoutDate).IsRequired();
                entity.Property(w => w.MaxParticipants).IsRequired();

                entity.HasOne(w => w.WorkoutType)
                      .WithMany(wt => wt.Workouts)
                      .HasForeignKey(w => w.WorkoutTypeId)
                      .OnDelete(DeleteBehavior.Restrict);

                // тренер тренировки
                entity.HasOne(w => w.User)
                      .WithMany(u => u.Workouts)
                      .HasForeignKey(w => w.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ── WorkoutRegistrations ──────────────────────────────────────────
            modelBuilder.Entity<WorkoutRegistration>(entity =>
            {
                entity.HasKey(wr => wr.Id);
                entity.Property(wr => wr.RegistrationDate).IsRequired();
                entity.Property(wr => wr.AttendanceMarked).HasDefaultValue(false);

                entity.HasOne(wr => wr.Workout)
                      .WithMany(w => w.WorkoutRegistrations)
                      .HasForeignKey(wr => wr.WorkoutId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(wr => wr.User)
                      .WithMany(u => u.WorkoutRegistrations)
                      .HasForeignKey(wr => wr.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ── MembershipPlans ───────────────────────────────────────────────
            modelBuilder.Entity<MembershipPlan>(entity =>
            {
                entity.HasKey(mp => mp.Id);
                entity.Property(mp => mp.PlanName).IsRequired().HasMaxLength(100);
                entity.Property(mp => mp.Price).HasColumnType("decimal(10,2)").IsRequired();
                entity.Property(mp => mp.ValidityDays).IsRequired();
                entity.Property(mp => mp.AllowedVisits).IsRequired();
            });

            // ── ClientMembership ──────────────────────────────────────────────
            modelBuilder.Entity<ClientMembership>(entity =>
            {
                entity.HasKey(cm => cm.Id);
                entity.Property(cm => cm.PurchaseDate).IsRequired();
                entity.Property(cm => cm.ExpiryDate).IsRequired();
                entity.Property(cm => cm.VisitsUsed).HasDefaultValue(0);

                entity.HasOne(cm => cm.User)
                      .WithMany(u => u.ClientMemberships)
                      .HasForeignKey(cm => cm.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(cm => cm.MembershipPlan)
                      .WithMany(mp => mp.ClientMemberships)
                      .HasForeignKey(cm => cm.MembershipPlanId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ── Seed data ─────────────────────────────────────────────────────
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, RoleName = "Администратор" },
                new Role { Id = 2, RoleName = "Тренер" },
                new Role { Id = 3, RoleName = "Клиент" }
            );
        }
    }
}

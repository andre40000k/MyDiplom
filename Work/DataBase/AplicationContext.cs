using LoginComponent.Models;
using LoginComponent.Models.Transports;
using Microsoft.EntityFrameworkCore;

namespace LoginComponent.DataBase
{
    public class AplicationContext : DbContext
    {
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        //public DbSet<UserTask> Tasks => Set<UserTask>();
        public DbSet<UserPackeg> UserPackegs => Set<UserPackeg>();
        public DbSet<Transport> Transports => Set<Transport>(); 
        public DbSet<User> Users => Set<User>();
        public DbSet<Department> Departments => Set<Department>();  

        public AplicationContext(DbContextOptions<AplicationContext> options) 
            : base(options) 
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshToken>(entity =>
            {

                entity.Property(e => e.ExpiryTime).HasColumnType("smalldatetime");

                entity.Property(e => e.TokenHash)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.TokenSalt)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Ts)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("TS");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RefreshToken_User");

                entity.ToTable("RefreshToken");
            });

            //modelBuilder.Entity<UserTask>(entity =>
            //{

            //    entity.Property(e => e.Name)
            //        .IsRequired()
            //        .HasMaxLength(100);

            //    entity.Property(e => e.Ts)
            //        .HasColumnType("smalldatetime")
            //        .HasColumnName("TS");

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.Tasks)
            //        .HasForeignKey(d => d.UserId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Task_User");

            //    entity.ToTable("Task");
            //});

            modelBuilder.Entity<UserPackeg>(entity =>
            {
                entity.Property(e => e.CurrentLocation)
                      .IsRequired();

                entity.Property(e => e.StartLocation)
                      .IsRequired();

                entity.Property(e => e.FinalLocation)
                      .IsRequired();

                entity.Property(e => e.Weight)
                      .IsRequired();

                entity.HasOne(d => d.User)
                      .WithMany(p => p.UserPackegs)
                      .HasForeignKey(d => d.UserId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Packeg_User");

                entity.HasOne(d => d.Transport)
                      .WithMany(p => p.UserPackegs)
                      .HasForeignKey(d => d.TransportId)
                      .OnDelete(DeleteBehavior.ClientNoAction)
                      .HasConstraintName("FK_Packeg_Transport");

                entity.ToTable("Packeg");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                
                entity.Property(e => e.Number)
                      .IsRequired();

                entity.Property(e => e.Name)
                      .IsRequired();

                entity.Property(e => e.Address)
                      .IsRequired();

                entity.HasOne(d => d.Transport)
                      .WithMany(p => p.Departments)
                      .HasForeignKey(d => d.TransportId)
                      .OnDelete(DeleteBehavior.ClientNoAction)
                      .HasConstraintName("FK_Department_Transport");

                entity.ToTable("Department");
            });

            modelBuilder.Entity<Transport>(entity =>
            {
                entity.Property(e => e.Capacity)
                      .IsRequired();

                entity.Property(e => e.Capacity)
                      .IsRequired();

                entity.Property(e => e.TypeOfTransport)
                      .IsRequired();

                entity.ToTable("Transport");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Ts)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("TS");

                entity.ToTable("User");

            });
        }
    }
}

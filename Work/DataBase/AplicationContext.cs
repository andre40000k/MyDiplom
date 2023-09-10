using LoginComponent.Models;
using LoginComponent.Models.Department;
using LoginComponent.Models.ParcelModel;
//using LoginComponent.Models.BaseDepartment;
using LoginComponent.Models.Transports;
using Microsoft.EntityFrameworkCore;

namespace LoginComponent.DataBase
{
    public class AplicationContext : DbContext
    {
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<User> Users => Set<User>();
        //public DbSet<UserTask> Tasks => Set<UserTask>();


        public DbSet<Parcel> Parcels => Set<Parcel>();
        public DbSet<Location> Locations => Set<Location>();
        public DbSet<Transport> Transports => Set<Transport>(); 




        public DbSet<LocalDepartment> LocalDepartments => Set<LocalDepartment>();
        public DbSet<DistrictDepartment> DistrictDepartments => Set<DistrictDepartment>();
        public DbSet<RegionalDepartment> RegionalDepartments => Set<RegionalDepartment>();

        // dotnet ef database update 20230908170300_ChangeV1
        public AplicationContext(DbContextOptions<AplicationContext> options) 
            : base(options) 
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocalDepartment>(entity =>
            {

                entity.Property(e => e.Number)
                      .IsRequired();

                entity.Property(e => e.Address)
                      .IsRequired();

                entity.HasOne(d => d.DistrictDepartment)
                     .WithMany(p => p.LocalDepatments)
                     .HasForeignKey(d => d.DistrictId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_DistrictDepartment_LocalDepartment");

                entity.ToTable("LocalDepartment");
            });

            modelBuilder.Entity<DistrictDepartment>(entity =>
            {
                entity.Property(e => e.Number)
                      .IsRequired();

                entity.Property(e => e.Address)
                      .IsRequired();

                entity.HasOne(d => d.RegionalDepartment)
                      .WithMany(p => p.DistrictDepartments)
                      .HasForeignKey(d => d.RegionalID)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_DistrictDepartment_RegionalDepartment");

                entity.ToTable("DistrictDepartment");
            });

            modelBuilder.Entity<RegionalDepartment>(entity =>
            {
                entity.Property(e => e.Number)
                      .IsRequired();

                entity.Property(e => e.Address)
                      .IsRequired();

                entity.ToTable("RegionalDepartment");
            });

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

            modelBuilder.Entity<Parcel>(entity =>
            {
                entity.Property(e => e.StartLocation)
                      .IsRequired();

                entity.Property(e => e.FinalLocation)
                      .IsRequired();

                entity.Property(e => e.Weight)
                      .IsRequired();

                entity.HasOne(p => p.User)
                      .WithMany(u => u.SentParcels)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                //entity.HasOne(d => d.User)
                //      .WithMany(p => p.UserPackegs)
                //      .HasForeignKey(d => d.UserId)
                //      .OnDelete(DeleteBehavior.ClientSetNull)
                //      .HasConstraintName("FK_Packeg_User");

                //entity.HasOne(d => d.Transport)
                //      .WithMany(p => p.UserPackegs)
                //      .HasForeignKey(d => d.TransportId)
                //      .OnDelete(DeleteBehavior.ClientNoAction)
                //      .HasConstraintName("FK_Packeg_Transport");

                entity.ToTable("Parcel");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasOne(p => p.Parcel)
                .WithMany(l => l.CurrentLocation)
                .HasForeignKey(p => p.ParcelId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.ToTable("Location");
            });



            modelBuilder.Entity<Transport>(entity =>
            {
                entity.Property(e => e.Capacity)
                      .IsRequired();

                entity.Property(e => e.Name)
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

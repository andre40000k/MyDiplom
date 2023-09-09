using LoginComponent.Models.Department;
using Microsoft.EntityFrameworkCore;

namespace LoginComponent.DataBase
{
    public partial class AplicationContext
    {
        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<LocalDepartment>(entity =>
            //{

            //    entity.Property(e => e.Number)
            //          .IsRequired();

            //    entity.Property(e => e.Address)
            //          .IsRequired();

            //    entity.HasOne(d => d.DistrictDepartment)
            //         .WithMany(p => p.LocalDepatments)
            //         .HasForeignKey(d => d.DistrictId)
            //         .OnDelete(DeleteBehavior.ClientSetNull)
            //         .HasConstraintName("FK_DistrictDepartment_LocalDepartment");

            //    entity.ToTable("LocalDepartment");
            //});

            //modelBuilder.Entity<DistrictDepartment>(entity =>
            //{
            //    entity.Property(e => e.Number)
            //          .IsRequired();

            //    entity.Property(e => e.Address)
            //          .IsRequired();

            //    entity.HasOne(d => d.RegionalDepartment)
            //          .WithMany(p => p.DistrictDepartments)
            //          .HasForeignKey(d => d.RegionalID)
            //          .OnDelete(DeleteBehavior.ClientSetNull)
            //          .HasConstraintName("FK_DistrictDepartment_RegionalDepartment");

            //    entity.ToTable("DistrictDepartment");
            //});

            //modelBuilder.Entity<RegionalDepartment>(entity =>
            //{
            //    entity.Property(e => e.Number)
            //          .IsRequired();

            //    entity.Property(e => e.Address)
            //          .IsRequired();

            //    entity.ToTable("RegionalDepartment");
            //});
        }
    }
}

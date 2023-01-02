using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityFramework.Configurations
{
    public class WorkerRequestConfiguration : IEntityTypeConfiguration<WorkerRequest>
    {
        public void Configure(EntityTypeBuilder<WorkerRequest> builder)
        {
            builder.ToTable("WorkerRequests");

            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.AssignedWorkerId).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.DocumentNumber).IsRequired().HasColumnType("nvarchar").HasMaxLength(20);
            builder.Property(x => x.DocumentNumber).IsRequired(false).HasColumnType("nvarchar").HasMaxLength(2000);
            builder.Property(x => x.RequestType).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(p => p.StartFrom).HasColumnType("date").IsRequired();
            builder.Property(p => p.FinishDate).HasColumnType("date").IsRequired();
            builder.Property(p => p.IsActive).IsRequired().HasDefaultValue(true);
            builder.HasMany(p => p.UserRequests).WithOne(x => x.Request).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
        }
    }
}

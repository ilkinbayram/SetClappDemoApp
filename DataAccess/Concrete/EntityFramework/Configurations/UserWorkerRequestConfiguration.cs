using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityFramework.Configurations
{
    public class UserWorkerRequestConfiguration : IEntityTypeConfiguration<UserWorkerRequest>
    {
        public void Configure(EntityTypeBuilder<UserWorkerRequest> builder)
        {
            builder.ToTable("UserWorkerRequests");

            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.RequestId).IsRequired();
            builder.Property(p => p.IsRequestOwner).HasColumnType("bit").IsRequired().HasDefaultValue(false);
            builder.Property(p => p.IsActive).IsRequired().HasDefaultValue(true);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities.Concrete;


namespace DataAccess.Concrete.EntityFramework.Configurations
{
    public class UserOperationClaimConfiguration : IEntityTypeConfiguration<UserOperationClaim>
    {
        public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
        {
            builder.ToTable("UserOperationClaims");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(o => o.User).WithMany(m => m.UserOperationClaims);
            builder.HasOne(o => o.OperationClaim).WithMany(m => m.UserOperationClaims);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestProject.Domain.Entities;

namespace TestProject.Infrastructure.Database.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(e => e.UserId)
                .IsRequired();

            builder.Property(e => e.CreditScore)
                .IsRequired();

            builder
                .HasOne(e => e.User)
                .WithOne(e => e.Account)
                .HasForeignKey<Account>(e => e.UserId);
        }
    }
}

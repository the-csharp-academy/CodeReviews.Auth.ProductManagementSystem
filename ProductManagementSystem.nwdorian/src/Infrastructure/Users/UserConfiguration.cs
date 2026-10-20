using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(150).IsRequired(false);
        builder.Property(u => u.LastName).HasMaxLength(150).IsRequired(false);

        builder.HasIndex(g => g.IsDeleted);
        builder.HasQueryFilter(GlobalFilters.SoftDelete, g => !g.IsDeleted);
        builder.Property(g => g.IsDeleted).HasDefaultValue(false).IsRequired();
        builder.Property(g => g.DeletedOnUtc).IsRequired(false);
        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(g => g.DeletedBy)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(g => g.CreatedOnUtc).IsRequired();
        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(g => g.CreatedBy)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(g => g.UpdatedOnUtc).IsRequired(false);
        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(g => g.UpdatedBy)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

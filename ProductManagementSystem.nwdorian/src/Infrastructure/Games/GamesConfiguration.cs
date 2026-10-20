using Domain.Games;
using Infrastructure.Database;
using Infrastructure.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Games;

public class GamesConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name).HasMaxLength(150).IsRequired();
        builder.HasIndex(g => g.Name).IsUnique();

        builder.Property(g => g.Genre).HasMaxLength(50).IsRequired();
        builder.Property(g => g.Price).HasPrecision(18, 2).IsRequired();
        builder.Property(g => g.ReleaseDate).IsRequired();

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

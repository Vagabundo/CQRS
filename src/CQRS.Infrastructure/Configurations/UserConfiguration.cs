using CQRS.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(user => user.Id);

        builder.Property(user => user.Name)
            .HasMaxLength(200)
            .HasConversion(name => name!.Value, value => new Name(value));

        builder.Property(user => user.FamilyName)
            .HasMaxLength(200)
            .HasConversion(familyName => familyName!.Value, value => new FamilyName(value));

        builder.Property(user => user.Email)
            .HasMaxLength(400)
            .HasConversion(email => email!.Value, value => new Domain.Users.Email(value));

        builder.HasIndex(user => user.Email)
            .IsUnique();

    }
}
using Domain.EFModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.Ignore(e => e.DomainEvents);

            builder.Property(t => t.Id)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
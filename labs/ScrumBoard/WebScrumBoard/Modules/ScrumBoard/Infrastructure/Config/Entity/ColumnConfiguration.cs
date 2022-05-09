using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebScrumBoard.Modules.ScrumBoard.Infrastructure.Entity;

namespace WebScrumBoard.Modules.ScrumBoard.Infrastructure.Config.Entity;

public class ColumnConfiguration : IEntityTypeConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> builder)
    {
        builder
            .ToTable("column")
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Title)
            .IsRequired()
            .HasColumnType("VARCHAR(255)");
    }
}

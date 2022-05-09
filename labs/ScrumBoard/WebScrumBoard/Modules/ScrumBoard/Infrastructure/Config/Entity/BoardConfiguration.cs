using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebScrumBoard.Modules.ScrumBoard.Infrastructure.Entity;

namespace WebScrumBoard.Modules.ScrumBoard.Infrastructure.Config.Entity;

public class BoardConfiguration : IEntityTypeConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> builder)
    {
        builder
            .ToTable("board")
            .HasKey(b => b.Id);

        builder
            .Property(b => b.Title)
            .IsRequired()
            .HasColumnType("VARCHAR(255)");
    }
}

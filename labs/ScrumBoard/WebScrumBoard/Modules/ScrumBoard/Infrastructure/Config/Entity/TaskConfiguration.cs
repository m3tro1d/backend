using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = WebScrumBoard.Modules.ScrumBoard.Infrastructure.Entity.Task;

namespace WebScrumBoard.Modules.ScrumBoard.Infrastructure.Config.Entity;

public class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder
            .ToTable("task")
            .HasKey(t => t.Id);

        builder
            .Property(t => t.Title)
            .IsRequired()
            .HasColumnType("VARCHAR(255)");

        builder
            .Property(t => t.Description)
            .IsRequired()
            .HasColumnType("TEXT");

        builder
            .Property(t => t.Priority)
            .IsRequired();
    }
}

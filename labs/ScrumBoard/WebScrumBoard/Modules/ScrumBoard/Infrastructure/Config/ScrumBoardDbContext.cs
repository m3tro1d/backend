using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebScrumBoard.Modules.ScrumBoard.Infrastructure.Entity;
using Task = WebScrumBoard.Modules.ScrumBoard.Infrastructure.Entity.Task;

namespace WebScrumBoard.Modules.ScrumBoard.Infrastructure.Config;

public class ScrumBoardDbContext : DbContext
{
    public DbSet<Board> Boards { get; set; }
    public DbSet<Column> Columns { get; set; }
    public DbSet<Task> Tasks { get; set; }

    public ScrumBoardDbContext(DbContextOptions<ScrumBoardDbContext> options)
        : base(options)
    {
    }

    protected virtual void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

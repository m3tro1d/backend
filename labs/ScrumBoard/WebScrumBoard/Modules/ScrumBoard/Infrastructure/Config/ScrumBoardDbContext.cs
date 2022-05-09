using Microsoft.EntityFrameworkCore;
using WebScrumBoard.Modules.ScrumBoard.Infrastructure.Entity;

namespace WebScrumBoard.Modules.ScrumBoard.Infrastructure.Config;

public class ScrumBoardDbContext : DbContext
{
    public DbSet<Board> Boards { get; set; }

    public ScrumBoardDbContext(DbContextOptions<ScrumBoardDbContext> options)
        : base(options)
    {
    }
}

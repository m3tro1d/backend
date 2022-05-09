using System.ComponentModel.DataAnnotations;

namespace WebScrumBoard.Modules.ScrumBoard.Infrastructure.Entity;

public class Task
{
    [Key]
    public Guid Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
}

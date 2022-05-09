using System.ComponentModel.DataAnnotations;

namespace WebScrumBoard.Modules.ScrumBoard.Infrastructure.Entity;

public class Column
{
    [Key]
    public Guid Id { get; set; }

    public string Title { get; set; }
    public List<Task> Tasks { get; set; } = new();
}

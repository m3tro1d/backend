using System.ComponentModel.DataAnnotations;

namespace WebScrumBoard.Modules.ScrumBoard.Infrastructure.Entity;

public class Board
{
    [Key]
    public Guid Id { get; set; }

    public string Title { get; set; }
    public virtual ICollection<Column> Columns { get; set; }
}

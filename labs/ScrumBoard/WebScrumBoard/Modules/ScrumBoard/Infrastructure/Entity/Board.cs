using System.ComponentModel.DataAnnotations;

namespace WebScrumBoard.Modules.ScrumBoard.Infrastructure.Entity;

public class Board
{
    [Key]
    public Guid Id { get; set; }

    public string Title { get; set; }
    public List<Column> Columns { get; set; }
}

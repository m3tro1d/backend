namespace WebScrumBoard.Controllers.Request;

public class CreateTaskRequest
{
    public string? ColumnId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
}

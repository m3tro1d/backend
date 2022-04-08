namespace WebScrumBoard.Controllers.Request;

public class ChangeTaskRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? Priority { get; set; }
}

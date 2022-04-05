namespace WebScrumBoard.Modules.ScrumBoard.App.Query.Data;

public class TaskData
{
    public string Title { get; }
    public string Description { get; }
    public string Priority { get; }

    public TaskData(string title, string description, string priority)
    {
        Title = title;
        Description = description;
        Priority = priority;
    }
}

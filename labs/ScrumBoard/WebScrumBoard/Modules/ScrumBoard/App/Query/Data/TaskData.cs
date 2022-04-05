namespace WebScrumBoard.Modules.ScrumBoard.App.Query.Data;

public class TaskData
{
    public string Id { get; }
    public string Title { get; }
    public string Description { get; }
    public string Priority { get; }

    public TaskData(string id, string title, string description, string priority)
    {
        Id = id;
        Title = title;
        Description = description;
        Priority = priority;
    }
}

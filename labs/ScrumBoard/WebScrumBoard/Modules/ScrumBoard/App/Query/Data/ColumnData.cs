namespace WebScrumBoard.Modules.ScrumBoard.App.Query.Data;

public class ColumnData
{
    public string Id { get; }
    public string Title { get; }
    public IEnumerable<TaskData> Tasks { get; }

    public ColumnData(string id, string title, IEnumerable<TaskData> tasks)
    {
        Id = id;
        Title = title;
        Tasks = tasks;
    }
}

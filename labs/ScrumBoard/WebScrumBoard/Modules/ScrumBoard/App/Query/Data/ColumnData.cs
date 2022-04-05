namespace WebScrumBoard.Modules.ScrumBoard.App.Query.Data;

public class ColumnData
{
    public string Title { get; }
    public IEnumerable<TaskData> Tasks { get; }

    public ColumnData(string title, IEnumerable<TaskData> tasks)
    {
        Title = title;
        Tasks = tasks;
    }
}

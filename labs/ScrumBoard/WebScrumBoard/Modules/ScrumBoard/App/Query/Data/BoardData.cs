namespace WebScrumBoard.Modules.ScrumBoard.App.Query.Data;

public class BoardData
{
    public string Id { get; }
    public string Title { get; }
    public IEnumerable<ColumnData> Columns { get; }

    public BoardData(string id, string title, IEnumerable<ColumnData> columns)
    {
        Id = id;
        Title = title;
        Columns = columns;
    }
}

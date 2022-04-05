namespace WebScrumBoard.Modules.ScrumBoard.App.Query.Data;

public class BoardData
{
    public string Title { get; }
    public IEnumerable<ColumnData> Columns { get; }

    public BoardData(string title, IEnumerable<ColumnData> columns)
    {
        Title = title;
        Columns = columns;
    }
}

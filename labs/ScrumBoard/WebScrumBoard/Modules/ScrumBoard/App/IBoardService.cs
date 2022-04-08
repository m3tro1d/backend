namespace WebScrumBoard.Modules.ScrumBoard.App;

public interface IBoardService
{
    public Guid CreateBoard(string title);
    public void RemoveBoard(Guid boardId);

    public Guid CreateColumn(Guid boardId, string title);
    public void ChangeColumnTitle(Guid columnId, string title);
}

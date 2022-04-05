using ScrumBoard.Model;

namespace WebScrumBoard.Modules.ScrumBoard.App;

public interface IBoardStore
{
    public void Store(IBoard board);
    public void Remove(Guid boardId);
}

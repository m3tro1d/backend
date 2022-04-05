using WebScrumBoard.Modules.ScrumBoard.App.Query.Data;

namespace WebScrumBoard.Modules.ScrumBoard.App.Query;

public interface IBoardQueryService
{
    public IEnumerable<BoardData> ListBoards();
}

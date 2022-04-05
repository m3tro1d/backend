using WebScrumBoard.Modules.ScrumBoard.App.Query.Data;

namespace WebScrumBoard.Controllers.Response;

public class ListBoardsResponse
{
    public IEnumerable<BoardData> Boards { get; }

    public ListBoardsResponse(IEnumerable<BoardData> boards)
    {
        Boards = boards;
    }
}

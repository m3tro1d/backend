using WebScrumBoard.Modules.ScrumBoard.App.Query.Data;

namespace WebScrumBoard.Controllers.Response;

public class GetBoardsResponse
{
    public IEnumerable<BoardData> Boards { get; }

    public GetBoardsResponse(IEnumerable<BoardData> boards)
    {
        Boards = boards;
    }
}

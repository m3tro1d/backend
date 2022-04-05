using ScrumBoard.Factory;
using ScrumBoard.Model;

namespace WebScrumBoard.Modules.ScrumBoard.App;

public class BoardService : IBoardService
{
    private IBoardStore _boardStore;

    public BoardService(IBoardStore boardStore)
    {
        _boardStore = boardStore;
    }

    public Guid CreateBoard(string title)
    {
        IBoard board = ScrumBoardFactory.CreateBoard(title);
        _boardStore.Store(board);

        return board.Id;
    }

    public void RemoveBoard(Guid boardId)
    {
        _boardStore.Remove(boardId);
    }
}

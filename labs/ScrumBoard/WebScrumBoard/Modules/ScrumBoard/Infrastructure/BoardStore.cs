using Microsoft.Extensions.Caching.Memory;
using ScrumBoard.Model;
using WebScrumBoard.Modules.ScrumBoard.App;
using WebScrumBoard.Modules.ScrumBoard.App.Exception;

namespace WebScrumBoard.Modules.ScrumBoard.Infrastructure;

public class BoardStore : IBoardStore
{
    private const string MEMORY_CACHE_KEY = "boards";

    private IMemoryCache _memoryCache;

    public BoardStore(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public void Store(IBoard board)
    {
        List<IBoard> boards = GetBoards();

        boards.RemoveAll(existingBoard => existingBoard.Id == board.Id);
        boards.Add(board);

        _memoryCache.Set(MEMORY_CACHE_KEY, boards);
    }

    public IBoard FindOne(Guid boardId)
    {
        List<IBoard> boards = GetBoards();

        IBoard? board = boards.Find(b => b.Id == boardId);
        if (board == null)
        {
            throw new BoardNotFoundException();
        }

        return board;
    }

    public void Remove(Guid boardId)
    {
        List<IBoard> boards = GetBoards();

        boards.RemoveAll(board => board.Id == boardId);

        _memoryCache.Set(MEMORY_CACHE_KEY, boards);
    }

    private List<IBoard> GetBoards()
    {
        List<IBoard> boards;
        _memoryCache.TryGetValue(MEMORY_CACHE_KEY, out boards);
        if (boards == null)
        {
            boards = new();
        }

        return boards;
    }
}

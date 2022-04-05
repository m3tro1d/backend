using Microsoft.Extensions.Caching.Memory;
using ScrumBoard.Model;
using WebScrumBoard.Modules.ScrumBoard.App;

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
        List<IBoard> boards = new();
        _memoryCache.TryGetValue(MEMORY_CACHE_KEY, out boards);
        if (boards == null)
        {
            boards = new();
        }

        boards.Add(board);

        _memoryCache.Set(MEMORY_CACHE_KEY, boards);
    }

    public void Remove(Guid boardId)
    {
        List<IBoard> boards = new();
        _memoryCache.TryGetValue(MEMORY_CACHE_KEY, out boards);
        if (boards == null)
        {
            boards = new();
        }

        boards.RemoveAll(board => board.Id == boardId);

        _memoryCache.Set(MEMORY_CACHE_KEY, boards);
    }
}

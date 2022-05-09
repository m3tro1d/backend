using Microsoft.Extensions.Caching.Memory;
using ScrumBoard.Model;
using WebScrumBoard.Modules.ScrumBoard.App;
using WebScrumBoard.Modules.ScrumBoard.App.Exception;
using WebScrumBoard.Modules.ScrumBoard.Infrastructure.Config;

namespace WebScrumBoard.Modules.ScrumBoard.Infrastructure;

public class BoardStore : IBoardStore
{
    private const string MEMORY_CACHE_KEY = "boards";

    private IMemoryCache _memoryCache;
    private readonly ScrumBoardDbContext _context;

    public BoardStore(IMemoryCache memoryCache, ScrumBoardDbContext context)
    {
        _memoryCache = memoryCache;
        _context = context;
    }

    public void Store(IBoard board)
    {
        Entity.Board? boardEntity = _context.Boards.Find(board.Id);
        if (boardEntity is null)
        {
            boardEntity = new();

            boardEntity.Id = board.Id;
            boardEntity.Title = board.Title;

            List<Entity.Column> columns = new();
            boardEntity.Columns = columns;

            _context.Boards.Add(boardEntity);
        }
        else
        {
            List<Entity.Column> columns = new();
            boardEntity.Columns = columns;

            _context.Boards.Update(boardEntity);
        }

        _context.SaveChanges();
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

    public IBoard FindOneByColumnId(Guid columnId)
    {
        List<IBoard> boards = GetBoards();

        IBoard? board = boards.Find(b => b.FindAllColumns().Any(c => c.Id == columnId));
        if (board == null)
        {
            throw new ColumnNotFoundException();
        }

        return board;
    }

    public IBoard FindOneByTaskId(Guid taskId)
    {
        List<IBoard> boards = GetBoards();

        IBoard? board = boards.Find(b => b.FindAllColumns().Any(c => c.FindAllTasks().Any(t => t.Id == taskId)));
        if (board == null)
        {
            throw new TaskNotFoundException();
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

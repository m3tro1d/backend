using Microsoft.Extensions.Caching.Memory;
using ScrumBoard.Model;
using WebScrumBoard.Modules.ScrumBoard.App.Query;
using WebScrumBoard.Modules.ScrumBoard.App.Query.Data;

namespace WebScrumBoard.Modules.ScrumBoard.Infrastructure;

public class BoardQueryService : IBoardQueryService
{
    private const string MEMORY_CACHE_KEY = "boards";

    private IMemoryCache _memoryCache;

    public BoardQueryService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public IEnumerable<BoardData> ListBoards()
    {
        List<IBoard> boards = new();
        _memoryCache.TryGetValue(MEMORY_CACHE_KEY, out boards);
        if (boards == null)
        {
            boards = new();
        }

        return HydrateBoards(boards);
    }

    private IEnumerable<BoardData> HydrateBoards(IEnumerable<IBoard> boards)
    {
        List<BoardData> result = new();
        foreach (IBoard board in boards)
        {
            List<ColumnData> columns = new();
            foreach (IColumn column in board.FindAllColumns())
            {
                List<TaskData> tasks = new();
                foreach (ITask task in column.FindAllTasks())
                {
                    tasks.Add(new TaskData(task.Id.ToString(), task.Title, task.Description, task.Priority.ToString()));
                }

                columns.Add(new ColumnData(column.Id.ToString(), column.Title, tasks));
            }

            result.Add(new BoardData(board.Id.ToString(), board.Title, columns));
        }

        return result;
    }
}

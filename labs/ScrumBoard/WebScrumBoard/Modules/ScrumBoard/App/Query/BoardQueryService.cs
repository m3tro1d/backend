using Microsoft.Extensions.Caching.Memory;
using ScrumBoard.Model;
using WebScrumBoard.Modules.ScrumBoard.App.Query.Data;

namespace WebScrumBoard.Modules.ScrumBoard.App.Query;

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

        List<BoardData> result = new();
        foreach (IBoard board in boards)
        {
            List<ColumnData> columns = new();
            foreach (IColumn column in board.FindAllColumns())
            {
                List<TaskData> tasks = new();
                foreach (ITask task in tasks)
                {
                    tasks.Add(new TaskData(task.Title, task.Description, PriorityToString(task.Priority)));
                }

                columns.Add(new ColumnData(column.Title, tasks));
            }

            result.Add(new BoardData(board.Title, columns));
        }

        return result;
    }

    private string PriorityToString(TaskPriority priority)
    {
        switch (priority)
        {
            case TaskPriority.HIGH:
                return "HIGH";
            case TaskPriority.MEDIUM:
                return "MEDIUM";
            case TaskPriority.LOW:
                return "LOW";
            case TaskPriority.NONE:
                return "NONE";
            default:
                return "";
        }
    }
}

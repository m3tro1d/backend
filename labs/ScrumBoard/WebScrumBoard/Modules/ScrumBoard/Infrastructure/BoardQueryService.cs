using Microsoft.EntityFrameworkCore;
using WebScrumBoard.Modules.ScrumBoard.App.Query;
using WebScrumBoard.Modules.ScrumBoard.App.Query.Data;
using WebScrumBoard.Modules.ScrumBoard.Infrastructure.Config;
using WebScrumBoard.Modules.ScrumBoard.Infrastructure.Entity;
using Task = WebScrumBoard.Modules.ScrumBoard.Infrastructure.Entity.Task;

namespace WebScrumBoard.Modules.ScrumBoard.Infrastructure;

public class BoardQueryService : IBoardQueryService
{
    private readonly ScrumBoardDbContext _context;

    public BoardQueryService(ScrumBoardDbContext context)
    {
        _context = context;
    }

    public IEnumerable<BoardData> ListBoards()
    {
        List<Board> boards = _context.Boards
            .Include(b => b.Columns)
            .ThenInclude(c => c.Tasks)
            .ToList();

        return HydrateBoards(boards);
    }

    private IEnumerable<BoardData> HydrateBoards(List<Board> boards)
    {
        List<BoardData> result = new();
        foreach (Board board in boards)
        {
            List<ColumnData> columns = new();
            foreach (Column column in board.Columns)
            {
                List<TaskData> tasks = new();
                foreach (Task task in column.Tasks)
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

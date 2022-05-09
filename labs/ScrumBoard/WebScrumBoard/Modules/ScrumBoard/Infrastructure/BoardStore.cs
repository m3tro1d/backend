using Microsoft.EntityFrameworkCore;
using ScrumBoard.Factory;
using ScrumBoard.Model;
using WebScrumBoard.Modules.ScrumBoard.App;
using WebScrumBoard.Modules.ScrumBoard.App.Exception;
using WebScrumBoard.Modules.ScrumBoard.Infrastructure.Config;

namespace WebScrumBoard.Modules.ScrumBoard.Infrastructure;

public class BoardStore : IBoardStore
{
    private readonly ScrumBoardDbContext _context;

    public BoardStore(ScrumBoardDbContext context)
    {
        _context = context;
    }

    public void Store(IBoard board)
    {
        Entity.Board? boardEntity = _context.Boards.Include(b => b.Columns)
            .ThenInclude(c => c.Tasks)
            .FirstOrDefault(b => b.Id == board.Id);

        if (boardEntity is null)
        {
            boardEntity = new();

            boardEntity.Id = board.Id;
            boardEntity.Title = board.Title;
            boardEntity.Columns = ConvertColumnsToEntities(board.FindAllColumns());

            _context.Boards.Add(boardEntity);
        }
        else
        {
            boardEntity.Columns = ConvertColumnsToEntities(board.FindAllColumns());

            _context.Boards.Update(boardEntity);
        }

        _context.SaveChanges();
    }

    public IBoard FindOne(Guid boardId)
    {
        Entity.Board? board = _context.Boards.Include(b => b.Columns)
            .ThenInclude(c => c.Tasks)
            .FirstOrDefault(b => b.Id == boardId);

        if (board is null)
        {
            throw new BoardNotFoundException();
        }

        return ConvertEntityToBoard(board);
    }

    public IBoard FindOneByColumnId(Guid columnId)
    {
        Entity.Board? board = _context.Boards.Include(b => b.Columns)
            .ThenInclude(c => c.Tasks)
            .FirstOrDefault(b => b.Columns.Any(c => c.Id == columnId));

        if (board is null)
        {
            throw new ColumnNotFoundException();
        }

        return ConvertEntityToBoard(board);
    }

    public IBoard FindOneByTaskId(Guid taskId)
    {
        Entity.Board? board = _context.Boards.Include(b => b.Columns)
            .ThenInclude(c => c.Tasks)
            .FirstOrDefault(b => b.Columns.Any(c => c.Tasks.Any(t => t.Id == taskId)));

        if (board is null)
        {
            throw new TaskNotFoundException();
        }

        return ConvertEntityToBoard(board);
    }

    public void Remove(Guid boardId)
    {
        Entity.Board? board = _context.Boards.Find(boardId);
        if (board is null)
        {
            return;
        }

        _context.Boards.Remove(board);
        _context.SaveChanges();
    }

    private List<Entity.Column> ConvertColumnsToEntities(IReadOnlyCollection<IColumn> columns)
    {
        List<Entity.Column> result = new();
        foreach (IColumn column in columns)
        {
            List<Entity.Task> tasks = new();
            foreach (ITask task in column.FindAllTasks())
            {
                Entity.Task taskEntity = new();
                taskEntity.Id = task.Id;
                taskEntity.Title = task.Title;
                taskEntity.Description = task.Description;
                taskEntity.Priority = (int)task.Priority;

                tasks.Add(taskEntity);
            }

            Entity.Column columnEntity = new();
            columnEntity.Id = column.Id;
            columnEntity.Title = column.Title;
            columnEntity.Tasks = tasks;

            result.Add(columnEntity);
        }

        return result;
    }

    private IBoard ConvertEntityToBoard(Entity.Board board)
    {
        List<IColumn> columns = new();
        foreach (Entity.Column column in board.Columns)
        {
            List<ITask> tasks = new();
            foreach (Entity.Task task in column.Tasks)
            {
                tasks.Add(ScrumBoardFactory.LoadTask(task.Id, task.Title, task.Description, (TaskPriority)task.Priority));
            }

            columns.Add(ScrumBoardFactory.LoadColumn(column.Id, column.Title, tasks));
        }

        return ScrumBoardFactory.LoadBoard(board.Id, board.Title, columns);
    }
}

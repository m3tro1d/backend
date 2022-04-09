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
        IBoard board = _boardStore.FindOne(boardId);
        _boardStore.Remove(board.Id);
    }

    public Guid CreateColumn(Guid boardId, string title)
    {
        IBoard board = _boardStore.FindOne(boardId);
        IColumn column = ScrumBoardFactory.CreateColumn(title);
        board.AddColumn(column);
        _boardStore.Store(board);

        return column.Id;
    }

    public void ChangeColumnTitle(Guid columnId, string title)
    {
        IBoard board = _boardStore.FindOneByColumnId(columnId);
        board.ChangeColumnTitle(columnId, title);
        _boardStore.Store(board);
    }

    public Guid CreateTask(Guid columnId, string title, string description, TaskPriority priority)
    {
        IBoard board = _boardStore.FindOneByColumnId(columnId);
        ITask task = ScrumBoardFactory.CreateTask(title, description, priority);
        board.AddTaskToColumn(task, columnId);
        _boardStore.Store(board);

        return task.Id;
    }

    public void ChangeTask(Guid taskId, string? title, string? description, TaskPriority? priority)
    {
        IBoard board = _boardStore.FindOneByTaskId(taskId);

        if (title is not null)
        {
            board.ChangeTaskTitle(taskId, title);
        }
        if (description is not null)
        {
            board.ChangeTaskDescription(taskId, description);
        }
        if (priority is not null)
        {
            board.ChangeTaskPriority(taskId, (TaskPriority)priority);
        }

        _boardStore.Store(board);
    }

    public void AdvanceTask(Guid taskId)
    {
        IBoard board = _boardStore.FindOneByTaskId(taskId);
        board.AdvanceTask(taskId);
        _boardStore.Store(board);
    }

    public void RemoveTask(Guid taskId)
    {
        IBoard board = _boardStore.FindOneByTaskId(taskId);
        board.RemoveTask(taskId);
        _boardStore.Store(board);
    }
}

using ScrumBoard.Model;

namespace ScrumBoard.Factory;

public class ScrumBoardFactory
{
    public static IBoard CreateBoard(string title)
    {
        return new Board(Guid.NewGuid(), title);
    }

    public static IBoard LoadBoard(Guid id, string title, List<IColumn> columns)
    {
        return new Board(id, title, columns);
    }

    public static IColumn CreateColumn(string title)
    {
        return new Column(Guid.NewGuid(), title);
    }

    public static IColumn LoadColumn(Guid id, string title, List<ITask> tasks)
    {
        return new Column(id, title, tasks);
    }

    public static ITask CreateTask(string title, string description, TaskPriority priority)
    {
        return new Model.Task(Guid.NewGuid(), title, description, priority);
    }

    public static ITask LoadTask(Guid id, string title, string description, TaskPriority priority)
    {
        return new Model.Task(id, title, description, priority);
    }
}

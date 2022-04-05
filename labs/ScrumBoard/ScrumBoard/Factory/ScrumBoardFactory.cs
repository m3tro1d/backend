using ScrumBoard.Model;

namespace ScrumBoard.Factory;

public class ScrumBoardFactory
{
    public static IBoard CreateBoard(string title)
    {
        return new Board(Guid.NewGuid(), title);
    }

    public static IColumn CreateColumn(string title)
    {
        return new Column(Guid.NewGuid(), title);
    }

    public static ITask CreateTask(string title, string description, TaskPriority priority)
    {
        return new Model.Task(Guid.NewGuid(), title, description, priority);
    }
}

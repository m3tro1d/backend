using ScrumBoard.Model;

namespace WebScrumBoard.Modules.ScrumBoard.App;

public interface IBoardService
{
    public Guid CreateBoard(string title);
    public void RemoveBoard(Guid boardId);

    public Guid CreateColumn(Guid boardId, string title);
    public void ChangeColumnTitle(Guid columnId, string title);

    public Guid CreateTask(Guid columnId, string title, string description, TaskPriority priority);
    public void ChangeTask(Guid taskId, string? title, string? description, TaskPriority? priority);
    public void AdvanceTask(Guid taskId);
    public void RemoveTask(Guid taskId);
}

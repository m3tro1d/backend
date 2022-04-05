namespace ScrumBoard.Model;

public interface IBoard
{
    public Guid Id { get; }
    public string Title { get; }

    public void AddColumn(IColumn column);
    public void ChangeColumnTitle(Guid columnId, string newTitle);
    public IReadOnlyCollection<IColumn> FindAllColumns();
    public IColumn? FindColumnById(Guid columnId);

    public void AddTaskToColumn(ITask task, Guid? columnId = null);
    public void ChangeTaskTitle(Guid taskId, string newTitle);
    public void ChangeTaskDescription(Guid taskId, string newDescription);
    public void ChangeTaskPriority(Guid taskId, TaskPriority newPriority);
    public void AdvanceTask(Guid taskId);
    public void RemoveTask(Guid taskId);
}

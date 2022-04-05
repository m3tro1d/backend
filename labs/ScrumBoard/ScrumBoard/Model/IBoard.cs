namespace ScrumBoard.Model;

public interface IBoard
{
    public string Title { get; }

    public void AddColumn(IColumn column);
    public void ChangeColumnTitle(string columnTitle, string newTitle);
    public IReadOnlyCollection<IColumn> FindAllColumns();
    public IColumn? FindColumnByTitle(string title);

    public void AddTaskToColumn(ITask task, string? columnTitle = null);
    public void ChangeTaskTitle(string taskTitle, string newTitle);
    public void ChangeTaskDescription(string taskTitle, string newDescription);
    public void ChangeTaskPriority(string taskTitle, TaskPriority newPriority);
    public void AdvanceTask(string taskTitle);
    public void RemoveTask(string taskTitle);
}

namespace ScrumBoard.Model;

public interface IColumn : IIdentifiable
{
    public string Title { get; set; }

    public void AddTask(ITask task);
    public IReadOnlyCollection<ITask> FindAllTasks();
    public ITask? FindTaskById(Guid taskId);
    public void RemoveTaskById(Guid taskId);
}

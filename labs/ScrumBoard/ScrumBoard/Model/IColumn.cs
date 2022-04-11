namespace ScrumBoard.Model;

public interface IColumn : IIdentifiable, IRenameable
{
    public void AddTask(ITask task);
    public IReadOnlyCollection<ITask> FindAllTasks();
    public ITask? FindTaskById(Guid taskId);
    public void RemoveTaskById(Guid taskId);
}

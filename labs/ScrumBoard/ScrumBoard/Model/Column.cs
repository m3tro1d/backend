using ScrumBoard.Exception;

namespace ScrumBoard.Model;

internal class Column : IColumn
{
    private List<ITask> _tasks;

    public Guid Id { get; }
    public string Title { get; set; }

    public Column(Guid id, string title)
    {
        Id = id;
        Title = title;
        _tasks = new();
    }

    public Column(Guid id, string title, List<ITask> tasks)
    {
        Id = id;
        Title = title;
        _tasks = tasks;
    }

    public void AddTask(ITask task)
    {
        if (FindTaskById(task.Id) != null)
        {
            throw new TaskAlreadyExistsException();
        }

        _tasks.Add(task);
    }

    public IReadOnlyCollection<ITask> FindAllTasks()
    {
        return _tasks;
    }

    public ITask? FindTaskById(Guid taskId)
    {
        return _tasks.Find(task => task.Id == taskId);
    }

    public void RemoveTaskById(Guid taskId)
    {
        _tasks.RemoveAll(task => task.Id == taskId);
    }
}

namespace ScrumBoard.Model;

internal class Task : ITask
{
    public Guid Id { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskPriority Priority { get; set; }

    public Task(Guid id, string title, string description, TaskPriority priority)
    {
        Id = id;
        Title = title;
        Description = description;
        Priority = priority;
    }
}

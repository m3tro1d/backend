namespace ScrumBoard.Model;

public enum TaskPriority
{
    HIGH,
    MEDIUM,
    LOW,
    NONE,
}

public interface ITask
{
    public Guid Id { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskPriority Priority { get; set; }
}

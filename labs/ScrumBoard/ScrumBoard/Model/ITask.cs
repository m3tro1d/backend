namespace ScrumBoard.Model;

public interface ITask : IIdentifiable
{
    public Guid Id { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskPriority Priority { get; set; }
}

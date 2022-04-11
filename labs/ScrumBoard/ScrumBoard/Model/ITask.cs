namespace ScrumBoard.Model;

public interface ITask : IIdentifiable, IRenameable
{
    public string Description { get; set; }
    public TaskPriority Priority { get; set; }
}

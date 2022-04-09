using ScrumBoard.Model;

namespace WebScrumBoard.Modules.ScrumBoard.App;

public interface IBoardStore
{
    public void Store(IBoard board);

    public IBoard FindOne(Guid boardId);
    public IBoard FindOneByColumnId(Guid columnId);
    public IBoard FindOneByTaskId(Guid taskId);

    public void Remove(Guid boardId);
}

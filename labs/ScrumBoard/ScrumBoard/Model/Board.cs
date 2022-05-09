using ScrumBoard.Exception;

namespace ScrumBoard.Model;

internal class Board : IBoard
{
    private const int MAX_COLUMNS = 10;
    private List<IColumn> _columns;

    public Guid Id { get; }
    public string Title { get; }

    public Board(Guid id, string title)
    {
        Id = id;
        Title = title;
        _columns = new();
    }

    public Board(Guid id, string title, List<IColumn> columns)
    {
        Id = id;
        Title = title;
        _columns = columns;
    }

    public void AddColumn(IColumn column)
    {
        if (_columns.Count == MAX_COLUMNS)
        {
            throw new BoardColumnCountExceededException();
        }

        if (FindColumnById(column.Id) != null)
        {
            throw new ColumnAlreadyExistsException();
        }

        _columns.Add(column);
    }

    public IReadOnlyCollection<IColumn> FindAllColumns()
    {
        return _columns;
    }

    public IColumn? FindColumnById(Guid columnId)
    {
        return _columns.Find(column => column.Id == columnId);
    }

    public void AdvanceTask(Guid taskId)
    {
        if (_columns.Count == 0)
        {
            throw new NoColumnsException();
        }

        ITask? task = null;
        int nextColumnIndex = 0;

        for (int i = 0; i < _columns.Count; ++i)
        {
            task = _columns[i].FindTaskById(taskId);
            if (task != null)
            {
                if (i == _columns.Count - 1)
                {
                    throw new FinalColumnReachedException();
                }

                _columns[i].RemoveTaskById(taskId);
                nextColumnIndex = i + 1;
                break;
            }
        }

        if (nextColumnIndex != 0 && task != null)
        {
            _columns[nextColumnIndex].AddTask(task);
            return;
        }

        throw new TaskNotFoundException();

    }

    public void AddTaskToColumn(ITask task, Guid? columnId = null)
    {
        if (_columns.Count == 0)
        {
            throw new NoColumnsException();
        }

        if (columnId == null)
        {
            _columns[0].AddTask(task);
            return;
        }

        IColumn? column = FindColumnById(columnId.Value);
        if (column == null)
        {
            throw new ColumnNotFoundException();
        }

        column.AddTask(task);
    }

    public void RemoveTask(Guid taskId)
    {
        foreach (IColumn column in _columns)
        {
            column.RemoveTaskById(taskId);
        }
    }

    public void ChangeColumnTitle(Guid columnId, string newTitle)
    {
        IColumn? column = FindColumnById(columnId);
        if (column == null)
        {
            throw new ColumnNotFoundException();
        }

        column.Title = newTitle;
    }

    public void ChangeTaskTitle(Guid taskId, string newTitle)
    {
        foreach (IColumn column in _columns)
        {
            ITask? task = column.FindTaskById(taskId);
            if (task != null)
            {
                task.Title = newTitle;
                return;
            }
        }

        throw new TaskNotFoundException();
    }

    public void ChangeTaskDescription(Guid taskId, string newDescription)
    {
        foreach (IColumn column in _columns)
        {
            ITask? task = column.FindTaskById(taskId);
            if (task != null)
            {
                task.Description = newDescription;
                return;
            }
        }

        throw new TaskNotFoundException();
    }

    public void ChangeTaskPriority(Guid taskId, TaskPriority newPriority)
    {
        foreach (IColumn column in _columns)
        {
            ITask? task = column.FindTaskById(taskId);
            if (task != null)
            {
                task.Priority = newPriority;
                return;
            }
        }

        throw new TaskNotFoundException();
    }
}

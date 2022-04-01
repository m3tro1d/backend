using ScrumBoard.Exception;

namespace ScrumBoard.Model
{
    internal class Board : IBoard
    {
        public string Title { get; }

        public Board(string title)
        {
            Title = title;
            _columns = new();
        }

        public void AddColumn(IColumn column)
        {
            if (_columns.Count == MAX_COLUMNS)
            {
                throw new BoardColumnCountExceededException();
            }

            if (FindColumnByTitle(column.Title) != null)
            {
                throw new ColumnAlreadyExistsException();
            }

            _columns.Add(column);
        }

        public IReadOnlyCollection<IColumn> FindAllColumns()
        {
            return _columns;
        }

        public IColumn? FindColumnByTitle(string title)
        {
            return _columns.Find(column => column.Title == title);
        }

        public void AdvanceTask(string taskTitle)
        {
            ITask? task = null;
            int nextColumnIndex = 0;

            for (int i = 0; i < _columns.Count; ++i)
            {
                task = _columns[i].FindTaskByTitle(taskTitle);
                if (task != null)
                {
                    if (i == _columns.Count - 1)
                    {
                        throw new FinalColumnReachedException();
                    }

                    _columns[i].RemoveTaskByTitle(taskTitle);
                    nextColumnIndex = i + 1;
                    break;
                }
            }

            if (nextColumnIndex != 0 && task != null)
            {
                _columns[nextColumnIndex].AddTask(task);
            }
        }

        public void AddTaskToColumn(ITask task, string? columnTitle = null)
        {
            if (_columns.Count == 0)
            {
                throw new NoColumnsException();
            }

            if (columnTitle == null)
            {
                _columns[0].AddTask(task);
                return;
            }

            IColumn? column = FindColumnByTitle(columnTitle);
            if (column == null)
            {
                throw new ColumnNotFoundException();
            }

            column.AddTask(task);
        }

        public void RemoveTask(string taskTitle)
        {
            foreach (IColumn column in _columns)
            {
                column.RemoveTaskByTitle(taskTitle);
            }
        }

        public void ChangeColumnTitle(string columnTitle, string newTitle)
        {
            IColumn? column = FindColumnByTitle(columnTitle);
            if (column == null)
            {
                throw new ColumnNotFoundException();
            }

            column.Title = newTitle;
        }

        public void ChangeTaskTitle(string taskTitle, string newTitle)
        {
            foreach (IColumn column in _columns)
            {
                ITask? task = column.FindTaskByTitle(taskTitle);
                if (task != null)
                {
                    task.Title = newTitle;
                    return;
                }
            }

            throw new TaskNotFoundException();
        }

        public void ChangeTaskDescription(string taskTitle, string newDescription)
        {
            foreach (IColumn column in _columns)
            {
                ITask? task = column.FindTaskByTitle(taskTitle);
                if (task != null)
                {
                    task.Description = newDescription;
                    return;
                }
            }

            throw new TaskNotFoundException();
        }

        public void ChangeTaskPriority(string taskTitle, TaskPriority newPriority)
        {
            foreach (IColumn column in _columns)
            {
                ITask? task = column.FindTaskByTitle(taskTitle);
                if (task != null)
                {
                    task.Priority = newPriority;
                    return;
                }
            }

            throw new TaskNotFoundException();
        }

        private const int MAX_COLUMNS = 10;
        private List<IColumn> _columns;
    }
}

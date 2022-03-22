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

        private const int MAX_COLUMNS = 10;
        private List<IColumn> _columns;
    }
}

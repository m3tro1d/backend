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

        private const int MAX_COLUMNS = 10;
        private List<IColumn> _columns;
    }
}

namespace ScrumBoard.Model
{
    public interface IBoard
    {
        public string Title { get; }

        public void AddColumn(IColumn column);

        public IReadOnlyCollection<IColumn> FindAllColumns();

        public IColumn? FindColumnByTitle(string title);

        public void AdvanceTask(string taskTitle);
    }
}

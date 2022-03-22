using ScrumBoard.Exception;

namespace ScrumBoard.Model
{
    internal class Column : IColumn
    {
        public string Title { get; set; }

        public Column(string title)
        {
            Title = title;
            _tasks = new();
        }

        public void AddTask(ITask task)
        {
            if (FindTaskByTitle(task.Title) != null)
            {
                throw new ColumnTaskAlreadyExistsException();
            }

            _tasks.Add(task);
        }

        public IReadOnlyCollection<ITask> FindAllTasks()
        {
            return _tasks;
        }

        public ITask? FindTaskByTitle(string title)
        {
            return _tasks.Find(task => task.Title == title);
        }

        private List<ITask> _tasks;
    }
}

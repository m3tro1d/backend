using ScrumBoard.Model;

namespace ScrumBoard.Factory
{
    public class ScrumBoardFactory
    {
        public IBoard CreateBoard(string title)
        {
            return new Board(title);
        }

        public IColumn CreateColumn(string title)
        {
            return new Column(title);
        }

        public ITask CreateTask()
        {
            throw new NotImplementedException();
        }
    }
}

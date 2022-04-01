using ScrumBoard.Exception;
using ScrumBoard.Factory;
using ScrumBoard.Model;
using Xunit;

namespace ScrumBoardTest
{
    public class BoardTest
    {
        [Fact]
        public void CreateBoard_ItHasTitleAndEmptyColumns()
        {
            IBoard board = MockBoard();

            Assert.Equal(_mockTitle, board.Title);
        }

        [Fact]
        public void AddColumn_ItAppearsInTheList()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();

            board.AddColumn(column);

            Assert.Collection(board.FindAllColumns(),
                    boardColumn => Assert.Equal(column, boardColumn)
                );
        }

        [Fact]
        public void AddSeveralColumns_TheyAppearInTheOrderOfAddition()
        {
            IBoard board = MockBoard();
            int amount = 3;

            for (int i = 0; i < amount; ++i)
            {
                IColumn column = ScrumBoardFactory.CreateColumn(i.ToString());
                board.AddColumn(column);
            }

            Assert.Collection(board.FindAllColumns(),
                    column => Assert.Equal("0", column.Title),
                    column => Assert.Equal("1", column.Title),
                    column => Assert.Equal("2", column.Title)
                );
        }

        [Fact]
        public void AddBoardWithTheSameTitle_ThrowsException()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();

            board.AddColumn(column);

            Assert.Throws<ColumnAlreadyExistsException>(() => board.AddColumn(column));
        }

        [Fact]
        public void AddMoreThan10Columns_ThrowsException()
        {
            IBoard board = MockBoard();
            for (int i = 0; i < 10; ++i)
            {
                IColumn column = ScrumBoardFactory.CreateColumn(i.ToString());
                board.AddColumn(column);
            }

            Assert.Throws<BoardColumnCountExceededException>(() => board.AddColumn(MockColumn()));
        }

        [Fact]
        public void FindExistingColumn_ReturnsColumn()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();

            board.AddColumn(column);

            Assert.Equal(column, board.FindColumnByTitle(column.Title));
        }

        [Fact]
        public void FindNonExistingColumn_ReturnsNull()
        {
            IBoard board = MockBoard();

            Assert.Null(board.FindColumnByTitle("whatever"));
        }

        // TODO:
        //   advance task
        //   add task to column
        //   remove task
        //   Change column title
        //   Change task title
        //   Change task description
        //   Change task priority

        private IBoard MockBoard()
        {
            return ScrumBoardFactory.CreateBoard(_mockTitle);
        }

        private IColumn MockColumn()
        {
            return ScrumBoardFactory.CreateColumn(_mockColumnTitle);
        }

        private ITask MockTask()
        {
            return ScrumBoardFactory.CreateTask(_mockTaskTitle, _mockDescription, _mockPriority);
        }

        private string _mockTitle = "Board title";

        private string _mockColumnTitle = "Column title";

        private string _mockTaskTitle = "Task title";
        private string _mockDescription = "Description";
        private TaskPriority _mockPriority = TaskPriority.MEDIUM;
    }
}

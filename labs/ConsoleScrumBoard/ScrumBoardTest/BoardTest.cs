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
                board.AddColumn(ScrumBoardFactory.CreateColumn(i.ToString()));
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
                board.AddColumn(ScrumBoardFactory.CreateColumn(i.ToString()));
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

        [Fact]
        public void AdvanceExistingTask_ItMovesToTheNextColumn()
        {
            IBoard board = MockBoard();
            IColumn column1 = ScrumBoardFactory.CreateColumn("1");
            IColumn column2 = ScrumBoardFactory.CreateColumn("2");
            board.AddColumn(column1);
            board.AddColumn(column2);
            ITask task = MockTask();
            board.AddTaskToColumn(task);

            board.AdvanceTask(task.Title);

            Assert.Empty(column1.FindAllTasks());
            Assert.Collection(column2.FindAllTasks(),
                    columnTask => Assert.Equal(task, columnTask)
                );
        }

        [Fact]
        public void AdvanceNonExistingTask_ThrowsException()
        {
            IBoard board = MockBoard();
            for (int i = 0; i < 5; ++i)
            {
                board.AddColumn(ScrumBoardFactory.CreateColumn(i.ToString()));
            }

            Assert.Throws<TaskNotFoundException>(() => board.AdvanceTask("whatever"));
        }

        [Fact]
        public void AdvanceTaskPastLastColumn_ThrowsException()
        {
            IBoard board = MockBoard();
            board.AddColumn(MockColumn());
            ITask task = MockTask();
            board.AddTaskToColumn(task);

            Assert.Throws<FinalColumnReachedException>(() => board.AdvanceTask(task.Title));
        }

        [Fact]
        public void AdvanceTaskWithNoColumns_ThrowsException()
        {
            IBoard board = MockBoard();

            Assert.Throws<NoColumnsException>(() => board.AdvanceTask("whatever"));
        }

        [Fact]
        public void AddTaskToBoardWithColumns_ItAppearsInTheFirstColumn()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            ITask task = MockTask();
            board.AddColumn(column);

            board.AddTaskToColumn(task);

            Assert.Collection(column.FindAllTasks(),
                    columnTask => Assert.Equal(task, columnTask)
                );
        }

        [Fact]
        public void AddTaskToBoardWithoutColumns_ThrowsException()
        {
            IBoard board = MockBoard();
            ITask task = MockTask();

            Assert.Throws<NoColumnsException>(() => board.AddTaskToColumn(task));
        }

        [Fact]
        public void AddTaskToExistingColumn_ItAppearsInTheColumn()
        {
            IBoard board = MockBoard();
            for (int i = 0; i < 5; ++i)
            {
                board.AddColumn(ScrumBoardFactory.CreateColumn(i.ToString()));
            }
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();

            board.AddTaskToColumn(task, column.Title);

            Assert.Collection(column.FindAllTasks(),
                    columnTask => Assert.Equal(task, columnTask)
                );
        }

        [Fact]
        public void AddTaskToNonExistingColumn_ThrowsException()
        {
            IBoard board = MockBoard();
            for (int i = 0; i < 5; ++i)
            {
                board.AddColumn(ScrumBoardFactory.CreateColumn(i.ToString()));
            }
            ITask task = MockTask();

            Assert.Throws<ColumnNotFoundException>(() => board.AddTaskToColumn(task, "whatever"));
        }

        [Fact]
        public void RemoveExistingTask_RemovesTaskFromTheColumn()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();
            column.AddTask(task);

            board.RemoveTask(task.Title);

            Assert.Empty(column.FindAllTasks());
        }

        [Fact]
        public void RemoveNonExistingTask_ColumnRemainsUnchanged()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            ITask task = MockTask();
            column.AddTask(task);

            board.RemoveTask("whatever");

            Assert.Collection(column.FindAllTasks(),
                    columnTask => Assert.Equal(task, columnTask)
                );
        }

        [Fact]
        public void ChangeExistingColumnTitle_TitleChanges()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            string newTitle = "Updated";

            board.ChangeColumnTitle(column.Title, newTitle);

            Assert.Collection(board.FindAllColumns(),
                    column => Assert.Equal(newTitle, column.Title)
                );
        }

        [Fact]
        public void ChangeNonExistingColumnTitle_ThrowsException()
        {
            IBoard board = MockBoard();

            Assert.Throws<ColumnNotFoundException>(() => board.ChangeColumnTitle("whatever", "Updated"));
        }

        [Fact]
        public void ChangeExistingTaskTitle_TitleChanges()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();
            column.AddTask(task);
            string newTitle = "Updated";

            board.ChangeTaskTitle(task.Title, newTitle);

            Assert.Equal(newTitle, task.Title);
        }

        [Fact]
        public void ChangeNonExistingTaskTitle_ThrowsException()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();
            column.AddTask(task);

            Assert.Throws<TaskNotFoundException>(() => board.ChangeTaskTitle("whatever", "Updated"));
        }

        [Fact]
        public void ChangeExistingTaskDescription_DescriptionChanges()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();
            column.AddTask(task);
            string newDescription = "Updated";

            board.ChangeTaskDescription(task.Title, newDescription);

            Assert.Equal(newDescription, task.Description);
        }

        [Fact]
        public void ChangeNonExistingTaskDescription_ThrowsException()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();
            column.AddTask(task);

            Assert.Throws<TaskNotFoundException>(() => board.ChangeTaskDescription("whatever", "Updated"));
        }

        [Fact]
        public void ChangeExistingTaskPriority_PriorityChanges()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();
            column.AddTask(task);
            TaskPriority newPriority = TaskPriority.NONE;

            board.ChangeTaskPriority(task.Title, newPriority);

            Assert.Equal(newPriority, task.Priority);
        }

        [Fact]
        public void ChangeNonExistingTaskPriority_ThrowsException()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();
            column.AddTask(task);

            Assert.Throws<TaskNotFoundException>(() => board.ChangeTaskPriority("whatever", TaskPriority.NONE));
        }

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

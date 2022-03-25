using ScrumBoard.Exception;
using ScrumBoard.Factory;
using ScrumBoard.Model;

namespace ConsoleScrumBoard
{
    class Program
    {
        public static void Main(string[] args)
        {
            IBoard board = InitializeBoard();
            Console.WriteLine("= Initial boards =\n");
            PrintBoard(board);

            ITask washTheFloorTask = ScrumBoardFactory.CreateTask("Wash the floor", "Floor is kinda dusty, better wash it.", TaskPriority.MEDIUM);
            board.AddTaskToColumn(washTheFloorTask);
            Console.WriteLine("\n= One task added =\n");
            PrintBoard(board);

            board.AdvanceTask("Wash the floor");
            Console.WriteLine("\n= Task advanced =\n");
            PrintBoard(board);

            ITask doTheDishesTask = ScrumBoardFactory.CreateTask("Do the dihes", "Too many dishes are on.", TaskPriority.HIGH);
            board.AddTaskToColumn(doTheDishesTask, "In Progress");
            Console.WriteLine("\n= Task added in \"In Progress\" =\n");
            PrintBoard(board);

            board.ChangeTaskTitle("Do the dihes", "Do the dishes");
            board.ChangeTaskDescription("Do the dishes", "Too many dishes are on, but this can wait.");
            board.ChangeTaskPriority("Do the dishes", TaskPriority.LOW);
            Console.WriteLine("\n= Updated dishes task =\n");
            PrintBoard(board);

            board.AdvanceTask("Wash the floor");
            board.AdvanceTask("Do the dishes");
            Console.WriteLine("\n= All tasks done =\n");
            PrintBoard(board);

            Console.WriteLine("\n= Trying to advance task beyond last column =\n");
            try
            {
                board.AdvanceTask("Do the dishes");
            }
            catch (FinalColumnReachedException)
            {
                Console.WriteLine("Failed! Already reached last column. Board left unchanged.\n");
                PrintBoard(board);
            }

            board.RemoveTask("Wash the floor");
            board.RemoveTask("Do the dishes");
            Console.WriteLine("\n= Removed all tasks =\n");
            PrintBoard(board);
        }

        private static IBoard InitializeBoard()
        {
            IBoard board = ScrumBoardFactory.CreateBoard("House Chores");

            IColumn todoColumn = ScrumBoardFactory.CreateColumn("To Do");
            IColumn inProgressColumn = ScrumBoardFactory.CreateColumn("In Progress");
            IColumn doneColumn = ScrumBoardFactory.CreateColumn("Done");
            board.AddColumn(todoColumn);
            board.AddColumn(inProgressColumn);
            board.AddColumn(doneColumn);

            return board;
        }

        private static void PrintBoard(IBoard board)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (IColumn column in board.FindAllColumns())
            {
                PrintColumn(column);
            }
            Console.ResetColor();
        }

        private static void PrintColumn(IColumn column)
        {
            Console.WriteLine($"== {column.Title} ==");
            PrintTasks(column);
        }

        private static void PrintTasks(IColumn column)
        {
            foreach (ITask task in column.FindAllTasks())
            {
                PrintTask(task);
            }
        }

        private static void PrintTask(ITask task)
        {
            Console.WriteLine($"  [{TaskPriorityToString(task.Priority)}] {task.Title}: {task.Description}");
        }

        private static string TaskPriorityToString(TaskPriority priority)
        {
            switch (priority)
            {
                case TaskPriority.HIGH:
                    return "HIGH";
                case TaskPriority.MEDIUM:
                    return "MEDIUM";
                case TaskPriority.LOW:
                    return "LOW";
                case TaskPriority.NONE:
                    return "NONE";
                default:
                    return "";
            }
        }
    }
}

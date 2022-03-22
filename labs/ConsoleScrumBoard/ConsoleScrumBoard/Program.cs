using ScrumBoard.Factory;
using ScrumBoard.Model;

namespace ConsoleScrumBoard
{
    class Program
    {
        public static void Main(string[] args)
        {
            ScrumBoardFactory scrumBoardFactory = new ScrumBoardFactory();

            IBoard board = scrumBoardFactory.CreateBoard("House Chores");

            IColumn todoColumn = scrumBoardFactory.CreateColumn("To Do");
            IColumn inProgressColumn = scrumBoardFactory.CreateColumn("In Progress");
            IColumn doneColumn = scrumBoardFactory.CreateColumn("Done");
            board.AddColumn(todoColumn);
            board.AddColumn(inProgressColumn);
            board.AddColumn(doneColumn);
            Console.WriteLine("= Initial boards =\n");
            PrintColumns(board);

            ITask washTheFloorTask = scrumBoardFactory.CreateTask("Wash the floor", "Floor is kinda dusty, better wash it.", TaskPriority.MEDIUM);
            todoColumn.AddTask(washTheFloorTask);
            Console.WriteLine("\n= One task added =\n");
            PrintColumns(board);

            board.AdvanceTask("Wash the floor");
            Console.WriteLine("\n= Task advanced =\n");
            PrintColumns(board);
        }

        private static void PrintColumns(IBoard board)
        {
            foreach (IColumn column in board.FindAllColumns())
            {
                PrintColumn(column);
            }
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
                    return "NONE";
                case TaskPriority.NONE:
                    return "NONE";
                default:
                    return "";
            }
        }
    }
}

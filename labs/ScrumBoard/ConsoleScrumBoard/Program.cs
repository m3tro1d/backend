using ScrumBoard.Exception;
using ScrumBoard.Factory;
using ScrumBoard.Model;

namespace ConsoleScrumBoard;

class Program
{
    public static void Main(string[] args)
    {
        IBoard board = ScrumBoardFactory.CreateBoard("House Chores");

        IColumn todoColumn = ScrumBoardFactory.CreateColumn("To Do");
        IColumn inProgressColumn = ScrumBoardFactory.CreateColumn("In Progress");
        IColumn doneColumn = ScrumBoardFactory.CreateColumn("Done");
        board.AddColumn(todoColumn);
        board.AddColumn(inProgressColumn);
        board.AddColumn(doneColumn);

        Console.WriteLine("= Initial boards =\n");
        PrintBoard(board);

        ITask washTheFloorTask = ScrumBoardFactory.CreateTask("Wash the floor", "Floor is kinda dusty, better wash it.", TaskPriority.MEDIUM);
        board.AddTaskToColumn(washTheFloorTask);
        Console.WriteLine("\n= One task added =\n");
        PrintBoard(board);

        board.AdvanceTask(washTheFloorTask.Id);
        Console.WriteLine("\n= Task advanced =\n");
        PrintBoard(board);

        ITask doTheDishesTask = ScrumBoardFactory.CreateTask("Do the dihes", "Too many dishes are on.", TaskPriority.HIGH);
        board.AddTaskToColumn(doTheDishesTask, inProgressColumn.Id);
        Console.WriteLine("\n= Task added in \"In Progress\" =\n");
        PrintBoard(board);

        board.ChangeTaskTitle(doTheDishesTask.Id, "Do the dishes");
        board.ChangeTaskDescription(doTheDishesTask.Id, "Too many dishes are on, but this can wait.");
        board.ChangeTaskPriority(doTheDishesTask.Id, TaskPriority.LOW);
        Console.WriteLine("\n= Updated dishes task =\n");
        PrintBoard(board);

        board.AdvanceTask(washTheFloorTask.Id);
        board.AdvanceTask(doTheDishesTask.Id);
        Console.WriteLine("\n= All tasks done =\n");
        PrintBoard(board);

        Console.WriteLine("\n= Trying to advance task beyond last column =\n");
        try
        {
            board.AdvanceTask(doTheDishesTask.Id);
        }
        catch (FinalColumnReachedException)
        {
            Console.WriteLine("Failed! Already reached last column. Board left unchanged.\n");
            PrintBoard(board);
        }

        board.RemoveTask(washTheFloorTask.Id);
        board.RemoveTask(doTheDishesTask.Id);
        Console.WriteLine("\n= Removed all tasks =\n");
        PrintBoard(board);
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
        Console.WriteLine($"  [{task.Priority}] {task.Title}: {task.Description}");
    }
}

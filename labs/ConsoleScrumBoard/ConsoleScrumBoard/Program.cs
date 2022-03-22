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

            Console.WriteLine("whatever");
        }
    }
}

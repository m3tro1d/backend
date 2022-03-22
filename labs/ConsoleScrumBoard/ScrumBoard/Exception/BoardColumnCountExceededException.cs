namespace ScrumBoard.Exception
{
    public class BoardColumnCountExceededException : System.Exception
    {
        public BoardColumnCountExceededException()
            : base("board column count exceeded")
        {
        }
    }
}

namespace ScrumBoard.Exception;

public class FinalColumnReachedException : System.Exception
{
    public FinalColumnReachedException()
        : base("final column reached")
    {
    }
}

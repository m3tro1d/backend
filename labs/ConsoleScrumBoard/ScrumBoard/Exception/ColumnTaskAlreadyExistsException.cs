namespace ScrumBoard.Exception
{
    public class ColumnTaskAlreadyExistsException : System.Exception
    {
        public ColumnTaskAlreadyExistsException()
            : base("column task already exists")
        {
        }
    }
}

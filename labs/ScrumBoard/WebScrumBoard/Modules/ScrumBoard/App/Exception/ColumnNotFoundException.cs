namespace WebScrumBoard.Modules.ScrumBoard.App.Exception;

class ColumnNotFoundException : System.Exception
{
    public ColumnNotFoundException()
        : base("column not found")
    {

    }
}

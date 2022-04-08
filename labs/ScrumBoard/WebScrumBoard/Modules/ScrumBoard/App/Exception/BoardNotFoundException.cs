namespace WebScrumBoard.Modules.ScrumBoard.App.Exception;

class BoardNotFoundException : System.Exception
{
    public BoardNotFoundException()
        : base("board not found")
    {

    }
}

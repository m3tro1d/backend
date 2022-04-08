namespace WebScrumBoard.Modules.ScrumBoard.App.Exception;

class TaskNotFoundException : System.Exception
{
    public TaskNotFoundException()
        : base("task not found")
    {

    }
}

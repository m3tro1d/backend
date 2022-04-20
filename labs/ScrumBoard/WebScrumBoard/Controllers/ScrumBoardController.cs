using Microsoft.AspNetCore.Mvc;
using ScrumBoard.Model;
using WebScrumBoard.Controllers.Request;
using WebScrumBoard.Controllers.Response;
using WebScrumBoard.Modules.ScrumBoard.App;
using WebScrumBoard.Modules.ScrumBoard.App.Exception;
using WebScrumBoard.Modules.ScrumBoard.App.Query;
using WebScrumBoard.Modules.ScrumBoard.App.Query.Data;

namespace WebScrumBoard.Controllers;

[Route("api/v1/scrumboard")]
[ApiController]
public class ScrumBoardController : ControllerBase
{
    private IBoardService _boardService;
    private IBoardQueryService _boardQueryService;

    public ScrumBoardController(IBoardService boardService, IBoardQueryService boardQueryService)
    {
        _boardService = boardService;
        _boardQueryService = boardQueryService;
    }

    // GET api/v1/scrumboard/board
    [HttpGet("board")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public GetBoardsResponse GetBoards()
    {
        IEnumerable<BoardData> boards = _boardQueryService.ListBoards();
        return new GetBoardsResponse(boards);
    }

    // POST: api/v1/scrumboard/board
    [HttpPost("board")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult CreateBoard([FromBody] CreateBoardRequest request)
    {
        Guid id = _boardService.CreateBoard(request.Title);

        return Created(id.ToString(), null);
    }

    // DELETE api/v1/scrumboard/board/74e353de-2938-4125-9d3c-80acff784644
    [HttpDelete("board/{boardId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult RemoveBoard(string boardId)
    {
        Guid id;
        if (!Guid.TryParse(boardId, out id))
        {
            return BadRequest("invalid guid");
        }

        try
        {
            _boardService.RemoveBoard(id);
            return NoContent();
        }
        catch (BoardNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    // POST: api/v1/scrumboard/column
    [HttpPost("column")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult CreateColumn([FromBody] CreateColumnRequest request)
    {
        Guid id;
        if (!Guid.TryParse(request.BoardId, out id))
        {
            return BadRequest("invalid guid");
        }

        try
        {
            Guid columnId = _boardService.CreateColumn(id, request.Title);
            return Created(columnId.ToString(), null);
        }
        catch (BoardNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // PATCH: api/v1/scrumboard/column/74e353de-2938-4125-9d3c-80acff784644
    [HttpPatch("column/{columnId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ChangeColumnTitle(string columnId, [FromBody] ChangeColumnTitleRequest request)
    {
        Guid id;
        if (!Guid.TryParse(columnId, out id))
        {
            return BadRequest("invalid guid");
        }

        try
        {
            _boardService.ChangeColumnTitle(id, request.Title);
            return Ok();
        }
        catch (ColumnNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    // POST: api/v1/scrumboard/task
    [HttpPost("task")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult CreateTask([FromBody] CreateTaskRequest request)
    {
        Guid id;
        if (!Guid.TryParse(request.ColumnId, out id))
        {
            return BadRequest("invalid guid");
        }

        try
        {
            TaskPriority priority = ApiToDomainTaskPriority(request.Priority);
            Guid taskId = _boardService.CreateTask(id, request.Title, request.Description, priority);
            return Created(taskId.ToString(), null);
        }
        catch (ColumnNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (InvalidPriorityException e)
        {
            return BadRequest(e.Message);
        }
    }

    // PATCH: api/v1/scrumboard/task/74e353de-2938-4125-9d3c-80acff784644
    [HttpPatch("task/{taskId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ChangeTask(string taskId, [FromBody] ChangeTaskRequest request)
    {
        Guid id;
        if (!Guid.TryParse(taskId, out id))
        {
            return BadRequest("invalid guid");
        }

        try
        {
            TaskPriority? priority = null;
            if (request.Priority is not null)
            {
                priority = ApiToDomainTaskPriority((int)request.Priority);
            }

            _boardService.ChangeTask(id, request.Title, request.Description, priority);
            return Ok();
        }
        catch (TaskNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (InvalidPriorityException e)
        {
            return BadRequest(e.Message);
        }
    }

    // POST: api/v1/scrumboard/task/74e353de-2938-4125-9d3c-80acff784644
    [HttpPost("task/{taskId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AdvanceTask(string taskId)
    {
        Guid id;
        if (!Guid.TryParse(taskId, out id))
        {
            return BadRequest("invalid guid");
        }

        try
        {
            _boardService.AdvanceTask(id);
            return Ok();
        }
        catch (TaskNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // DELETE: api/v1/scrumboard/task/74e353de-2938-4125-9d3c-80acff784644
    [HttpDelete("task/{taskId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RemoveTask(string taskId)
    {
        Guid id;
        if (!Guid.TryParse(taskId, out id))
        {
            return BadRequest("invalid guid");
        }

        try
        {
            _boardService.RemoveTask(id);
            return NoContent();
        }
        catch (TaskNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    private static TaskPriority ApiToDomainTaskPriority(int priority)
    {
        return priority switch
        {
            0 => TaskPriority.NONE,
            1 => TaskPriority.LOW,
            2 => TaskPriority.MEDIUM,
            3 => TaskPriority.HIGH,
            _ => throw new InvalidPriorityException(),
        };
    }
}

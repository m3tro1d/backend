using Microsoft.AspNetCore.Mvc;
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
    public ListBoardsResponse ListBoards()
    {
        IEnumerable<BoardData> boards = _boardQueryService.ListBoards();
        return new ListBoardsResponse(boards);
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

    // TODO
    // add task
    // change task title
    // change task description
    // change task priority
    // advance task
}

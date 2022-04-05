using Microsoft.AspNetCore.Mvc;
using WebScrumBoard.Controllers.Request;
using WebScrumBoard.Controllers.Response;
using WebScrumBoard.Modules.ScrumBoard.App;
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

    // GET api/v1/scrumboard
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ListBoardsResponse ListBoards()
    {
        IEnumerable<BoardData> boards = _boardQueryService.ListBoards();
        return new ListBoardsResponse(boards);
    }

    // POST: api/v1/scrumboard
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult CreateBoard([FromBody] CreateBoardRequest request)
    {
        Guid id = _boardService.CreateBoard(request.Title);

        return Created(id.ToString(), null);
    }

    // DELETE api/v1/scrumboard/74e353de-2938-4125-9d3c-80acff784644
    [HttpDelete("{boardId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult RemoveBoard(string boardId)
    {
        Guid id;
        if (!Guid.TryParse(boardId, out id))
        {
            return BadRequest("invalid guid");
        }

        _boardService.RemoveBoard(id);

        return NoContent();
    }

    // POST: api/v1/scrumboard/74e353de-2938-4125-9d3c-80acff784644
    [HttpPost("{boardId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult CreateColumn(string boardId, [FromBody] CreateColumnRequest request)
    {
        // TODO
        return Ok();
    }

    // PATCH: api/v1/scrumboard/74e353de-2938-4125-9d3c-80acff784644/74e353de-2938-4125-9d3c-80acff784644
    [HttpPatch("{boardId}/{columnId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult CreateColumn(string boardId, string columnId, [FromBody] ChangeColumnTitleRequest request)
    {
        // TODO
        return Ok();
    }

    // DELETE: api/v1/scrumboard/74e353de-2938-4125-9d3c-80acff784644/74e353de-2938-4125-9d3c-80acff784644
    [HttpDelete("{boardId}/{columnId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult RemoveColumn(string boardId, string columnId)
    {
        // TODO
        return Ok();
    }

    // TODO
    // add task
    // change task title
    // change task description
    // change task priority
    // advance task
}

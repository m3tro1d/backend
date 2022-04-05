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
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult RemoveBoard(string id)
    {
        Guid boardId;
        if (!Guid.TryParse(id, out boardId))
        {
            return BadRequest("invalid guid");
        }

        _boardService.RemoveBoard(boardId);

        return NoContent();
    }
}

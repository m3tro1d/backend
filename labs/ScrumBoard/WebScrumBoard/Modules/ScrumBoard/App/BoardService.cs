﻿using ScrumBoard.Factory;
using ScrumBoard.Model;

namespace WebScrumBoard.Modules.ScrumBoard.App;

public class BoardService : IBoardService
{
    private IBoardStore _boardStore;

    public BoardService(IBoardStore boardStore)
    {
        _boardStore = boardStore;
    }

    public Guid CreateBoard(string title)
    {
        IBoard board = ScrumBoardFactory.CreateBoard(title);
        _boardStore.Store(board);

        return board.Id;
    }

    public void RemoveBoard(Guid boardId)
    {
        IBoard board = _boardStore.FindOne(boardId);
        _boardStore.Remove(board.Id);
    }

    public Guid CreateColumn(Guid boardId, string title)
    {
        IBoard board = _boardStore.FindOne(boardId);
        IColumn column = ScrumBoardFactory.CreateColumn(title);
        board.AddColumn(column);
        _boardStore.Store(board);

        return column.Id;
    }

    public void ChangeColumnTitle(Guid columnId, string title)
    {
        IBoard board = _boardStore.FindOneByColumnId(columnId);
        board.ChangeColumnTitle(columnId, title);
        _boardStore.Store(board);
    }
}

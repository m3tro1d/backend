using System;
using ScrumBoard.Exception;
using ScrumBoard.Factory;
using ScrumBoard.Model;
using Xunit;

namespace ScrumBoardTest;

public class ColumnTest
{
    [Fact]
    public void CreateColumn_ItHasTitleAndEmptyTasks()
    {
        IColumn column = MockColumn();

        Assert.Equal(_mockTitle, column.Title);
        Assert.Empty(column.FindAllTasks());
    }

    [Fact]
    public void ChangeColumnTitle_TitleChanges()
    {
        IColumn column = MockColumn();
        string newTitle = "Updated";

        column.Title = newTitle;

        Assert.Equal(newTitle, column.Title);
    }

    [Fact]
    public void AddTask_ItAppearsInTheList()
    {
        IColumn column = MockColumn();
        ITask task = MockTask();

        column.AddTask(task);

        Assert.Collection(column.FindAllTasks(),
                columnTask => Assert.Equal(task, columnTask)
            );
    }

    [Fact]
    public void AddSeveralTasks_TheyAppearInTheOrderOfAddition()
    {
        IColumn column = MockColumn();
        int amount = 3;

        for (int i = 0; i < amount; ++i)
        {
            column.AddTask(ScrumBoardFactory.CreateTask(i.ToString(), _mockDescription, _mockPriority));
        }

        Assert.Collection(column.FindAllTasks(),
                task => Assert.Equal("0", task.Title),
                task => Assert.Equal("1", task.Title),
                task => Assert.Equal("2", task.Title)
            );
    }

    [Fact]
    public void AddTaskWithTheSameTitle_ThrowsException()
    {
        IColumn column = MockColumn();
        ITask task = MockTask();

        column.AddTask(task);

        Assert.Throws<TaskAlreadyExistsException>(() => column.AddTask(task));
    }

    [Fact]
    public void FindExistingTask_ReturnsTask()
    {
        IColumn column = MockColumn();
        ITask task = MockTask();

        column.AddTask(task);

        Assert.Equal(task, column.FindTaskById(task.Id));
    }

    [Fact]
    public void FindNonExistingTask_ReturnsNull()
    {
        IColumn column = MockColumn();

        Assert.Null(column.FindTaskById(Guid.NewGuid()));
    }

    [Fact]
    public void RemoveExistingTask_RemovesTaskFromTheList()
    {
        IColumn column = MockColumn();
        ITask task = MockTask();
        column.AddTask(task);

        column.RemoveTaskById(task.Id);

        Assert.Empty(column.FindAllTasks());
    }

    [Fact]
    public void RemoveNonExistingTask_TaskListRemainsUnchanged()
    {
        IColumn column = MockColumn();
        ITask task = MockTask();
        column.AddTask(task);

        column.RemoveTaskById(Guid.NewGuid());

        Assert.Single(column.FindAllTasks());
    }

    private IColumn MockColumn()
    {
        return ScrumBoardFactory.CreateColumn(_mockTitle);
    }

    private ITask MockTask()
    {
        return ScrumBoardFactory.CreateTask(_mockTaskTitle, _mockDescription, _mockPriority);
    }

    private string _mockTitle = "Column title";

    private string _mockTaskTitle = "Task title";
    private string _mockDescription = "Description";
    private TaskPriority _mockPriority = TaskPriority.MEDIUM;
}

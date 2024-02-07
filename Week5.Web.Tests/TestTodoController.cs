using Week5.Web.Controllers;
using Week5.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace Week5.Web.Tests;
public class TestTodoController
{
  private readonly DatabaseContext dbContext;
  private readonly TodosController todoController;
  public TestTodoController()
  {
    // Set up DbContext
    var options = new DbContextOptionsBuilder<DatabaseContext>()
        .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
        .Options;
    dbContext = new DatabaseContext(options);
    dbContext.Database.EnsureDeleted();

    // Optionally, seed the database with test data here

    // Create an instance of the controller
    todoController = new TodosController(dbContext);
  }

  [Fact]
  public async Task GivenNoTodos_Get_AllTodos_OnSuccess_ReturnsAllNo_todos()
  {
    var result = await todoController.GetTodoItems();
    var value = result.Value;

    // Asser
    // the result exists 
    result.Should().BeOfType<ActionResult<IEnumerable<Todo>>>();
    // length of array is 0
    value.Should().BeEmpty();
  }
  

  [Fact]
  public async Task Get_AllTodos_OnSuccess_ReturnsAllTodos()
  {
    dbContext.Todos.Add(new Todo { Title = "Test 1", Completed = false });
    dbContext.Todos.Add(new Todo { Title = "Test 2", Completed = false });
    dbContext.Todos.Add(new Todo { Title = "Test 3", Completed = false });
    dbContext.Todos.Add(new Todo { Title = "Test 4", Completed = false });
    dbContext.Todos.Add(new Todo { Title = "Test 5", Completed = false });
    await dbContext.SaveChangesAsync();

    var result = await todoController.GetTodoItems();

    // Assert
    result.Should().BeOfType<ActionResult<IEnumerable<Todo>>>();
    result.Value.Should().BeOfType<List<Todo>>();
    result.Value.Should().HaveCount(5);

    result.Value.Should().BeEquivalentTo(
      new List<Todo>
      {
        new Todo { Id = 1, Title = "Test 1", Completed = false },
        new Todo { Id = 2, Title = "Test 2", Completed = false },
        new Todo { Id = 3, Title = "Test 3", Completed = false },
        new Todo { Id = 4, Title = "Test 4", Completed = false },
        new Todo { Id = 5, Title = "Test 5", Completed = false }
      },
      options => options.ComparingByMembers<Todo>().ExcludingMissingMembers()
    );
  }
















  [Fact]
  public async Task GetOne_OnSuccess_ReturnsOne()
  {

    dbContext.Todos.Add(new Todo { Title = "Test 1", Completed = false });
    await dbContext.SaveChangesAsync();

    var result = await todoController.GetTodoItem(1);
    var todo = result.Value;
    // Assert
    todo.Should().NotBeNull();
    todo!.Title.Should().Be("Test 1");
  }

  [Fact]
  public async Task GetOne_OnSuccess_ReturnsNothing()
  {
    var result = await todoController.GetTodoItem(1);
    var todo = result.Value;
    todo.Should().BeNull();
  }

  [Fact]
  public async Task Post_OnSuccess_ReturnsCreatedAtAction()
  {
    var todo = new Todo { Title = "New Task", Completed = false };
    var result = await todoController.PostTodoItem(todo);

    var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
    createdAtActionResult.ActionName.Should().Be(nameof(TodosController.GetTodoItem));

    var returnedTodo = Assert.IsType<Todo>(createdAtActionResult.Value);
    returnedTodo.Should().BeEquivalentTo(todo, options => options.ComparingByMembers<Todo>().ExcludingMissingMembers());
    returnedTodo.Id.Should().NotBe(0);
  }

  [Fact]
  public async Task Put_OnSuccess_NoContentResult()
  {
    var todo = new Todo { Title = "Existing Task", Completed = false };
    dbContext.Todos.Add(todo);
    await dbContext.SaveChangesAsync();

    todo.Title = "Updated Task";
    var result = await todoController.PutTodoItem(todo.Id, todo);

    result.Should().BeOfType<NoContentResult>();
    dbContext.Todos.Find(todo.Id)?.Title.Should().Be("Updated Task");
  }

  [Fact]
  public async Task Delete_OnSuccess_ReturnsNoContentResult()
  {
    var todo = new Todo { Title = "Task to Delete", Completed = false };
    dbContext.Todos.Add(todo);
    await dbContext.SaveChangesAsync();

    var result = await todoController.DeleteTodoItem(todo.Id);

    result.Should().BeOfType<NoContentResult>();
    dbContext.Todos.Find(todo.Id).Should().BeNull();
  }

}
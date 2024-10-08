using Harjoitus1;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ToDoDB>(opt => opt.UseInMemoryDatabase("ToDoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

var todoItems = app.MapGroup("/todoitems");

todoItems.MapGet("/", GetAllTodos);
todoItems.MapGet("/complete", GetCompleteTodos);
todoItems.MapGet("/{id}", GetTodo);
todoItems.MapPost("/", CreateTodo);
todoItems.MapPut("/{id}", UpdateTodo);
todoItems.MapDelete("/{id}", DeleteTodo);

app.Run();

static async Task<IResult> GetAllTodos(ToDoDB db)
{
    return TypedResults.Ok(await db.Todos.Select(x => new TodoItemDTO(x)).ToArrayAsync());
}
static async Task<IResult> GetCompleteTodos(ToDoDB db)
{
    return TypedResults.Ok(await db.Todos.Where(t => t.IsComplete).Select(
        x => new TodoItemDTO(x)).ToListAsync());
}
static async Task<IResult> GetTodo(int id, ToDoDB db)
{
    return await db.Todos.FindAsync(id)
        is ToDo todo
            ? TypedResults.Ok(new TodoItemDTO(todo))
            : TypedResults.NotFound();
}
static async Task<IResult> CreateTodo(TodoItemDTO todoItemDTO, ToDoDB db)
{
    var todoItem = new ToDo
    {
        IsComplete = todoItemDTO.IsComplete,
        Name = todoItemDTO.Name
    };
    db.Todos.Add(todoItem);
    await db.SaveChangesAsync();

    todoItemDTO = new TodoItemDTO(todoItem);

    return TypedResults.Created($"/todoitems/{todoItem.Id}", todoItemDTO);
}
static async Task<IResult> UpdateTodo(int id, TodoItemDTO todoItemDTO, ToDoDB db)
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return TypedResults.NotFound();

    todo.Name = todoItemDTO.Name;
    todo.IsComplete = todoItemDTO.IsComplete;

    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}
static async Task<IResult> DeleteTodo(int id, ToDoDB db)
{
    if (await db.Todos.FindAsync(id) is ToDo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
    return TypedResults.NotFound();
}

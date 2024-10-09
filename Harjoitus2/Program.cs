using Harjoitus2;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<QuestDb>(opt => opt.UseInMemoryDatabase("QuestList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();
var quests = app.MapGroup("/quests");

/*quests.MapGet("/", GetAllQuests);
quests.MapGet("/{id}", GetQuest);
quests.MapPost("/", CreateQuest);*/

app.MapGet("/quests", async (QuestDb db) =>
    await db.Quests.ToListAsync()
);

app.MapGet("/quests/{id}", async (int id, QuestDb db) =>
    await db.Quests.FindAsync(id)
        is Quest quest
            ? Results.Ok(quest)
            : Results.NotFound()
);
app.MapPost("/quests", async (Quest quest, QuestDb db) =>
{   
    db.Quests.Add(quest);
    await db.SaveChangesAsync();

    return Results.Created($"/quests/{quest.Id}", quest);
});


app.Run();



/*static async Task<IResult> GetAllQuests(QuestDb db)
{
    return TypedResults.Ok(await db.Quests.ToArrayAsync());
}
static async Task<IResult> GetQuest(int id, QuestDb db)
{
    return await db.Quests.FindAsync(id)
        is Quest quest
            ? TypedResults.Ok(quest)
            : TypedResults.NotFound();
}
static async Task<IResult> CreateQuest(Quest quest, QuestDb db)
{
    db.Quests.Add(quest);
    await db.SaveChangesAsync();

    return TypedResults.Created($"/quests/{quest.Id}", quest);
}*/

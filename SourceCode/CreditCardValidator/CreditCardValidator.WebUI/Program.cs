var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapGet("/", async context =>
{
    var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Pages", "index.html");

    if (File.Exists(filepath))
    {
        context.Response.ContentType = "text/html";
        await context.Response.SendFileAsync(filepath);
    }
    else
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
    }
});

app.Run();

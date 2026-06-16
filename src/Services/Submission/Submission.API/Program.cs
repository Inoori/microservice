using Submission.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapAllEndpoints();


app.Run();
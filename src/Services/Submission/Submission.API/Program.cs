using Submission.API;
using Submission.API.Endpoints;
using Submission.Application;
using Submission.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices()
    .AddApplicationServices()
    .AddPersistenceServices(builder.Configuration, builder.Environment.IsDevelopment());

var app = builder.Build();

app.UseHttpsRedirection();

app.MapAllEndpoints();


app.Run();
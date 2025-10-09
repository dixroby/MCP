using DynamicQueries.Core;
using DynamicQueries.Repositories;
using DynamicQueries.Rest.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDynamicQueriesServices();
builder.Services.AddDynamicQueriesRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseDynamicQueriesMetadataEndpoints();
app.UseDynamicQueriesEndpoint();
app.Run();
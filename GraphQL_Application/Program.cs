using GraphQL_Application;
using GraphQL_Application.DataLoader;
using GraphQL_Application.Extensions;
using GraphQL_Application.Mutation;
using GraphQL_Application.Queries;
using GraphQL_Application.Schema;
using Microsoft.EntityFrameworkCore;
using Raven.Client.Documents;
using Serilog;
using static Raven.Client.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var schemaAuth =Boolean.Parse(builder.Configuration["graphQlSchemaAuth"]);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGraphQLServer()
    .RegisterDbContext<DataContext>()
    .AddQueryType<SqlAuctionQuery>()
    .AddDataLoader<AuctionBidByAuctionDataLoader>()
    .AddType<SqlAuctionType>()
    //.AddMutationType<AuctionMutationcs>()
    .AllowIntrospection(schemaAuth);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Logging.AddSerilog();
builder.Host.UseSerilog();

builder.Services.AddDbContext<DataContext>(options =>
{
    var folder = Environment.SpecialFolder.LocalApplicationData;
    var path = Environment.GetFolderPath(folder);
    options.UseSqlite($"Data Source ="+ System.IO.Path.Join(path, "auction.db"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.SeedData();
app.SeedSQLData();

app.MapGraphQL().WithOptions(new HotChocolate.AspNetCore.GraphQLServerOptions { Tool = { Enable = schemaAuth } });

app.MapControllers();

app.Run();

using GraphQL_Application.Extensions;
using GraphQL_Application.Mutation;
using GraphQL_Application.Queries;
using GraphQL_Application.Schema;
using Raven.Client.Documents;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var schemaAuth =Boolean.Parse(builder.Configuration["graphQlSchemaAuth"]);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGraphQLServer()
    //.AllowIntrospection(schemaAuth)
    .AddQueryType(t => t.Name("Query"))
    .AddTypeExtension<AuctionQuery>()
    .AddTypeExtension<MyQueries>()
    .AddType<AuctionType>()
    .AddMutationType<AuctionMutationcs>()
    .AddRavenFiltering()
    .AddRavenSorting()
    .AddRavenProjections()
    .AddRavenPagingProviders();

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Logging.AddSerilog();
builder.Host.UseSerilog();

builder.Services.AddSingleton<IDocumentStore>(_ =>
new DocumentStore
{
    Urls = new[] { "https://a.uk-dev.emrgroup.ravendb.cloud/" },
    Database = "india-8",
    Certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(
        @"C:\Users\mahima.nadig\Downloads\uk-dev.emrgroup.client.certificate\uk-dev.emrgroup.client.certificate.with.password.pfx",
        "3922FD4111EC2C99571775AAF2959AD")
}.Initialize());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.SeedData();

app.MapGraphQL().WithOptions(new HotChocolate.AspNetCore.GraphQLServerOptions { Tool = { Enable = schemaAuth } });

app.MapControllers();

app.Run();

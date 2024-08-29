using Bookstore.GraphQL.Mutation;
using Bookstore.GraphQL.Query;
using Bookstore.GraphQL.Types;
using Bookstore.Mapping;
using DataLayer.Models;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddGraphQLServer()
    .AddType<BookType>()
    .AddType<AuthorType>()
    .AddQueryType<BookstoreQuery>()
    .AddMutationType<BooksMutation>();



builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<BookstoreQuery>();
builder.Services.AddScoped<BooksMutation>();

builder.Services.AddDbContext<BookstoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("bookstoreDBConnection"),
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 5, // The maximum number of retry attempts (default is 5)
                                maxRetryDelay: TimeSpan.FromSeconds(30), // The maximum delay between retries
                                errorNumbersToAdd: null); // Add SQL error numbers to the list of transient errors
                        }), ServiceLifetime.Scoped);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins(["http://localhost:5121", "http://localhost:5074"])
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BookstoreDbContext>();

    dbContext.Database.Migrate();

    SeedData(dbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UsePlayground(new PlaygroundOptions()
    {
        QueryPath = "/api",
        Path = "/playground"
    });
}

//app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");
app.UseRouting();
app.UseEndpoints(x => x.MapGraphQL("/graphql"));

app.UseAuthorization();

app.MapControllers();

app.Run();

void SeedData(BookstoreDbContext context)
{

    if (!context.Books.Any() && !context.Authors.Any())
    {
        var authors = new List<Author>
        {
            new Author { Name = "Author 1" },
            new Author { Name = "Author 2" },
            new Author { Name = "Author 3" }
        };

        context.Authors.AddRange(authors);
        context.SaveChanges();

        var books = new List<Book>
        {
            new Book { Title = "Harry Potter", ISBN = "HP-123", AuthorId = authors[0].Id },
            new Book { Title = "Alice in Wonderland", ISBN = "0987654321", AuthorId = authors[0].Id },
            new Book { Title = "Jungle Book", ISBN = "BK-654321", AuthorId = authors[1].Id },
            new Book { Title = "Facebook", ISBN = "60087654321", AuthorId = authors[1].Id },
            new Book { Title = "English", ISBN = "4008654321", AuthorId = authors[1].Id },
            new Book { Title = "Grammar", ISBN = "3007654321", AuthorId = authors[2].Id }
        };

        context.Books.AddRange(books);
        context.SaveChanges();
    }
}
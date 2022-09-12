using Bogus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

var fakeUser=new Faker<User>();
    fakeUser.RuleFor(x=>x.Id,f=>f.Random.Number(1_000_000))
    .RuleFor(x=>x.Name,f=>f.Person.FirstName)
    .RuleFor(x=>x.LastName,f=>f.Person.LastName)
    .RuleFor(x=>x.Username,f=>f.Person.UserName)
    .RuleFor(x=>x.Email,f=>f.Person.Email)
    .RuleFor(x=>x.BirthDate,f=>f.Person.DateOfBirth)
    .RuleFor(x=>x.Country,f=>f.Address.Country())
    .RuleFor(x=>x.City,f=>f.Address.City())
    ;

app.Map("/", () => fakeUser.Generate());

app.Run();
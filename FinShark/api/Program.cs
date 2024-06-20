using api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Let's hook up the DB
builder.Services.AddDbContext<ApplicationDBContext>(options => {
    //This is where you plug in which database you want to use
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); //Search within our app settings json
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// Controls the pipeline (where middleware will be)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
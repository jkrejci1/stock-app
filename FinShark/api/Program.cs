using api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Let's hook up the DB by using that ApplicationDBContext class
//So this along with the code that's inside ApplicationDBContext is what creates our database wit the data we want
builder.Services.AddDbContext<ApplicationDBContext>(options => {
    //This is where you plug in which database you want to use
    //This searches within our appsettings.json file for DefaultConnection within our ConnectionStrings which will then contain our connection string (May need to search for one, it also needs to contain the name of the device you're doing this on (so the name of my laptop for instance in my case))
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
using api.Data;
using api.Interfaces; //Bring in interface folder to allow our program to use it
using api.Models;
using api.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers(); //Add the controllers to the api Program
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Use the Newton Software for certain key relationships (PREVENTS OBJECT CYCLES!!)
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

//Let's hook up the DB by using that ApplicationDBContext class
//So this along with the code that's inside ApplicationDBContext is what creates our database with the data we want
builder.Services.AddDbContext<ApplicationDBContext>(options => {
    //This is where you plug in which database you want to use
    //This searches within our appsettings.json file for DefaultConnection within our ConnectionStrings which will then contain our connection string (May need to search for one, it also needs to contain the name of the device you're doing this on (so the name of my laptop for instance in my case))
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); //Search within our app settings json
});

//Allows us to use the interfaces in our program
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
// Controls the pipeline (where middleware will be)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); //HTTPS redirect error fix

app.Run();
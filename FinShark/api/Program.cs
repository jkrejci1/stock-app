using api.Data;
using api.Interfaces; //Bring in interface folder to allow our program to use it
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

//Add our user model stuff for user profile data
builder.Services.AddIdentity<AppUser, IdentityRole>(options => {
    options.Password.RequireDigit = true; //Requires saved passwords to contain numbers
    options.Password.RequireLowercase = true; //Requires lower case letters
    options.Password.RequireUppercase = true; //Require uppercase
    options.Password.RequireNonAlphanumeric = true; //Require special chars
    options.Password.RequiredLength = 12; //Password must at least have a length of 12 chars
})
.AddEntityFrameworkStores<ApplicationDBContext>(); //This is connected to the builder above, adds the entity framework for it

//Used to add JWT schema
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = 
    options.DefaultChallengeScheme = 
    options.DefaultForbidScheme = 
    options.DefaultScheme = 
    options.DefaultSignInScheme = 
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    //Token validation params
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"], //Deployment stuff is inappsettings.json regarding this stuff, and NEED TO POSSIBLY EDIT THIS WHEN TRYING TO DEPLOY THIS TO SOMETHING LIKE AZURE OR AWS (KEEP AND EYE OUT!!) issuer == the server, audience == whoevers using the app, SigningKey == IMPROTANT --> the secret that signs JWT tokens (NEEDS TO BE HIDDEN AND SECURE)
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey( //Encrypt the signing key
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]) //Grabs the signing key from our app settings json file
        )
    };
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

//Use authentication and authorization for validation
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); //HTTPS redirect error fix

app.Run();
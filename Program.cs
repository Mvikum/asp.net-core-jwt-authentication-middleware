using StudentService.Services;
using StudentService;
using Microsoft.EntityFrameworkCore;
using StudentService.Middlewares.helpers.utils;
using StudentService.Middlewares;

var builder = WebApplication.CreateBuilder(args);
//add global attributes
GlobalAttributes.mysqlConfiguration.connectionString = builder.Configuration.GetConnectionString("MySqlConnection");



//add appication DB context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

//register custom services
builder.Services.AddScoped<IStudentService, StudentServices>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp",
        builder => builder
            .WithOrigins("http://localhost:5173") // your Vite/Vue dev server
            .AllowAnyMethod()
            .AllowAnyHeader());
});
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
app.UseJwtMiddleware();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

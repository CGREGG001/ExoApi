using ExoApi;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// builder.Services.AddTransient<SqlConnection>(option =>
// {
//     string connectionString = builder.Configuration.GetConnectionString("Server=127.0.0.1,1400;Database=ExoApi;User Id=SA;Password=Test1234=;TrustServerCertificate=True;")!;
//     return new SqlConnection(connectionString);
// });

builder.Services.AddTransient<SqlConnection>( c => new SqlConnection(builder.Configuration.GetConnectionString("Server=127.0.0.1,1400;Database=ExoApi;User Id=SA;Password=Test1234=;TrustServerCertificate=True;")));

builder.Services.AddTransient<UserServices>();

// Add services to the container.
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

app.MapControllers();

app.Run();

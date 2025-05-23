using ProviderApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//MySQL Server config
builder.Services.AddDbContext<ProviderDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration
       .GetConnectionString("DefaultConnection"))
);

// CORS para comunicación con el front en development
builder.Services.AddCors(opts =>
  opts.AddPolicy("AllowSPA", p => p
    .AllowAnyOrigin()   // <<< en dev solo, no en prod
    .AllowAnyHeader()
    .AllowAnyMethod()
  )
);

builder.Services.AddControllers();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowSPA");

app.MapControllers();

app.Run();

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrimeTech.DataAccess.Data;
using PrimeTech.Mapping;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Map));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseCors(x => x.WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader().SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());
app.MapControllers();
app.Run();

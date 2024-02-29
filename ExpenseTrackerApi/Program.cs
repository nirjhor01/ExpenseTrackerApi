using ExpenseTrackerApi.Repository.Interfaces;

using ExpenseTrackerApi.Repository.Implementations;
using ExpenseTrackerApi.Service.Implementations;
using ExpenseTrackerApi.Service.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddTransient<IExpenseTrackerRepository, ExpenseTrackerRepository>();
builder.Services.AddTransient<IExpeneseTrackerServices, ExpenseTrackerServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
//app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());


app.MapControllers();

app.Run();

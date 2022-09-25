using Microsoft.Extensions.Hosting;
using WebApplication1;
using WebApplication1.Logger;
using Serilog;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Host.UseSerilog((hostContext, loggerConfiguration) => _ = loggerConfiguration.ReadFrom.Configuration(builder.Configuration));

builder.Logging.AddConsole();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Logging.AddFileLogger(options =>
//{
//    builder.Configuration.GetSection("Logging").GetSection("RoundTheCodeFile").GetSection("Options").Bind(options);
//});
// builder.Services.AddSingleton<typeof(CustomMiddleWare)>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
app.UseMiddleware<CustomMiddleWare>();
app.UseHttpLogging();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
Log.CloseAndFlush();

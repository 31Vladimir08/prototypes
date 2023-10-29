using DeliveryDatasService.Extensions;
using DeliveryDatasService.HostedServices;
using DeliveryDatasService.Models.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterInIoC();
builder.Services.Configure<DbSettingsOption>(builder.Configuration.GetSection("DbSettings"));
builder.Services.Configure<TablesOption>(builder.Configuration.GetSection("TablesSetting"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<DeliveryHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Confluent.Kafka;

using Quartz;
using Quartz.AspNetCore;

using QuartzService.GrpcServices;

using QuartzService.DataBase;
using QuartzService.Enums;
using QuartzService.Extensions;
using QuartzService.Interceptors;
using QuartzService.Models.Options.DataBase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DbSettingsOption>(builder.Configuration.GetSection("DbSettings"));
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));

builder.Services.AddDbContextFactory<QuartzContext>();
builder.Services.BuildServiceProvider().GetRequiredService<QuartzContext>()
    .Database.EnsureCreated();
builder.Services.RegisterInIoC();

builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<LogInterceptor>();
}).AddJsonTranscoding();

// Add services to the container.
builder.Services.AddQuartz(q =>
{
    //q.UseMicrosoftDependencyInjectionJobFactory();
    q.UsePersistentStore(s =>
    {
        s.PerformSchemaValidation = true; // default
        s.UseProperties = true; // preferred, but not default
        var retryInterval = Convert.ToDouble(builder.Configuration["QuartzSettings:RetryInterval"]);
        s.RetryInterval = TimeSpan.FromSeconds(retryInterval);
        var connectionString = builder.Configuration["DbSettings:ConnectionString"] ?? string.Empty;
        if (SupportedDb.MSSQL.ToString().Equals(builder.Configuration["DbSettings:TypeDb"]))
        {
            s.UseSqlServer(sqlServer =>
            {
                sqlServer.ConnectionString = connectionString;
                // this is the default
                sqlServer.TablePrefix = "QRTZ_";
            });
        }
        else if (SupportedDb.PostgreSQL.ToString().Equals(builder.Configuration["DbSettings:TypeDb"]))
        {
            s.UsePostgres(sqlServer =>
            {
                sqlServer.ConnectionString = connectionString;
                // this is the default
                sqlServer.TablePrefix = "QRTZ_";
            });
        }

        s.UseJsonSerializer();
        s.UseClustering(c =>
        {
            c.CheckinMisfireThreshold = TimeSpan.FromSeconds(60);
            c.CheckinInterval = TimeSpan.FromSeconds(60);
        });
    });
});
// ASP.NET Core hosting
builder.Services.AddQuartzServer(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddGrpcSwagger().AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapGrpcService<JobService>();

app.Run();

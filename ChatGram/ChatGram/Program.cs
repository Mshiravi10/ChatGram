using Domain.Entities.ChatgramCore;
using Domain.ILifeTime;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});



builder.Services.AddDbContext<ChatgramCoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ChatgramCore")));

builder.Services.AddDbContext<ChatgramFileDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ChatgramFile")));

builder.Services.AddIdentity<UserEntity, IdentityRole>()
    .AddEntityFrameworkStores<ChatgramCoreDbContext>()
.AddDefaultTokenProviders();

var redisConfiguration = ConfigurationOptions.Parse(builder.Configuration.GetSection("Redis:ConnectionString").Value);

var connectionMultiplexer = ConnectionMultiplexer.Connect(redisConfiguration);

builder.Services.AddSingleton<IConnectionMultiplexer>(connectionMultiplexer);

builder.Services.Scan(scan => scan
                        .FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                        .AddClasses(classes => classes.AssignableTo<ITransientService>())
                        .AsImplementedInterfaces()
                        .WithTransientLifetime());

builder.Services.Scan(scan => scan
                        .FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                        .AddClasses(classes => classes.AssignableTo<IScopedService>())
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

builder.Services.Scan(scan => scan
                        .FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                        .AddClasses(classes => classes.AssignableTo<ISingeltonService>())
                        .AsImplementedInterfaces()
                        .WithSingletonLifetime());


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetSection("Redis:ConnectionString").Value;
    options.InstanceName = "Chatgram";
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseCors("AllowReactApp");



app.Run();
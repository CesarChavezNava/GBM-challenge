using AutoMapper;
using Broker.Accounts.Application.Create;
using Broker.Accounts.Application.Find;
using Broker.Accounts.Application.Search;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Repositories;
using Broker.Accounts.Domain.Rules;
using Broker.Accounts.Infrastructure.API;
using Broker.Accounts.Infrastructure.API.Middlewares;
using Broker.Accounts.Infrastructure.SQL;
using Broker.Accounts.Infrastructure.SQL.Repositories;
using Broker.Core.Rules;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5024);
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region AutoMapper

MapperConfiguration mapperConfiguration = new(config =>
{
    config.AddProfiles(new Profile[] { new MapperSQLSchemasProfile(), new MapperDtosProfile() });
});

IMapper mapper = mapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);

#endregion AutoMapper

#region DbContext

builder.Services.AddDbContext<AccountContext>(options =>
{
    options.UseSqlServer("name=AccountConnection");
});

#endregion DbContext

#region FluentValidation

builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

#endregion FluentValidation

#region Policies

builder.Services.AddScoped<Policy<WriteOrder>, OrdersPolicy>();

#endregion Policies

#region Ports

builder.Services.AddScoped<IForCreateAccount, AccountCreator>();
builder.Services.AddScoped<IForCreateOrder, OrderCreator>();
builder.Services.AddScoped<IForFindAccount, AccountFinder>();
builder.Services.AddScoped<IForSearchOrders, OrdersSearcher>();

#endregion Ports

#region Repositories

builder.Services.AddScoped<IAccountRepository, SQLAccountRepository>();
builder.Services.AddScoped<IOrderRepository, SQLOrderRepository>();

#endregion Repositories

var app = builder.Build();

#region Migrations

// Migrations
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AccountContext>();
    context.Database.Migrate();
}

#endregion Migrations

#region Custom Middlewares

app.UseMiddleware<ExceptionCatchingMiddleware>();

#endregion Custom Middlewares

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

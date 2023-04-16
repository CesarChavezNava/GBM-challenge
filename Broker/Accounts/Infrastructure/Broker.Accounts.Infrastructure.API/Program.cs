using AutoMapper;
using Broker.Accounts.Application.Create;
using Broker.Accounts.Application.Find;
using Broker.Accounts.Domain.Repositories;
using Broker.Accounts.Infrastructure.API;
using Broker.Accounts.Infrastructure.SQL;
using Broker.Accounts.Infrastructure.SQL.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region DbContext

builder.Services.AddDbContext<AccountContext>(options =>
{
    options.UseSqlServer("name=AccountConnection");
});

#endregion DbContext

#region AutoMapper

MapperConfiguration mapperConfiguration = new(config =>
{
    config.AddProfiles(new Profile[] { new MapperSQLSchemasProfile(), new MapperDtosProfile() });
});

IMapper mapper = mapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);

#endregion AutoMapper

#region FluentValidation

builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

#endregion FluentValidation

#region Repositories

builder.Services.AddScoped<IAccountRepository, SQLAccountRepository>();
builder.Services.AddScoped<IIssuerRepository, SQLIssuerRepository>();
builder.Services.AddScoped<IOrderRepository, SQLOrderRepository>();

#endregion Repositories

#region Ports

builder.Services.AddScoped<IForCreateAccount, AccountCreator>();
builder.Services.AddScoped<IForCreateOrder, OrderCreator>();
builder.Services.AddScoped<IForFindAccount, AccountFinder>();

#endregion Ports

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

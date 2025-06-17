using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rommanel.API.Converters;
using Rommanel.API.Middleware;
using Rommanel.Application;
using Rommanel.Application.Behaviors;
using Rommanel.Application.Clientes.Strategies.UpdateCliente;
using Rommanel.Domain.Repositories;
using Rommanel.Infrastructure.Data;
using Rommanel.Infrastructure.Repositories;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(IApplicationMarker));
builder.Services.AddValidatorsFromAssemblyContaining<IApplicationMarker>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter());
    });

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

var connectionString = builder.Configuration.GetConnectionString("Default")
    ?? "Host=db;Database=rommanel;Username=postgres;Password=postgres";
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IUnitOfWork, ClienteRepository>();

builder.Services.AddScoped<IClienteUpdateStrategy, NomeUpdateStrategy>();
builder.Services.AddScoped<IClienteUpdateStrategy, DocumentoUpdateStrategy>();
builder.Services.AddScoped<IClienteUpdateStrategy, EmailUpdateStrategy>();
builder.Services.AddScoped<IClienteUpdateStrategy, TelefoneUpdateStrategy>();
builder.Services.AddScoped<IClienteUpdateStrategy, DataNascimentoUpdateStrategy>();
builder.Services.AddScoped<IClienteUpdateStrategy, InscricaoEstadualUpdateStrategy>();
builder.Services.AddScoped<IClienteUpdateStrategy, IsentoIEUpdateStrategy>();

builder.Services.AddScoped<IClienteUpdateStrategy, CepUpdateStrategy>();
builder.Services.AddScoped<IClienteUpdateStrategy, LogradouroUpdateStrategy>();
builder.Services.AddScoped<IClienteUpdateStrategy, NumeroUpdateStrategy>();
builder.Services.AddScoped<IClienteUpdateStrategy, BairroUpdateStrategy>();
builder.Services.AddScoped<IClienteUpdateStrategy, CidadeUpdateStrategy>();
builder.Services.AddScoped<IClienteUpdateStrategy, EstadoUpdateStrategy>();


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //db.Database.Migrate(); //Dev only
}

//Como é uma api de testes, deixei o swagger em PRD
app.UseSwagger();
app.UseSwaggerUI();
//fim


app.UseAuthorization();
app.MapControllers();
app.Run();

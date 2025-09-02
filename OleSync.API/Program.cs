using OleSync.API.Validators.BoardValidator;
using OleSync.API.Validators.CommitteeValidator;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OleSync.Infrastructure.Persistence.Context;
using System.Reflection;
using OleSync.Application.Boards.Requests;
using OleSync.API.Validators.BoardValidator;
using OleSync.Infrastructure.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<OleSyncContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Infrastructure Layer
InfrastructureServiceRegistration.AddInfrastructureServices(builder.Services, builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Angular dev server
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
// Register MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.RegisterServicesFromAssembly(typeof(CreateBoardCommandRequest).Assembly);
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Enable annotations if using Swashbuckle.AspNetCore.Annotations
    c.EnableAnnotations();

    // Add schema filters if needed
    //c.SchemaFilter<UserDefaultSchemaFilter>();
}); builder.Services.AddHttpContextAccessor();

builder.Services.AddValidatorsFromAssemblyContaining<CreateBoardDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateBoardDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AddBoardMemberDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCommitteeDtoValidator>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();

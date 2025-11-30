using Microsoft.OpenApi;
using Price.Calculator.Contracts.Interface;
using Price.Calculator.Domain;
using Price.Calculator.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Cart API",
        Version = "v1",
        Description = "API for managing prices, discounts and carts"
    });
    c.EnableAnnotations();
});
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddSingleton<IPriceRespository, PriceRespository>();
builder.Services.AddSingleton<ICartRepository, CartRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();

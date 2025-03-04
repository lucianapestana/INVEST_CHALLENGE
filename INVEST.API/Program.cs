using INVEST.API.DATA.Context;
using INVEST.API.Repository;
using INVEST.API.Repository.Interfaces;
using INVEST.API.Service;
using INVEST.API.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InvestContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

#region [ Services ]

builder.Services.AddScoped<IAccountClientService, AccountClientService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();

#endregion [ Services ]

#region [ Repositories ]

builder.Services.AddScoped<IAccountClientRepository, AccountClientRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

#endregion [ Repositories ]

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "INVEST CHALLENGE - API",
        Version = "v1",
        Description = "Projeto API para investimentos"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "INVEST CHALLENGE - API v1");
        c.RoutePrefix = string.Empty;  // Swagger disponível em /, não em /swagger
    });
}

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();

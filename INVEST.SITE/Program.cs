using INVEST.BUSINESSLOGIC.Settings;
using INVEST.SITE.Factory;
using INVEST.SITE.Factory.Interfaces;
using INVEST.SITE.Service;
using INVEST.SITE.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

#region [ Factory ]

builder.Services.AddScoped<IClientFactory, ClientFactory>(); 
builder.Services.AddScoped<IOrderFactory, OrderFactory>(); 
builder.Services.AddScoped<IProductFactory, ProductFactory>();

#endregion [ Factory ]

#region [ Service ]

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRestService, RestService>();

#endregion [ Service ]

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");

app.Run();

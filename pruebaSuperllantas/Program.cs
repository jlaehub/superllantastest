using pruebaSuperllantas.Cruds;
using pruebaSuperllantas.Models;
using pruebaSuperllantas.List;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<genericList<Branch>,branchCrud>();
builder.Services.AddScoped<genericList<Company>, companyCrud>();
builder.Services.AddScoped<genericList<Customer>, CustomerCrud>();
builder.Services.AddScoped<genericList<Product>, productCrud>();
builder.Services.AddScoped<genericList<Sale>, SaleCrud>();
builder.Services.AddScoped<genericList<User>, UserCrud>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

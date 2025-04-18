using SaviourButtigiegEP.DataAccess;
using SaviourButtigiegEP.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using SaviourButtigiegEP.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PollDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddScoped<CommonRepository, PollRepository>(); FOR DATABASE SAVING

builder.Services.AddScoped<CommonRepository, FileRepository>();


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();


var app = builder.Build();

app.UseSession();


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
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();

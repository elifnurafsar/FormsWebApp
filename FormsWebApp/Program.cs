using FormsWebApp.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

///////////////////////////Register database context//////////////////////////////////////////
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("localDatabase")));
///////////////////////////////////////////////////////////////////////////////////////////

// Add services to the container.
builder.Services.AddControllersWithViews();

////////////////////////Auth Requirements/////////////////////
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme).AddCookie( option =>
    {
        option.LoginPath = "/Access/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

////////////////////////End Of Auth Requirements/////////////////////


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

////////////////////////Use Authentication/////////////////////
app.UseAuthentication();
////////////////////////End of Use Authentication/////////////////////

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "register",
    pattern: "{controller=CLogins}/{action=Create}/{id?}");

app.Run();

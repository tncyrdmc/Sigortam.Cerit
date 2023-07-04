using Microsoft.EntityFrameworkCore;
using Sigortam.Cerit.Core.Interfaces;
using Sigortam.Cerit.Core.Services.Insurance;
using Sigortam.Cerit.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Sigortam.Cerit.Data.Entity;
using Sigortam.Cerit.Core.Services.Setting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddAuthentication("YourSchemeName").AddCookie("YourSchemeName",
                config =>
                {
                    config.Cookie.Name = "YourCookieName";
                    config.LoginPath = "/Login/Index";
                });

//builder.Services.AddAuthentication(
//    CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(option =>
//    {
//        option.LoginPath = "/Login/Index";
//        option.LogoutPath = "/Login/Index";
//        option.Cookie.Path = "/";
//        option.Cookie.HttpOnly = true;
//        option.ExpireTimeSpan = TimeSpan.FromMinutes(1);
//        option.SlidingExpiration = true;
//    });
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.User.RequireUniqueEmail = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContextConnection")));
builder.Services.AddScoped<IInsurance, InsuranceService>();
builder.Services.AddScoped<ISetting, SettingService>();
builder.Services.AddMvc();
builder.Services.AddMemoryCache();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else

{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Insurance}/{action=Index}/{id?}");

app.Run();




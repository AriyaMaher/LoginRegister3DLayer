using LoginRegister3DLayer.Core.Interface;
using LoginRegister3DLayer.Core.Services;
using LoginRegister3DLayer.Database.Context;
using Microsoft.CodeAnalysis.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddMvc();

builder.Services.AddScoped<DatabaseContext, DatabaseContext>();
builder.Services.AddScoped<IAccount,AccountService>();

const string scheme = "LoginRegister3DLayer";
builder.Services.AddAuthentication(scheme).AddCookie(scheme, option =>
{
    option.LoginPath = "/home/Login";
    option.AccessDeniedPath = "/home/Login";
    option.ExpireTimeSpan = TimeSpan.FromDays(30);//For LocalHost
});


var app = builder.Build();

app.UseStaticFiles(); //اجاره دسترسی به پوشه ها را میدهد
app.UseRouting(); //ساختار آدرس دهی که پایین تعریف شده است

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=home}/{action=index}/{id?}");

});

app.Run();

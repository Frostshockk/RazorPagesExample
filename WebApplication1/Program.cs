using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Lax;

        options.ExpireTimeSpan = TimeSpan.FromDays(30);
        options.SlidingExpiration = true;
        options.LoginPath = "/Login";
    });

builder.Services.AddScoped<AuthService>();

builder.Services.AddRazorPages(options => {
    options.Conventions.AuthorizeFolder("/");
    options.Conventions.AllowAnonymousToPage("/Login");
    options.Conventions.AllowAnonymousToPage("/Register");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    

    if (!context.BoardTasks.Any())
    {
        context.BoardTasks.AddRange(
            new BoardTask { 
                Title = "Пример задачи", 
                Description = "Описание задачи", 
                Status = "todo", 
                Position = 0 },
            new BoardTask { 
                Title = "Другая задача", 
                Description = "Еще описание", 
                Status = "inprogress", 
                Position = 1 }
            );
    }
    context.SaveChanges();
   
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Use(async (context, next) =>
{
    if (!context.User.Identity.IsAuthenticated &&
        !context.Request.Path.StartsWithSegments("/Login") &&
        !context.Request.Path.StartsWithSegments("/Register"))
    {
        context.Response.Redirect("/Login");
        return;
    }
    await next.Invoke();
});
app.Run();

using System.Globalization;
using Filmora.Data;
using Filmora.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Hãy cấu hình ConnectionStrings:DefaultConnection.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".RapMo.Cart";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddScoped<PosterStorage>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<OrderEmailService>();
var hasSmtpCredentials = !string.IsNullOrWhiteSpace(builder.Configuration["Email:Address"])
    && !string.IsNullOrWhiteSpace(builder.Configuration["Email:AppPassword"]);
if (!builder.Environment.IsDevelopment() || hasSmtpCredentials)
    builder.Services.AddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, IdentityEmailSender>();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
}

// Dấu thập phân của biểu mẫu HTML là dấu chấm. Ngôn ngữ UI vẫn dùng tiếng Việt.
var formatCulture = CultureInfo.GetCultureInfo("en-US");
var uiCulture = CultureInfo.GetCultureInfo("vi-VN");
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(formatCulture, uiCulture),
    SupportedCultures = [formatCulture],
    SupportedUICultures = [uiCulture]
});

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(name: "default", pattern: "{controller=Movies}/{action=Index}/{id?}");
app.MapRazorPages();

if (args.Contains("--prepare-db", StringComparer.Ordinal))
{
    await using var scope = app.Services.CreateAsyncScope();
    await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.MigrateAsync();
    await SampleData.InitializeAsync(scope.ServiceProvider, builder.Configuration);
    Console.WriteLine("Đã cập nhật cơ sở dữ liệu.");
    return;
}

await app.RunAsync();

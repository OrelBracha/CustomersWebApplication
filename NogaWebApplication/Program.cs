using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using NogaWebApplication.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc().AddNewtonsoftJson(options =>
options.SerializerSettings.ContractResolver = new DefaultContractResolver());
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<NogaDbContext>(options =>
options.UseSqlServer(builder.Configuration
.GetConnectionString("NogaWebAppConnectionString")));

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

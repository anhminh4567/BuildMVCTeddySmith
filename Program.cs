using BuildMVCTeddySmith.Controllers;
using BuildMVCTeddySmith.DatabaseStuff;
using BuildMVCTeddySmith.Helpers;
using BuildMVCTeddySmith.Interface;
using BuildMVCTeddySmith.Repo;
using BuildMVCTeddySmith.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// this we inject the whole SERVICE directly instead of going through an interface and repo
builder.Services.AddScoped<RaceRepository>();
builder.Services.AddScoped<IClubRepository,ClubRepository>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
var app = builder.Build();


if (args.Length == 1 && args[0].ToLower()=="seeddata") {
    SeedClass.SeedData(app);
}
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

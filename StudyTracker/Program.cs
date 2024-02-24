using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyTracker.Data;
using StudyTracker.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<StudyTrackerDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.EnableSensitiveDataLogging(); // Enable sensitive data logging
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<StudyTrackerDbContext>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorPages();

builder.Services.AddScoped<AssignmentService>();
builder.Services.AddScoped<CommonServices>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<SubjectService>();
builder.Services.AddScoped<UserService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

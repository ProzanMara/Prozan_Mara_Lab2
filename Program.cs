using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Prozan_Mara_Lab2;
using Prozan_Mara_Lab2.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Prozan_Mara_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Prozan_Mara_Lab2Context' not found.");

builder.Services.AddDbContext<Prozan_Mara_Lab2Context>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddRazorPages();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<Prozan_Mara_Lab2Context>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
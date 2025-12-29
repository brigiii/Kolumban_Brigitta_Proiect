using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Kolumban_Brigitta_Proiect.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Kolumban_Brigitta_ProiectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Kolumban_Brigitta_ProiectContext") ?? throw new InvalidOperationException("Connection string 'Kolumban_Brigitta_ProiectContext' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

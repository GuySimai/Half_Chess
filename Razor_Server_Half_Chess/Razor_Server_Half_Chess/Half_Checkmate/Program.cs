using Microsoft.EntityFrameworkCore;
using Half_Checkmate.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Half_CheckmateContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Half_CheckmateContext") ?? throw new InvalidOperationException("Connection string 'Half_CheckmateContext' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers(); // For the Controllers

app.Run();

using DogalEmlak.Dao;
using DogalEmlak.Service.Abstract;
using DogalEmlak.Service.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//veritaban� ba�lant�s�n� tan�t
builder.Services.AddDbContext<DatabaseContext>();

//addtransient metodu ile servis katman�n� tan�t (transient orta halli)
builder.Services.AddTransient(typeof(IService<>), typeof(Service<>));

//cookie yetkilendirme
builder.Services
	.AddAuthentication("Cookies")
	.AddCookie(opt =>
	{
		opt.Cookie.Name = ".DogalEmlak.WebUI.auth";
		opt.ExpireTimeSpan = TimeSpan.FromDays(1);
		opt.SlidingExpiration = false;
		opt.LoginPath = "/Account/Login";
		opt.LogoutPath = "/Account/Logout";
		opt.AccessDeniedPath = "/Home/AccessDenied";
	});

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

//areas/admin i�in
app.MapControllerRoute(
  name: "admin",
  pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
);

//areas/user i�in
app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

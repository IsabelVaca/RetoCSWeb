using CSweb.Middleware;

var builder = WebApplication.CreateBuilder(args);

// API Flask de perfil (home usa URLs en HomeApiService, puerto 8001).
const string perfilApiUrl = "http://127.0.0.1:8001";

builder.Services.AddControllersWithViews();

// Sesión para el login hardcodeado (sin BD).
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(8);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



builder.Services.AddHttpClient<CSweb.Services.IPerfilApiService, CSweb.Services.PerfilApiService>(client =>
{
    client.BaseAddress = new Uri(perfilApiUrl.TrimEnd('/') + "/");
});

// Solo registro DI; HomeApiService usa URLs absolutas en el puerto 8001.
builder.Services.AddHttpClient<CSweb.Services.IHomeApiService, CSweb.Services.HomeApiService>();

builder.Services.AddHttpClient<CSweb.Services.IExplorarApiService, CSweb.Services.ExplorarApiService>(client =>
{
    client.BaseAddress = new Uri(perfilApiUrl.TrimEnd('/') + "/");
});

builder.Services.AddHttpClient<CSweb.Services.IUsuarioAPIService, CSweb.Services.UsuarioAPIService>(client =>
{
    client.BaseAddress = new Uri(perfilApiUrl.TrimEnd('/') + "/");
});

builder.Services.AddHttpClient<CSweb.Services.IPromptService, CSweb.Services.PromptService>(client =>
{
    client.BaseAddress = new Uri(perfilApiUrl.TrimEnd('/') + "/");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

// Obliga a pasar por Login antes que cualquier otra página.
app.UseMiddleware<RequireLoginMiddleware>();

app.UseAuthorization();

// Ruta por defecto: Login es lo primero al abrir la app.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();

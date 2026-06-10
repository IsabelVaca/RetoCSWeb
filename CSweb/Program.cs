using CSweb.Middleware;
using Microsoft.AspNetCore.StaticFiles;


var builder = WebApplication.CreateBuilder(args);



const string perfilApiUrl = " http://10.14.255.42:8001";



builder.Services.AddControllersWithViews();



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



builder.Services.AddHttpClient<CSweb.Services.ITiendaApiService, CSweb.Services.TiendaApiService>();



var app = builder.Build();



if (!app.Environment.IsDevelopment())

{

    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();

}



app.UseHttpsRedirection();

var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".data"] = "application/octet-stream";
provider.Mappings[".wasm"] = "application/wasm";
app.UseStaticFiles(new StaticFileOptions()
{
ContentTypeProvider = provider,
});

app.UseRouting();

app.UseSession();



app.UseMiddleware<RequireLoginMiddleware>();



app.UseAuthorization();



app.MapControllerRoute(

    name: "default",

    pattern: "{controller=Home}/{action=Login}/{id?}");



app.Run();


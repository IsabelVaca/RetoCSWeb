var builder = WebApplication.CreateBuilder(args);



// API Flask de perfil (base de datos).

const string perfilApiUrl = "http://127.0.0.1:8001";



builder.Services.AddControllersWithViews();

builder.Services.AddSession();



builder.Services.AddHttpClient<CSweb.Services.IPerfilApiService, CSweb.Services.PerfilApiService>(client =>

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

app.UseAuthorization();



app.MapControllerRoute(

    name: "default",

    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();


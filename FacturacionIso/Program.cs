using FacturacionIso.Data;
using FacturacionIso.Services;
using Microsoft.EntityFrameworkCore;
using ServiceReference1;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICedulaValidator, ValCedula>(); // Registro del servicio

// Registro del cliente SOAP
builder.Services.AddScoped<AsientoContableServiceSoapClient>(provider =>
{
    return new AsientoContableServiceSoapClient(AsientoContableServiceSoapClient.EndpointConfiguration.AsientoContableServiceSoap);
});

// Agregar DbContext con MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySQLConnection"),
                     new MySqlServerVersion(new Version(8, 0, 21))));

var app = builder.Build();

// Verificar la conexión a la base de datos
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        // Intenta ejecutar una consulta simple para verificar la conexión
        if (dbContext.Database.CanConnect())
        {
            Console.WriteLine("Conexión a la base de datos exitosa.");
        }
        else
        {
            Console.WriteLine("No se pudo conectar a la base de datos.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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

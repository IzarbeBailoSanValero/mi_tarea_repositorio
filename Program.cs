/*program.cs se encarga de: punto de entrada de la api + iniciar + conectar + inyectar + arrancar*/




//0. archivos usados
using RestauranteAPI.Controllers;
using RestauranteAPI.Repositories;


//1. builder configura la aplicación antes de que empiece a funcionar. 
var builder = WebApplication.CreateBuilder(args);


//2. obtener connectionString definida en appsettings.json
var connectionString = builder.Configuration.GetConnectionString("RestauranteDB");


//3. inyección de dependencias
builder.Services.AddScoped<IPlatoPrincipalRepository, PlatoPrincipalRepository>(provider =>      //addscoped --> define tiempo de servicio --> se crea objeto por cada http
new PlatoPrincipalRepository(connectionString));                                               //cuando algun componente pida esa interfaz --> se creará un objeto de ese tipo
                                                                                               //inyectamos un a instancia de platoPrincipalRepository con la connString



builder.Services.AddScoped<IBebidaRepository, BebidaRepository>(provider =>      //addscoped --> define tiempo de servicio --> se crea objeto por cada http
new BebidaRepository(connectionString));                                               //cuando algun componente pida esa interfaz --> se creará un objeto de ese tipo
                                                                                               //inyectamos un a instancia de platoPrincipalRepository con la connString




builder.Services.AddScoped<IPostreRepository, PostreRepository>(provider =>
    new PostreRepository(connectionString));


//lo comento porque aun no me va elcombo
// builder.Services.AddScoped<IComboRepository, ComboRepository>(provider =>      //addscoped --> define tiempo de servicio --> se crea objeto por cada http
// new ComboRepository(connectionString));                                               //cuando algun componente pida esa interfaz --> se creará un objeto de ese tipo
//                                                                                                //inyectamos un a instancia de platoPrincipalRepository con la connString





//4. configuración de servicios extra
builder.Services.AddControllers();               // Permite usar controladores
builder.Services.AddEndpointsApiExplorer();      // Permite que Swagger detecte los endpoints
builder.Services.AddSwaggerGen();             // Activa Swagger para documentación automática

var app = builder.Build();              //el metodo build crea el objeto final de tipo webAplication con la configuración que he introducido




//5.  middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); //enruta hacia controladores



//6. arranca app
app.Run();

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApiAutores.Controllers;
using WebApiAutores.Filtros;
using WebApiAutores.Middlewares;
using WebApiAutores.Servicios;

namespace WebApiAutores
{
    public class Startup
    {

        public Startup(IConfiguration configuration) {

            Configuration = configuration;
        
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(opciones =>
            {
                opciones.Filters.Add(typeof(FiltrosExcepcion));
            }).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler=ReferenceHandler.IgnoreCycles);

            //services.AddTransient<IServicio, ServicioA>();
            //Transitorio:siempre entrega una nueva instancia :no utiliza estado
            //services.AddTransient<ServicioA>();

            //el tiempo de vida de la clase aumenta devuelve la misma instancia :ejemplo applicatcionDbContext permite trabajar con lo mismo datos
            services.AddTransient<IServicio, ServicioA>();

            services.AddTransient<ServicioTransient>();
            services.AddScoped<ServicioScoped>();
            services.AddSingleton<ServicioSingleton>();
            services.AddTransient<MiFiltroDeAccion>();
            services.AddHostedService<EscribirEnArchivo>();

            services.AddResponseCaching();//Permite usar cache

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();//Authenticacion
            //singleton permite trabajar con data en memoria devuelve la misma data compartida entre todos





            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer( Configuration.GetConnectionString("defaultConnection")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiAutores", Version = "v1" });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILogger<Startup> logger)
        {

            //app.UseMiddleware<LoguearRespuestaHTTPMiddleware>();

            app.UseLoguearRespuestaHTTP();

            app.Map("/ruta1", app =>
            {
                
                app.Run(async contexto =>
                {
                    await contexto.Response.WriteAsync("Estoy interceptando la tuberia");
                });

            });



            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseResponseCaching();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();   
            });


        }
    }
}

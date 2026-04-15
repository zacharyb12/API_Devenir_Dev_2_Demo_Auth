
using Application_Devenir_Dev_2.Services;
using Application_Devenir_Dev_2.Services.Interfaces;
using Domain_Devenir_Dev_2.Interfaces;
using Infrastructure.Devenir_Dev_2.Data;
using Infrastructure.Devenir_Dev_2.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application_Devenir_Dev_2.DTOS;
using Microsoft.AspNetCore.RateLimiting;
namespace API_Devenir_Dev_2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuration----------------------------

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Ajouter CORS
            builder.Services.AddCors(
                options=>
                {
                    options.AddPolicy(name: "localdev",

                        policy =>
                        {
                            policy.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                        });
                }
                
                );

            //Jwt
            builder.Services.AddAuthentication(
                JwtBearerDefaults.AuthenticationScheme
                ).AddJwtBearer(options =>                 
                   options.TokenValidationParameters =
                   new TokenValidationParameters()
                   {
                       //Est-ce que je veux valider qui a construit le token?
                       ValidateIssuer = true,
                       //Est-ce que je veux vérifier pour quel scope, site,..le token est valide ?
                       ValidateAudience = true,
                       //Est-ce que je veux vérifier si le token n'a pas expiré ?
                       ValidateLifetime = true,
                       //Est-ce que je veux valider l'exactitude tu token (a-t-il été modifié avant l'envoir, intercepté?
                       ValidateIssuerSigningKey = true,

                       //configurer les options
                       //Issuer
                       ValidIssuer = builder.Configuration["JWT:Issuer"],
                       //Audience
                       ValidAudience = builder.Configuration["JWT:Audience"],
                       //SigningKey
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:secretKey"]??""))
                   }
                );
             


            // Configuration de Entity Framework Core avec SQL Server
#if DEBUG
            builder.Services.AddDbContext<MovieContext>(options =>
                // fournir la connectionString en paramètre
                options.UseSqlServer(builder.Configuration.GetConnectionString("myConnection"))
            );
#else
            builder.Services.AddDbContext<MovieContext>(options =>
                // fournir la connectionString en paramètre
                options.UseSqlServer(builder.Configuration.GetConnectionString("Prod"))


            );
             
#endif

            // Injection de dépendance
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IMovieRepository , MovieRepository>();
            builder.Services.AddScoped<IUserRepository,UserRepository>();
            builder.Services.AddScoped<IUserService,UserService>();
            builder.Services.AddScoped<IAccountService,AccountService>();


            //Pour le jwt
            //1 - je load les paramètres du settings dans un objet
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));

            // Rate Limit : limiter le nombre de requette pour un delai
            builder.Services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter("fixed", opt =>
                {
                    // Delai en minutes - secondes - etc ...
                    opt.Window = TimeSpan.FromMinutes(1);
                    // nombre de requettes par minutes
                    opt.PermitLimit = 10;
                    // nombre de requettes en attente
                    opt.QueueLimit = 0;
                });
            });


            // construction de l'application 
            var app = builder.Build();

            // Utilisation -----------------------------

            app.UseCors("localdev");


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

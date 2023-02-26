using AutoMapper;
using Org.OpenAPITools.Client;
using RecipePlannerApi.Api;
using RecipePlannerApi.Api.Interface;
using RecipePlannerApi.Controllers;
using RecipePlannerApi.Dao;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Service;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApi
{
    public class Program {

        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddScoped<IUserDao, UserDao>();
            builder.Services.AddScoped<IPantryDao, PantryDao>();
            builder.Services.AddScoped<IIngredientDao, IngredientDao>();
            builder.Services.AddScoped<IAppDao, AppDao>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAppService, AppService>();
            builder.Services.AddScoped<IRecipeService, RecipeService>();
            builder.Services.AddScoped<IMeasurementService, MeasurementService>();

            builder.Services.AddScoped<IRecipeApi, RecipeApi>();

            Configuration.ApiKey.Add("x-api-key", Connection.SpoonacularApiKey);
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
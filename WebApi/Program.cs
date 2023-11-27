using CrealutionServer.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using CrealutionServer.Infrastructure.Repositories;
using CrealutionServer.Infrastructure.Repositories.Interfaces;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Collections.Generic;
using CrealutionServer.Infrastructure.Middlewares;
using CrealutionServer.Configurations.Mapping;

namespace CrealutionServer.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "WebApi.xml");

                options.IncludeXmlComments(xmlPath);
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Crealution API",
                    Description = "Crealution server WebAPI",
                    Contact = new OpenApiContact
                    {
                        Email = builder.Configuration.GetSection("Contacts")["Email"]
                    }
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Enter JWT authorization token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        },
                        new List<string>()
                    }
                });
            });

            builder.Services.AddAutoMapper(typeof(CrealutionMappingProfile));
            builder.Services.AddDbContext<CrealutionDb>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IStatisticTypeRepository, StatisticTypeRepository>();
            
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(builder.Configuration.GetSection("ElsasticSearch")["Url"]))
                {
                    AutoRegisterTemplate = true,
                })
                .CreateLogger();

            builder.Services.AddSingleton(Log.Logger);
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog();
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CrealutionDb>();
                context.Database.Migrate();
            }
            
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.UseMiddleware<CrealutionErrorHandlingMiddleware>();
            app.Run();
        }
    }
}
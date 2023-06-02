using AssetManagementApi.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using System.Text;

namespace AssetManagementApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(System.String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AssetManagmentContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DbConnection"))
            );

            services.AddControllers();
            services.AddScoped<AssetManagmentContext>();
			
			services.AddScoped<IUser, UserRepository>();
			services.AddScoped<IAssign, AssetRepository>();
            
            services.AddScoped<IBook, BookRepository>();
			services.AddScoped<IHardware, HardwareRepository>();
			services.AddScoped<ISoftware, SoftwareRepository>();
			services.AddSingleton<ILog, LogNLog>();
			
			//

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = true,
						ValidIssuer = Configuration["Jwt:Issuer"],
						ValidAudience = Configuration["Jwt:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))

					};
				});

			services.AddSwaggerGen(option =>
			{
				option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
				});
				option.AddSecurityRequirement(new OpenApiSecurityRequirement
					 {
						 {
							   new OpenApiSecurityScheme
								 {
									 Reference = new OpenApiReference
									 {
										 Type = ReferenceType.SecurityScheme,
										 Id = "Bearer"
									 }
								 },
								 new string[] {}
						 }
					 });

			});


			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "ASSET MANAGEMENT SYSTEM", Version = "v1" });
			});
		}

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
			app.UseAuthentication();
            app.UseAuthorization(); 
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Asset Management System");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });

        }
    }
}

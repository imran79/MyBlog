namespace User.Api
{
	using AutoMapper;
	using FluentValidation.AspNetCore;
	using MediatR;
	using Microsoft.AspNetCore.Authentication.JwtBearer;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Filters;
	using Microsoft.AspNetCore.ResponseCompression;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using Microsoft.IdentityModel.Tokens;
	using Microsoft.OpenApi.Models;
	using System;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using System.Runtime.CompilerServices;
	using System.Text;
	using User.Data.Database;
	using User.Domain.Entities;
	using User.Messaging.Sender.Options.v1;
	using User.Service.v1.Command;
	using User.Service.v1.Query;

	/// <summary>
	/// Defines the <see cref="Startup" />.
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Startup"/> class.
		/// </summary>
		/// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// Gets the Configuration.
		/// </summary>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// The ConfigureServices.
		/// </summary>
		/// <param name="services">The services<see cref="IServiceCollection"/>.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddOptions();

			services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMq"));

			services.AddDbContext<UserContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BloggigUserDatabase")));



			services.AddIdentity<IdentityUser, IdentityRole>()
				.AddEntityFrameworkStores<UserContext>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(options =>
					{
						options.TokenValidationParameters = new TokenValidationParameters
						{
							ValidateIssuer = true,
							ValidateAudience = true,
							ValidateLifetime = true,
							ValidateIssuerSigningKey = true,
							ValidIssuer = Configuration["JwtIssuer"],
							ValidAudience = Configuration["JwtAudience"],
							IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"]))
						};
					});

			services.AddMvc().AddNewtonsoftJson();
			services.AddResponseCompression(opts =>
			{
				opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
					new[] { "application/octet-stream" });
			});

			services.AddAutoMapper(typeof(Startup));

			services.AddMvc().AddFluentValidation();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "User Api",
					Description = "A simple API to create or pay users",
					Contact = new OpenApiContact
					{
						Name = "Imran khan",
						Email = "khanimran79@gmail.com",
						Url = new Uri("https://xyz.com/")
					}
				});

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = actionContext =>
				{
					var actionExecutingContext =
						actionContext as ActionExecutingContext;

					if (actionContext.ModelState.ErrorCount > 0
						&& actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
					{
						return new UnprocessableEntityObjectResult(actionContext.ModelState);
					}

					return new BadRequestObjectResult(actionContext.ModelState);
				};
			});



			services.AddTransient<IRequestHandler<GetAllUsersQuery, ConfiguredCancelableAsyncEnumerable<UserEntity>>, GetAllUsersQueryHandler>();
			services.AddTransient<IRequestHandler<GetUserByIdQuery, UserEntity>, GetUserByIdQueryHandler>();
			services.AddTransient<IRequestHandler<CreateUserCommand, UserEntity>, CreateUserCommandHandler>();
			services.AddTransient<IRequestHandler<UpdateUserCommand, UserEntity>, UpdateUserCommandHandler>();
		}

		/// <summary>
		/// The Configure.
		/// </summary>
		/// <param name="app">The app<see cref="IApplicationBuilder"/>.</param>
		/// <param name="env">The env<see cref="IWebHostEnvironment"/>.</param>
		/// <param name="db">The db<see cref="UserContext"/>.</param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserContext db)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}
			db.Database.EnsureCreated();
			db.Database.Migrate();
		//	app.UseHttpsRedirection();
			app.UseSwagger();
			app.UseCors(x => x
   .AllowAnyMethod()
   .AllowAnyHeader()
   .SetIsOriginAllowed(origin => true) // allow any origin  
   .AllowCredentials());

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "User API V1");
				c.RoutePrefix = string.Empty;
			});
			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}

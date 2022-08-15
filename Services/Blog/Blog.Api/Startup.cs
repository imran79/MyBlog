using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AutoMapper;
using Blog.Api.Models.v1;
using Blog.Api.Validators.v1;
using Blog.Data.Database;
using Blog.Data.Entities;
using Blog.Data.Repository.v1;
using Blog.Service.v1.Blogs.Query;
using Blog.Service.v1.Command;
using Blog.Service.v1.Query;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Blog.Messaging.Receive.Receiver.v1;
using Blog.Service.v1.Services;
using Blog.Messaging.Receive.Options.v1;
using OpenIddict.Validation.AspNetCore;

namespace Blog.Api {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services) {
            services.AddOptions ();

            services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMq"));

            services.AddDbContext<BlogContext> (options => options.UseSqlServer (Configuration.GetConnectionString ("BloggingDatabase")));

            services.AddAutoMapper (typeof (Startup));

            services.AddMvc ().AddFluentValidation ();

            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new OpenApiInfo {
                    Version = "v1",
                        Title = "Blog Api",
                        Description = "A simple API to create or update blogs :)",
                        Contact = new OpenApiContact {
                            Name = "Imran",
                                Email = "imran.lko@outlook.com",
                                Url = new Uri ("https://www.myblog.com/")
                        }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine (AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments (xmlPath);
            });

            services.Configure<ApiBehaviorOptions> (options => {
                options.InvalidModelStateResponseFactory = actionContext => {
                    var actionExecutingContext =
                        actionContext as ActionExecutingContext;

                    if (actionContext.ModelState.ErrorCount > 0 &&
                        actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count) {
                        return new UnprocessableEntityObjectResult (actionContext.ModelState);
                    }

                    return new BadRequestObjectResult (actionContext.ModelState);
                };
            });

            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(IBlogAuthorUpdateService).Assembly);

            services.AddTransient(typeof (IRepository<>), typeof (Repository<>));
            services.AddTransient<IBlogRepository, BlogRepository> ();
            services.AddTransient<IPostRepository, PostRepository> ();
            services.AddTransient<IBlogAuthorRepository, BlogAuthorRepository>();


            services.AddTransient<IValidator<CreateBlogModel>, CreateBlogModelValidator> ();
            services.AddTransient<IValidator<UpdateBlogModel>, UpdateBlogModelValidator> ();

            

            services.AddTransient<IRequestHandler<CreateBlogCommand, BlogEntity>, CreateBlogCommandHandler> ();
            services.AddTransient<IRequestHandler<UpdateBlogCommand, BlogEntity>, UpdateBlogCommandHandler> ();
            services.AddTransient<IRequestHandler<GetBlogByIdQuery, BlogEntity>, GetBlogByIdQueryHandler> ();
            services.AddTransient<IRequestHandler<GetBlogsQuery, List<BlogEntity>>, GetBlogsQueryHandler > ();
            services.AddTransient<IRequestHandler<UpsertBlogAuthorCommand, BlogAuthor>, UpsertBlogAuthorCommandHandler>();

            services.AddTransient<IRequestHandler<CreatePostCommand, Post>, CreatePostCommandHandler> ();
            services.AddTransient<IRequestHandler<UpdatePostCommand, Post>, UpdatePostCommandHandler> ();
            services.AddTransient<IRequestHandler<GetPostByIdQuery, Post>, GetPostByIdQueryHandler> ();
            services.AddTransient<IRequestHandler<GetPostsByBlogIdQuery, List<Post>>, GetPostsByBlogIdQueryHandler> ();
            services.AddTransient<IBlogAuthorUpdateService, BlogAuthorUpdateService>();

            services.AddHostedService<BlogAuthorUpdateReceiver>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
            });

            // Register the OpenIddict validation components.
            services.AddOpenIddict()
                .AddValidation(options =>
                {
                    // Note: the validation handler uses OpenID Connect discovery
                    // to retrieve the address of the introspection endpoint.
                    options.SetIssuer("http://localhost:4002/");
                    options.AddAudiences("resource_server_1");

                    // Configure the validation handler to use introspection and register the client
                    // credentials used when communicating with the remote introspection endpoint.
                    options.UseIntrospection()
                           .SetClientId("resource_server_1")
                           .SetClientSecret("846B62D0-DEF9-4215-A99D-86E6B8DAB342");

                    // Register the System.Net.Http integration.
                    options.UseSystemNetHttp();

                    // Register the ASP.NET Core host.
                    options.UseAspNetCore();
                });         
        
            services.AddControllersWithViews();
        }

        public void Configure (IApplicationBuilder app, IWebHostEnvironment env, BlogContext db) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseHsts ();
            }
            db.Database.EnsureCreated();
            db.Database.Migrate();
           // app.UseHttpsRedirection ();
            app.UseSwagger ();
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "BLOG API V1");
                c.RoutePrefix = string.Empty;
            });

         

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });

            app.UseRouting ();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}
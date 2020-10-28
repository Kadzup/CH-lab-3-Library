using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Library
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async (context) =>
                {
                    await HTMLBuilder.WriteUserssPage(context);
                });

                endpoints.MapGet("/users/", async (context) =>
                {
                    await HTMLBuilder.WriteUserssPage(context);
                });

                endpoints.MapGet("/books/", async (context) =>
                {
                    await HTMLBuilder.WriteBooksPage(context);
                });

                endpoints.MapGet("/orders/", async (context) =>
                {
                    await HTMLBuilder.WriteOrdersPage(context);
                });
            });
        }
    }
}

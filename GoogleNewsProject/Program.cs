using GoogleNewsProject;
using DAL_;

namespace GoogleNewsProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Register IMemoryCache
            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<DAL_.NewsService>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //****
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();

            }

            app.UseHttpsRedirection();

            // using html files
            app.UseDefaultFiles();
            app.UseStaticFiles();

            //****
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();


            app.Run();
        }
    }
}

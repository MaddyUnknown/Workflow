using Workflow.API.Application.Extentions;
using Workflow.API.Infrastructure.Application.Extentions;
using Workflow.API.Infrastructure.DataAccess.Extentions;
using Workflow.API.Infrastructure.Web.Extentions;

namespace Workflow.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration; // application configuration

            // Add application services
            builder.Services.AddApplicationServices();
            
            builder.Services.AddWebInfrastructureService(config);
            builder.Services.AddApplicationInfrastructureServices();
            builder.Services.AddDataAccessInfrastructureService();


            var app = builder.Build();
            app.ConfigureHttpRequestPipeline();

            app.Run();
        }
    }
}
using Dapper;
using Workflow.API.Core.Interfaces.Repositories;
using Workflow.API.Infrastructure.DataAccess.Mappers;
using Workflow.API.Infrastructure.DataAccess.Repositories;

namespace Workflow.API.Infrastructure.DataAccess.Extentions
{
    public static class DataAccessInfraExtention
    {
        public static void AddDataAccessInfrastructureService(this IServiceCollection services)
        {
            SqlMapper.AddTypeHandler<DateOnly?>(new NullableDateOnlyMapper());

            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<ITokenRepository, TokenRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

        }
    }
}

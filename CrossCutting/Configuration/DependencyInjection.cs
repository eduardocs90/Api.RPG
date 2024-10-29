using Domain.Authentication;
using Domain.Entities;
using Domain.Repositories;
using InfraData.Authentication;
using InfraData.Context;
using InfraData.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Interfaces;
using Service.Mapping;
using Service.Services;

namespace CrossCutting.Configuration
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastruture(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContextDb>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<IEquipmentRepository,EquipmentRepository>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IDecodeToken, DecodeToken>();

            /*
                        services.AddHangfire(config =>
                        {
                            string connectionString = configuration.GetConnectionString("DefaultConnection");
                            config.UsePostgreSqlStorage(connectionString);
                        });*/
            return services;





        }
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<IEquipmentService,  EquipmentService>();


            services.AddAutoMapper(typeof(DtoToDomain));
            services.AddAutoMapper(typeof(DomainToDto));

            return services;
        }

    }
}

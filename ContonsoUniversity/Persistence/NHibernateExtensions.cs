using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using System;
using NHibernate.Driver;
using Persistence.Mappings;

namespace Persistence
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
        {  
            var sessionFactory = CreateSessionFactory(connectionString);

            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());

            return services;
        }

        private static ISessionFactory CreateSessionFactory(string connectionString)
        {
            //return Fluently.Configure()
            //  .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
            //  .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(NHibernateExtensions).Assembly))
            //  .ExposeConfiguration(BuildSchema)
            //  .BuildSessionFactory();

            ModelMapper modelMapper = new ModelMapper();
            modelMapper.AddMappings(typeof(CourseMap).Assembly.GetTypes());

            var config = new Configuration();
            config.DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2012Dialect>();
                db.ConnectionString = connectionString;
                db.ConnectionReleaseMode =
                ConnectionReleaseMode.OnClose;
                db.LogSqlInConsole = true;
                db.LogFormattedSql = true;
                db.SchemaAction = SchemaAutoAction.Recreate;
            })
            .AddMapping(modelMapper.CompileMappingForAllExplicitlyAddedEntities());

            var sessionFactory = config.BuildSessionFactory();
            return sessionFactory;
        }

        private static void BuildSchema(Configuration config)
        {
            new SchemaExport(config).Drop(false, true);
            new SchemaUpdate(config).Execute(false, true);
        }
    }
}

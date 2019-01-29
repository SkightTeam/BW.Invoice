using System;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using Invoice.Domain;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Invoice.Persistent.NHibernate
{
    public class SessionProvider
    { 
        public static ISessionFactory Initilize()
        {
            var assembly = Assembly.GetAssembly(typeof(Organization));
            var fluentConfig = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile("Data/Invoice.sqlite").ShowSql)
                .Mappings(m =>
                {
                    m.AutoMappings.Add(
                            AutoMap
                                .Assemblies(new MyAutomappingConfig(), assembly)
                                .Conventions.Add<CascadConvention>())
                        ;
                    m.FluentMappings.AddFromAssembly(assembly);

                })
                .ExposeConfiguration(x => x.SetProperty("expiration", "900"))
                ;


            if (!Directory.Exists("Data"))
            {
                Directory.CreateDirectory("Data");
            }

            fluentConfig.ExposeConfiguration(x =>
                new SchemaExport(x).SetOutputFile("Data/TableScript.sql")
                    .Execute(true, true, false));


             return fluentConfig.BuildSessionFactory();
        }

       

        public class CascadConvention : IHasManyConvention
        {
            public void Apply(IOneToManyCollectionInstance instance)
            {
                instance.Cascade.All();
            }
        }
        public class MyAutomappingConfig : DefaultAutomappingConfiguration
        {
            public override bool ShouldMap(Type type)
            {
                if (type != null && type.Namespace != null)
                {
                    return Enumerable.Contains(type.Namespace.Split("."), "Domain");
                }
                return false;
            }
        }
    }
}
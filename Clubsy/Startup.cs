using Clubsy.Migrations;
using Microsoft.Owin;
using Owin;
using System.Data.Entity.Migrations;

[assembly: OwinStartupAttribute(typeof(Clubsy.Startup))]
namespace Clubsy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // Uncomment this when debugging seeding data, to be able to debug in the same VS instance
            //Configuration configuration = new Configuration();
            //DbMigrator dbm = new DbMigrator(configuration);
            //dbm.Update();
        }
    }
}

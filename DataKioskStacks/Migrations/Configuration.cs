using DataKioskStacks.DataManager;

namespace DataKioskStacks.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed partial class Configuration : DbMigrationsConfiguration<DataKioskEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataKioskEntities context)
        {
            ProcessSeed(context);
        }
    }
}

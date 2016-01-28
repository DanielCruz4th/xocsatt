namespace XOcsatt.Entities.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<XOcsatt.Entities.BackupMachineContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(XOcsatt.Entities.BackupMachineContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.BackupMachines.AddOrUpdate(
                new BackupMachine
                {
                    ID = Guid.NewGuid(),
                    IPAddress = "192.168.1.2",
                    SourcePath = "C:\\Backup\\",
                    SourceUserName = "dcru2",
                    SourcePassword = "set4Now&ChangeLater",
                    DestinationPath = "\\\\Backup",
                    DestinationUserName = "dcru2",
                    DestinationPassword = "set4Now&ChangeLater",
                    Enabled = true,
                    CreatedBy = "dcru2",
                    DateCreated = DateTime.UtcNow
                },
                new BackupMachine
                {
                    ID = Guid.NewGuid(),
                    IPAddress = "192.168.1.3",
                    SourcePath = "C:\\Backup\\",
                    SourceUserName = "dcru2",
                    SourcePassword = "set4Now&ChangeLater",
                    DestinationPath = "\\\\Backup",
                    DestinationUserName = "dcru2",
                    DestinationPassword = "set4Now&ChangeLater",
                    Enabled = true,
                    CreatedBy = "dcru2",
                    DateCreated = DateTime.UtcNow
                },
                new BackupMachine
                {
                    ID = Guid.NewGuid(),
                    IPAddress = "192.168.1.4",
                    SourcePath = "C:\\Backup\\",
                    SourceUserName = "dcru2",
                    SourcePassword = "set4Now&ChangeLater",
                    DestinationPath = "\\\\Backup",
                    DestinationUserName = "dcru2",
                    DestinationPassword = "set4Now&ChangeLater",
                    Enabled = true,
                    CreatedBy = "dcru2",
                    DateCreated = DateTime.UtcNow
                }
            );
        }
    }
}

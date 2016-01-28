namespace XOcsatt.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Creatingbackupmachine : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BackupMachines",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        IPAddress = c.String(),
                        SourcePath = c.String(),
                        SourceUserName = c.String(),
                        SourcePassword = c.String(),
                        DestinationPath = c.String(),
                        DestinationUserName = c.String(),
                        DestinationPassword = c.String(),
                        Enabled = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastUpdated = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BackupMachines");
        }
    }
}

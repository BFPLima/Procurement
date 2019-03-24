namespace Procurement.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUNSPSCAndDescriptionDirectlyToItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "UNSPSC", c => c.String());
            AddColumn("dbo.Items", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "Description");
            DropColumn("dbo.Items", "UNSPSC");
        }
    }
}

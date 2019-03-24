namespace Procurement.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminInfoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastName = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        UserType = c.Int(nullable: false),
                        LegalPersonality = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AttributeValues",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Item_ID = c.Int(),
                        TemplateAttribute_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Items", t => t.Item_ID)
                .ForeignKey("dbo.TemplateAttributes", t => t.TemplateAttribute_ID)
                .Index(t => t.Item_ID)
                .Index(t => t.TemplateAttribute_ID);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        Template_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TemplateItems", t => t.Template_ID)
                .Index(t => t.Template_ID);
            
            CreateTable(
                "dbo.TemplateItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ItemType_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ItemTypes", t => t.ItemType_ID)
                .Index(t => t.ItemType_ID);
            
            CreateTable(
                "dbo.ItemTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TemplateAttributes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Order = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        TemplateItem_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TemplateItems", t => t.TemplateItem_ID)
                .Index(t => t.TemplateItem_ID);
            
            CreateTable(
                "dbo.CustumerInfoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.CustumerOrderItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DesiredDateDelivery = c.DateTime(nullable: false),
                        Quantity = c.Double(nullable: false),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CustumerOrder_ID = c.Int(),
                        Item_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CustumerOrders", t => t.CustumerOrder_ID)
                .ForeignKey("dbo.Items", t => t.Item_ID)
                .Index(t => t.CustumerOrder_ID)
                .Index(t => t.Item_ID);
            
            CreateTable(
                "dbo.CustumerOrders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CustumerInfo_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CustumerInfoes", t => t.CustumerInfo_ID)
                .Index(t => t.CustumerInfo_ID);
            
            CreateTable(
                "dbo.SupplierInfoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.SupplierOffers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        ProvidedDateDelivery = c.DateTime(nullable: false),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Item_ID = c.Int(),
                        SupplierInfo_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Items", t => t.Item_ID)
                .ForeignKey("dbo.SupplierInfoes", t => t.SupplierInfo_ID)
                .Index(t => t.Item_ID)
                .Index(t => t.SupplierInfo_ID);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        RoleID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleID, t.UserID })
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.RoleID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SupplierInfoes", "User_ID", "dbo.Users");
            DropForeignKey("dbo.SupplierOffers", "SupplierInfo_ID", "dbo.SupplierInfoes");
            DropForeignKey("dbo.SupplierOffers", "Item_ID", "dbo.Items");
            DropForeignKey("dbo.CustumerOrderItems", "Item_ID", "dbo.Items");
            DropForeignKey("dbo.CustumerOrderItems", "CustumerOrder_ID", "dbo.CustumerOrders");
            DropForeignKey("dbo.CustumerOrders", "CustumerInfo_ID", "dbo.CustumerInfoes");
            DropForeignKey("dbo.CustumerInfoes", "User_ID", "dbo.Users");
            DropForeignKey("dbo.AttributeValues", "TemplateAttribute_ID", "dbo.TemplateAttributes");
            DropForeignKey("dbo.Items", "Template_ID", "dbo.TemplateItems");
            DropForeignKey("dbo.TemplateAttributes", "TemplateItem_ID", "dbo.TemplateItems");
            DropForeignKey("dbo.TemplateItems", "ItemType_ID", "dbo.ItemTypes");
            DropForeignKey("dbo.AttributeValues", "Item_ID", "dbo.Items");
            DropForeignKey("dbo.AdminInfoes", "User_ID", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "UserID", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "RoleID", "dbo.Roles");
            DropIndex("dbo.RoleUsers", new[] { "UserID" });
            DropIndex("dbo.RoleUsers", new[] { "RoleID" });
            DropIndex("dbo.SupplierOffers", new[] { "SupplierInfo_ID" });
            DropIndex("dbo.SupplierOffers", new[] { "Item_ID" });
            DropIndex("dbo.SupplierInfoes", new[] { "User_ID" });
            DropIndex("dbo.CustumerOrders", new[] { "CustumerInfo_ID" });
            DropIndex("dbo.CustumerOrderItems", new[] { "Item_ID" });
            DropIndex("dbo.CustumerOrderItems", new[] { "CustumerOrder_ID" });
            DropIndex("dbo.CustumerInfoes", new[] { "User_ID" });
            DropIndex("dbo.TemplateAttributes", new[] { "TemplateItem_ID" });
            DropIndex("dbo.TemplateItems", new[] { "ItemType_ID" });
            DropIndex("dbo.Items", new[] { "Template_ID" });
            DropIndex("dbo.AttributeValues", new[] { "TemplateAttribute_ID" });
            DropIndex("dbo.AttributeValues", new[] { "Item_ID" });
            DropIndex("dbo.AdminInfoes", new[] { "User_ID" });
            DropTable("dbo.RoleUsers");
            DropTable("dbo.SupplierOffers");
            DropTable("dbo.SupplierInfoes");
            DropTable("dbo.CustumerOrders");
            DropTable("dbo.CustumerOrderItems");
            DropTable("dbo.CustumerInfoes");
            DropTable("dbo.TemplateAttributes");
            DropTable("dbo.ItemTypes");
            DropTable("dbo.TemplateItems");
            DropTable("dbo.Items");
            DropTable("dbo.AttributeValues");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.AdminInfoes");
        }
    }
}

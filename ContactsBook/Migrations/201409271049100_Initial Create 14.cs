namespace ContactsBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate14 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contacts", "GroupId", "dbo.ContactGroups");
            DropForeignKey("dbo.Contacts", "UserId", "dbo.Users");
            DropIndex("dbo.Contacts", new[] { "GroupId" });
            DropIndex("dbo.Contacts", new[] { "UserId" });
            AlterColumn("dbo.Contacts", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Contacts", "GroupId", c => c.Int(nullable: false));
            CreateIndex("dbo.Contacts", "GroupId");
            CreateIndex("dbo.Contacts", "UserId");
            AddForeignKey("dbo.Contacts", "GroupId", "dbo.ContactGroups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Contacts", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Contacts", "GroupId", "dbo.ContactGroups");
            DropIndex("dbo.Contacts", new[] { "UserId" });
            DropIndex("dbo.Contacts", new[] { "GroupId" });
            AlterColumn("dbo.Contacts", "GroupId", c => c.Int());
            AlterColumn("dbo.Contacts", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Contacts", "UserId");
            CreateIndex("dbo.Contacts", "GroupId");
            AddForeignKey("dbo.Contacts", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Contacts", "GroupId", "dbo.ContactGroups", "Id");
        }
    }
}

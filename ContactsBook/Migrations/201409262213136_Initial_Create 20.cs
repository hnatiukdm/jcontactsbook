namespace ContactsBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Create20 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contacts", "UserId", "dbo.Users");
            DropIndex("dbo.Contacts", new[] { "UserId" });
            AlterColumn("dbo.Contacts", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Contacts", "UserId");
            AddForeignKey("dbo.Contacts", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "UserId", "dbo.Users");
            DropIndex("dbo.Contacts", new[] { "UserId" });
            AlterColumn("dbo.Contacts", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Contacts", "UserId");
            AddForeignKey("dbo.Contacts", "UserId", "dbo.Users", "Id");
        }
    }
}

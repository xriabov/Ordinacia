namespace Ordinacia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocReference : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Users", "User_UserId", "Users");
            DropIndex("Users", new[] { "User_UserId" });
            AddColumn("InWs", "Doc_UserId", c => c.Int());
            AddColumn("Pharms", "Doc_UserId", c => c.Int());
            CreateIndex("InWs", "Doc_UserId");
            CreateIndex("Pharms", "Doc_UserId");
            AddForeignKey("InWs", "Doc_UserId", "Users", "UserId");
            AddForeignKey("Pharms", "Doc_UserId", "Users", "UserId");
            DropColumn("Users", "User_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "User_UserId", c => c.Int());
            DropForeignKey("dbo.Pharms", "Doc_UserId", "dbo.Users");
            DropForeignKey("dbo.InWs", "Doc_UserId", "dbo.Users");
            DropIndex("dbo.Pharms", new[] { "Doc_UserId" });
            DropIndex("dbo.InWs", new[] { "Doc_UserId" });
            DropColumn("dbo.Pharms", "Doc_UserId");
            DropColumn("dbo.InWs", "Doc_UserId");
            CreateIndex("dbo.Users", "User_UserId");
            AddForeignKey("dbo.Users", "User_UserId", "dbo.Users", "UserId");
        }
    }
}

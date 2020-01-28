namespace Ordinacia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Specification");
            DropColumn("dbo.Users", "Employer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Employer", c => c.String(unicode: false));
            AddColumn("dbo.Users", "Specification", c => c.String(unicode: false));
        }
    }
}

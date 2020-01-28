namespace Ordinacia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubFields : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                        Height = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        InsuranceID = c.Int(nullable: false),
                        DoctorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PatientID);
            
            AddColumn("dbo.Users", "Specification", c => c.String(unicode: false));
            AddColumn("dbo.Users", "Employer", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Employer");
            DropColumn("dbo.Users", "Specification");
            DropTable("dbo.Patients");
        }
    }
}

namespace Ordinacia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class A : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Docs",
                c => new
                    {
                        DocID = c.Int(nullable: false, identity: true),
                        Specialization = c.String(unicode: false),
                        RefUser_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.DocID)
                .ForeignKey("dbo.Users", t => t.RefUser_UserId)
                .Index(t => t.RefUser_UserId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                        Height = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        InsuranceName = c.String(unicode: false),
                        Doctor_DocID = c.Int(),
                    })
                .PrimaryKey(t => t.PatientID)
                .ForeignKey("dbo.Docs", t => t.Doctor_DocID)
                .Index(t => t.Doctor_DocID);
            
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        MedicineID = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        PharmacyName = c.String(unicode: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.MedicineID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(unicode: false),
                        UserPassword = c.String(unicode: false),
                        FirstName = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.InWs",
                c => new
                    {
                        InWID = c.Int(nullable: false, identity: true),
                        InsuranceName = c.String(unicode: false),
                        RefUser_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.InWID)
                .ForeignKey("dbo.Users", t => t.RefUser_UserId)
                .Index(t => t.RefUser_UserId);
            
            CreateTable(
                "dbo.Pharms",
                c => new
                    {
                        PharmID = c.Int(nullable: false, identity: true),
                        Pharmacy = c.String(unicode: false),
                        RefUser_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.PharmID)
                .ForeignKey("dbo.Users", t => t.RefUser_UserId)
                .Index(t => t.RefUser_UserId);
            
            CreateTable(
                "dbo.MedicinePatients",
                c => new
                    {
                        Medicine_MedicineID = c.Int(nullable: false),
                        Patient_PatientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Medicine_MedicineID, t.Patient_PatientID })
                .ForeignKey("dbo.Medicines", t => t.Medicine_MedicineID, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.Patient_PatientID, cascadeDelete: true)
                .Index(t => t.Medicine_MedicineID)
                .Index(t => t.Patient_PatientID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.RoleID })
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pharms", "RefUser_UserId", "dbo.Users");
            DropForeignKey("dbo.InWs", "RefUser_UserId", "dbo.Users");
            DropForeignKey("dbo.Docs", "RefUser_UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "UserID", "dbo.Users");
            DropForeignKey("dbo.MedicinePatients", "Patient_PatientID", "dbo.Patients");
            DropForeignKey("dbo.MedicinePatients", "Medicine_MedicineID", "dbo.Medicines");
            DropForeignKey("dbo.Patients", "Doctor_DocID", "dbo.Docs");
            DropIndex("dbo.UserRoles", new[] { "RoleID" });
            DropIndex("dbo.UserRoles", new[] { "UserID" });
            DropIndex("dbo.MedicinePatients", new[] { "Patient_PatientID" });
            DropIndex("dbo.MedicinePatients", new[] { "Medicine_MedicineID" });
            DropIndex("dbo.Pharms", new[] { "RefUser_UserId" });
            DropIndex("dbo.InWs", new[] { "RefUser_UserId" });
            DropIndex("dbo.Patients", new[] { "Doctor_DocID" });
            DropIndex("dbo.Docs", new[] { "RefUser_UserId" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.MedicinePatients");
            DropTable("dbo.Pharms");
            DropTable("dbo.InWs");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Medicines");
            DropTable("dbo.Patients");
            DropTable("dbo.Docs");
        }
    }
}

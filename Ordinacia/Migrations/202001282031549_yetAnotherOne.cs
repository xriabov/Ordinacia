namespace Ordinacia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yetAnotherOne : DbMigration
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
                "dbo.InWs",
                c => new
                    {
                        InWID = c.Int(nullable: false, identity: true),
                        nsuranceName = c.String(unicode: false),
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
            
            AddColumn("dbo.Patients", "InsuranceName", c => c.Int(nullable: false));
            AddColumn("dbo.Patients", "Doctor_UserId", c => c.Int());
            AddColumn("dbo.Patients", "Doc_DocID", c => c.Int());
            CreateIndex("dbo.Patients", "Doctor_UserId");
            CreateIndex("dbo.Patients", "Doc_DocID");
            AddForeignKey("dbo.Patients", "Doctor_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Patients", "Doc_DocID", "dbo.Docs", "DocID");
            DropColumn("dbo.Patients", "InsuranceID");
            DropColumn("dbo.Patients", "DoctorID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "DoctorID", c => c.Int(nullable: false));
            AddColumn("dbo.Patients", "InsuranceID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Pharms", "RefUser_UserId", "dbo.Users");
            DropForeignKey("dbo.InWs", "RefUser_UserId", "dbo.Users");
            DropForeignKey("dbo.Docs", "RefUser_UserId", "dbo.Users");
            DropForeignKey("dbo.Patients", "Doc_DocID", "dbo.Docs");
            DropForeignKey("dbo.MedicinePatients", "Patient_PatientID", "dbo.Patients");
            DropForeignKey("dbo.MedicinePatients", "Medicine_MedicineID", "dbo.Medicines");
            DropForeignKey("dbo.Patients", "Doctor_UserId", "dbo.Users");
            DropIndex("dbo.MedicinePatients", new[] { "Patient_PatientID" });
            DropIndex("dbo.MedicinePatients", new[] { "Medicine_MedicineID" });
            DropIndex("dbo.Pharms", new[] { "RefUser_UserId" });
            DropIndex("dbo.InWs", new[] { "RefUser_UserId" });
            DropIndex("dbo.Patients", new[] { "Doc_DocID" });
            DropIndex("dbo.Patients", new[] { "Doctor_UserId" });
            DropIndex("dbo.Docs", new[] { "RefUser_UserId" });
            DropColumn("dbo.Patients", "Doc_DocID");
            DropColumn("dbo.Patients", "Doctor_UserId");
            DropColumn("dbo.Patients", "InsuranceName");
            DropTable("dbo.MedicinePatients");
            DropTable("dbo.Pharms");
            DropTable("dbo.InWs");
            DropTable("dbo.Medicines");
            DropTable("dbo.Docs");
        }
    }
}

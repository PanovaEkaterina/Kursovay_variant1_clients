namespace BeautySaloonService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdminFIO = c.String(nullable: false),
                        AdminLogin = c.String(nullable: false),
                        AdminPassword = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KlientId = c.Int(nullable: false),
                        ProcedureId = c.Int(nullable: false),
                        MasterId = c.Int(),
                        AdminId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: true)
                .ForeignKey("dbo.Klients", t => t.KlientId, cascadeDelete: true)
                .ForeignKey("dbo.Masters", t => t.MasterId)
                .ForeignKey("dbo.Procedures", t => t.ProcedureId, cascadeDelete: true)
                .Index(t => t.KlientId)
                .Index(t => t.ProcedureId)
                .Index(t => t.MasterId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.Klients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KlientFIO = c.String(nullable: false),
                        KlientLogin = c.String(nullable: false),
                        KlientPassword = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Masters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MasterFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Procedures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcedureName = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProcedureMaterials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcedureId = c.Int(nullable: false),
                        MaterialId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .ForeignKey("dbo.Procedures", t => t.ProcedureId, cascadeDelete: true)
                .Index(t => t.ProcedureId)
                .Index(t => t.MaterialId);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaterialName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "ProcedureId", "dbo.Procedures");
            DropForeignKey("dbo.ProcedureMaterials", "ProcedureId", "dbo.Procedures");
            DropForeignKey("dbo.ProcedureMaterials", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.Requests", "MasterId", "dbo.Masters");
            DropForeignKey("dbo.Requests", "KlientId", "dbo.Klients");
            DropForeignKey("dbo.Requests", "AdminId", "dbo.Admins");
            DropIndex("dbo.ProcedureMaterials", new[] { "MaterialId" });
            DropIndex("dbo.ProcedureMaterials", new[] { "ProcedureId" });
            DropIndex("dbo.Requests", new[] { "AdminId" });
            DropIndex("dbo.Requests", new[] { "MasterId" });
            DropIndex("dbo.Requests", new[] { "ProcedureId" });
            DropIndex("dbo.Requests", new[] { "KlientId" });
            DropTable("dbo.Materials");
            DropTable("dbo.ProcedureMaterials");
            DropTable("dbo.Procedures");
            DropTable("dbo.Masters");
            DropTable("dbo.Klients");
            DropTable("dbo.Requests");
            DropTable("dbo.Admins");
        }
    }
}

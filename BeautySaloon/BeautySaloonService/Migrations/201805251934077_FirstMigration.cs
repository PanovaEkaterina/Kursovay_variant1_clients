namespace BeautySaloonService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Klients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KlientFIO = c.String(nullable: false),
                        Mail = c.String(),
                        KlientPassword = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KlientId = c.Int(nullable: false),
                        ZakazId = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumPay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateVisit = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Klients", t => t.KlientId, cascadeDelete: true)
                .ForeignKey("dbo.Zakazs", t => t.ZakazId, cascadeDelete: true)
                .Index(t => t.KlientId)
                .Index(t => t.ZakazId);
            
            CreateTable(
                "dbo.Zakazs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZakazName = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ZakazProcedures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZakazId = c.Int(nullable: false),
                        ProcedureId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Procedures", t => t.ProcedureId, cascadeDelete: true)
                .ForeignKey("dbo.Zakazs", t => t.ZakazId, cascadeDelete: true)
                .Index(t => t.ZakazId)
                .Index(t => t.ProcedureId);
            
            CreateTable(
                "dbo.Procedures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcedureName = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ZakazProcedures", "ZakazId", "dbo.Zakazs");
            DropForeignKey("dbo.ZakazProcedures", "ProcedureId", "dbo.Procedures");
            DropForeignKey("dbo.Requests", "ZakazId", "dbo.Zakazs");
            DropForeignKey("dbo.Requests", "KlientId", "dbo.Klients");
            DropIndex("dbo.ZakazProcedures", new[] { "ProcedureId" });
            DropIndex("dbo.ZakazProcedures", new[] { "ZakazId" });
            DropIndex("dbo.Requests", new[] { "ZakazId" });
            DropIndex("dbo.Requests", new[] { "KlientId" });
            DropTable("dbo.Procedures");
            DropTable("dbo.ZakazProcedures");
            DropTable("dbo.Zakazs");
            DropTable("dbo.Requests");
            DropTable("dbo.Klients");
        }
    }
}

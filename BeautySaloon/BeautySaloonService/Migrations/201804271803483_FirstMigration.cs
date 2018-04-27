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
                        Email = c.String(nullable: false),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KlientId = c.Int(nullable: false),
                        RequestProcedureId = c.Int(nullable: false),
                        PaymentId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Summ = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Klients", t => t.KlientId, cascadeDelete: true)
                .ForeignKey("dbo.Payments", t => t.PaymentId, cascadeDelete: true)
                .ForeignKey("dbo.RequestProcedures", t => t.RequestProcedureId, cascadeDelete: true)
                .Index(t => t.KlientId)
                .Index(t => t.RequestProcedureId)
                .Index(t => t.PaymentId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Summ = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RequestProcedures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcedureId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Procedures", t => t.ProcedureId, cascadeDelete: true)
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
            DropForeignKey("dbo.Requests", "RequestProcedureId", "dbo.RequestProcedures");
            DropForeignKey("dbo.RequestProcedures", "ProcedureId", "dbo.Procedures");
            DropForeignKey("dbo.Requests", "PaymentId", "dbo.Payments");
            DropForeignKey("dbo.Requests", "KlientId", "dbo.Klients");
            DropIndex("dbo.RequestProcedures", new[] { "ProcedureId" });
            DropIndex("dbo.Requests", new[] { "PaymentId" });
            DropIndex("dbo.Requests", new[] { "RequestProcedureId" });
            DropIndex("dbo.Requests", new[] { "KlientId" });
            DropTable("dbo.Procedures");
            DropTable("dbo.RequestProcedures");
            DropTable("dbo.Payments");
            DropTable("dbo.Requests");
            DropTable("dbo.Klients");
        }
    }
}

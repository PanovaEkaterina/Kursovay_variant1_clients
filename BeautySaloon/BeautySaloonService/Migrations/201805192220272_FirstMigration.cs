namespace BeautySaloonService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProcedureMaterials", "Count", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProcedureMaterials", "Count", c => c.Int(nullable: false));
        }
    }
}

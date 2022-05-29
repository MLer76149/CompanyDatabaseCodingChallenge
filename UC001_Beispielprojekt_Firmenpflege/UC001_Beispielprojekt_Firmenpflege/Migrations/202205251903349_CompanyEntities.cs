namespace UC001_Beispielprojekt_Firmenpflege.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompanyModels",
                c => new
                    {
                        CompanyNo = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false),
                        Industry = c.String(nullable: false),
                        AmountEmployees = c.Int(nullable: false),
                        City = c.String(nullable: false),
                        Holding = c.String(nullable: false),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyNo);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CompanyModels");
        }
    }
}

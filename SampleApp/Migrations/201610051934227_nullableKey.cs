namespace SampleApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableKey : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.DataSets", new[] { "ParentDataSet_Id" });
            AlterColumn("dbo.DataSets", "ParentDataSet_Id", c => c.Int());
            CreateIndex("dbo.DataSets", "ParentDataSet_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DataSets", new[] { "ParentDataSet_Id" });
            AlterColumn("dbo.DataSets", "ParentDataSet_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.DataSets", "ParentDataSet_Id");
        }
    }
}

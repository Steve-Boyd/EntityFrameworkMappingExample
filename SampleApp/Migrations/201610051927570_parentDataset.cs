namespace SampleApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class parentDataset : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DataSets", "ParentDataSet_Id", c => c.Int(nullable: true));
            CreateIndex("dbo.DataSets", "ParentDataSet_Id");
            AddForeignKey("dbo.DataSets", "ParentDataSet_Id", "dbo.DataSets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DataSets", "ParentDataSet_Id", "dbo.DataSets");
            DropIndex("dbo.DataSets", new[] { "ParentDataSet_Id" });
            DropColumn("dbo.DataSets", "ParentDataSet_Id");
        }
    }
}

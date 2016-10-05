namespace SampleApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class t : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DataSets", "ParentDataSet_Id", "dbo.DataSets");
            DropIndex("dbo.DataSets", new[] { "ParentDataSet_Id" });
            DropColumn("dbo.DataSets", "ParentDataSet_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DataSets", "ParentDataSet_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.DataSets", "ParentDataSet_Id");
            AddForeignKey("dbo.DataSets", "ParentDataSet_Id", "dbo.DataSets", "Id");
        }
    }
}

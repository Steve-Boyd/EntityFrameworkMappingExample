namespace SampleApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialnew : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DataSetPortfolioMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Portfolio_Id = c.Int(nullable: false),
                        DataSet_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DataSets", t => t.DataSet_Id, cascadeDelete: true)
                .ForeignKey("dbo.Portfolios", t => t.Portfolio_Id, cascadeDelete: true)
                .Index(t => t.Portfolio_Id)
                .Index(t => t.DataSet_Id);

            CreateTable(
                "dbo.DataSets",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    IsActive = c.Boolean(nullable: false),
                    ParentDataSet_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);
              
            
            CreateTable(
                "dbo.Portfolios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PortfolioProgramMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Program_Id = c.Int(nullable: false),
                        DataSet_Id = c.Int(nullable: false),
                        DataSetPortfolioMap_Id = c.Int(nullable: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DataSets", t => t.DataSet_Id, cascadeDelete: true)
                .ForeignKey("dbo.Programs", t => t.Program_Id, cascadeDelete: true)
                .ForeignKey("dbo.DataSetPortfolioMaps", t => t.DataSetPortfolioMap_Id, cascadeDelete: false)
                .Index(t => t.Program_Id)
                .Index(t => t.DataSet_Id)
                .Index(t => t.DataSetPortfolioMap_Id);
            
            CreateTable(
                "dbo.Programs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProgramProjectMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Project_Id = c.Int(nullable: false),
                        DataSet_Id = c.Int(nullable: false),
                        PortfolioProgramMap_Id = c.Int(nullable: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DataSets", t => t.DataSet_Id, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.Project_Id, cascadeDelete: true)
                .ForeignKey("dbo.PortfolioProgramMaps", t => t.PortfolioProgramMap_Id, cascadeDelete: false)
                .Index(t => t.Project_Id)
                .Index(t => t.DataSet_Id)
                .Index(t => t.PortfolioProgramMap_Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PortfolioProgramMaps", "DataSetPortfolioMap_Id", "dbo.DataSetPortfolioMaps");
            DropForeignKey("dbo.ProgramProjectMaps", "PortfolioProgramMap_Id", "dbo.PortfolioProgramMaps");
            DropForeignKey("dbo.ProgramProjectMaps", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.ProgramProjectMaps", "DataSet_Id", "dbo.DataSets");
            DropForeignKey("dbo.PortfolioProgramMaps", "Program_Id", "dbo.Programs");
            DropForeignKey("dbo.PortfolioProgramMaps", "DataSet_Id", "dbo.DataSets");
            DropForeignKey("dbo.DataSetPortfolioMaps", "Portfolio_Id", "dbo.Portfolios");
            
            DropForeignKey("dbo.DataSetPortfolioMaps", "DataSet_Id", "dbo.DataSets");
            DropIndex("dbo.ProgramProjectMaps", new[] { "PortfolioProgramMap_Id" });
            DropIndex("dbo.ProgramProjectMaps", new[] { "DataSet_Id" });
            DropIndex("dbo.ProgramProjectMaps", new[] { "Project_Id" });
            DropIndex("dbo.PortfolioProgramMaps", new[] { "DataSetPortfolioMap_Id" });
            DropIndex("dbo.PortfolioProgramMaps", new[] { "DataSet_Id" });
            DropIndex("dbo.PortfolioProgramMaps", new[] { "Program_Id" });
            
            DropIndex("dbo.DataSetPortfolioMaps", new[] { "DataSet_Id" });
            DropIndex("dbo.DataSetPortfolioMaps", new[] { "Portfolio_Id" });
            DropTable("dbo.Projects");
            DropTable("dbo.ProgramProjectMaps");
            DropTable("dbo.Programs");
            DropTable("dbo.PortfolioProgramMaps");
            DropTable("dbo.Portfolios");
            DropTable("dbo.DataSets");
            DropTable("dbo.DataSetPortfolioMaps");
        }
    }
}

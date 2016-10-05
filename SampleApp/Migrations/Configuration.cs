using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace SampleApp.Migrations
{
    public class DataContext : DbContext
    {
        public DbSet<DataSet> DataSets { get; set; }
        public DbSet<DataSetPortfolioMap> DataSetPortfolioMaps { get; set; }
        public DbSet<PortfolioProgramMap> PortfolioProgramMaps { get; set; }
        public DbSet<ProgramProjectMap> ProgramProjectMaps { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DataSet>()
                .HasMany(ds => ds.DataSetPortfolioMaps);
                
            modelBuilder.Entity<DataSetPortfolioMap>()
                .HasMany(dsp => dsp.PortfolioProgramMaps);

            modelBuilder.Entity<PortfolioProgramMap>()
                .HasMany(ppm => ppm.ProgramProjectMaps);

    //        modelBuilder.Entity<StagingArea>()
    //.HasMany<Authentication.ApplicationUser>(d => d.Users)
    //.WithMany(p => p.StagingAreas)
    //.Map(pd =>
    //{
    //    pd.MapLeftKey("StagingAreaId");
    //    pd.MapRightKey("UserId");
    //    pd.ToTable("StagingAreaUser");
    //});
        }

        //public DataContext()
        //{
        //    Configuration.ProxyCreationEnabled = true;
        //    Configuration.LazyLoadingEnabled = true;
        //}
    }

    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        public static DataContext Create()
        {
            return new DataContext();
        }
        

        protected override void Seed(DataContext context)
        {
            var dataSet = new DataSet() {IsActive = true};

            var project = new Project() { Name = "Project 1" };
            var project2 = new Project() { Name = "Project 2" };

            var programProjectMap = new ProgramProjectMap() { Project = project, DataSet = dataSet };
            var programProjectMap2 = new ProgramProjectMap() { Project = project2, DataSet = dataSet };

            var program = new Program() { Name = "Program 1" };

            var portfolioProgram = new PortfolioProgramMap() { Program = program, ProgramProjectMaps = new List<ProgramProjectMap>() { programProjectMap, programProjectMap2 }, DataSet = dataSet };

            var portfolio = new Portfolio() { Name = "Portfolio 1" };

            var dataSetPortfolioMap = new DataSetPortfolioMap() { Portfolio = portfolio, PortfolioProgramMaps = new List<PortfolioProgramMap>() { portfolioProgram }, DataSet = dataSet };

            //var programProjectMap = new ProgramProjectMap() { Project = project };
            //var programProjectMap2 = new ProgramProjectMap() { Project = project2 };

            //var program = new Program() { Name = "Program 1" };

            //var portfolioProgram = new PortfolioProgramMap() { Program = program, ProgramProjectMaps = new List<ProgramProjectMap>() { programProjectMap, programProjectMap2 } };

            //var portfolio = new Portfolio() { Name = "Portfolio 1" };

            //var dataSetPortfolioMap = new DataSetPortfolioMap() { Portfolio = portfolio, PortfolioProgramMaps = new List<PortfolioProgramMap>() { portfolioProgram } };

            dataSet.DataSetPortfolioMaps = new List<DataSetPortfolioMap> { dataSetPortfolioMap };

            context.DataSets.AddOrUpdate(dataSet);
        }
    }
}

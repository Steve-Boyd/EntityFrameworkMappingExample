using System;
using System.Linq;
using DataContext = SampleApp.Migrations.DataContext;

//using System.Data.Linq;

namespace SampleApp
{
    class ConsoleProgram
    {
        static void Main(string[] args)
        {
            // copy prod DS
            var dataSetId = CreateNewDataSet();

            // Add Data
            var projectId = AddProjectToNewDataSet(dataSetId);

            // Remove Data
            RemoveProjectFromNewDataSet(dataSetId, projectId);

            Console.ReadLine();
        }

        private static void RemoveProjectFromNewDataSet(int dataSetId, int projectId)
        {
            // remove everything but the given project
            using (var db = new DataContext())
            {
                var db2 = new DataContext();

                var projectsToDelete = db.ProgramProjectMaps.Where(p => p.Project.Id != projectId && p.DataSet_Id == dataSetId);

                db.ProgramProjectMaps.RemoveRange(projectsToDelete);
                db.SaveChanges();
                //db.Dispose();
            }
        }

        private static int AddProjectToNewDataSet(int dataSetId)
        {
            // first program and portfolio in the dataSet will be used as project parent for demo purposes. These ids will be needed for actual implementation.
            using (var db = new DataContext())
            {
                var db2 = new DataContext();

                var clone = db.DataSets
                    .AsNoTracking()
                    .FirstOrDefault(ds => ds.Id == dataSetId);

                var dataSetPortfolioMaps = clone.DataSetPortfolioMaps.FirstOrDefault();
                var ppm = dataSetPortfolioMaps.PortfolioProgramMaps.FirstOrDefault();
                
                var project = new Project() {Name="Steve's awesome program!"};
                var programProjectMap = new ProgramProjectMap() {DataSet_Id = dataSetId, PortfolioProgramMap_Id = ppm.Id, Project = project};
                
                db2.ProgramProjectMaps.Add(programProjectMap);
                db2.SaveChanges();
                db2.Dispose();

                return project.Id;
            }
        }


        /// <summary>
        /// Copy current dataset for new stage
        /// </summary>
        private static int CreateNewDataSet()
        {

            using (var db = new DataContext())
            {
                var db2 = new DataContext();

                var clone = db.DataSets
                    .AsNoTracking()
                    //.Include(ds => ds.DataSetPortfolioMaps.Select(pm => pm.PortfolioProgramMaps.Select(a => a.ProgramProjectMaps.Select(d => d.PortfolioProgramMap))))
                    .FirstOrDefault(ds => ds.IsActive);

                var dataSetPortfolioMaps = clone.DataSetPortfolioMaps.FirstOrDefault();
                var ppm = dataSetPortfolioMaps.PortfolioProgramMaps.FirstOrDefault();
                var pm = ppm.ProgramProjectMaps.FirstOrDefault();

                clone.IsActive = false;
                var parentId = clone.Id;
                
                db2.DataSets.Add(clone);
                db2.SaveChanges();
                clone.ParentDataSet_Id = parentId;
                db2.SaveChanges();
                db2.Dispose();

                return clone.Id;
            }
        }
        
   
   
    }
}

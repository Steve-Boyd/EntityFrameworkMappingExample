using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleApp
{
 public class Portfolio
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Program
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DataSet
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int? ParentDataSet_Id { get; set; }
        [ForeignKey("ParentDataSet_Id")]
        public virtual DataSet ParentDataSet { get; set; }
        public virtual List<DataSetPortfolioMap> DataSetPortfolioMaps { get; set; }

        
    }
    public class DataSetPortfolioMap
    {
        public int Id { get; set; }

        [ForeignKey("Portfolio_Id")]
        public virtual Portfolio Portfolio { get; set; }

        public int Portfolio_Id { get; set; }
        public int DataSet_Id { get; set; }

        [ForeignKey("DataSet_Id")]
        public virtual DataSet  DataSet{ get; set; }
        public virtual List<PortfolioProgramMap> PortfolioProgramMaps { get; set; }
    }
    public class PortfolioProgramMap
    {
        public int Id { get; set; }

        public int Program_Id { get; set; }
        public int DataSet_Id { get; set; }
        public int DataSetPortfolioMap_Id { get; set; }

        [ForeignKey("Program_Id")]
        public virtual Program Program { get; set; }
        [ForeignKey("DataSet_Id")]
        public virtual DataSet DataSet { get; set; }
        [ForeignKey("DataSetPortfolioMap_Id")]
        public virtual DataSetPortfolioMap DataSetPortfolioMap { get; set; }
        public virtual List<ProgramProjectMap> ProgramProjectMaps { get; set; }
    }
    public class ProgramProjectMap
    {
        public int Id { get; set; }
        public int Project_Id { get; set; }
        public int DataSet_Id { get; set; }
        public int PortfolioProgramMap_Id { get; set; }
        [ForeignKey("Project_Id")]
        public virtual Project Project { get; set; }
        [ForeignKey("DataSet_Id")]
        public virtual DataSet DataSet { get; set; }
        [ForeignKey("PortfolioProgramMap_Id")]
        public virtual PortfolioProgramMap PortfolioProgramMap { get; set; }
    }
}

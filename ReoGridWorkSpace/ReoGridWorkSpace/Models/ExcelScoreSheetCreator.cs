using ReoGridWorkSpace.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReoGridWorkSpace.Models
{
  public class ExcelScoreSheetCreator : IScoreSheetCreator
  {

    private readonly string _excelFilePath;

    public DataTable CreateDataSource()
    {
      try
      {
        return new DataTable();
      }
      catch
      {
        throw;
      }
    }



  }
}

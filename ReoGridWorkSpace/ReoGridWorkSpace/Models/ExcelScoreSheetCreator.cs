using MiniExcelLibs;
using ReoGridWorkSpace.Entity;
using ReoGridWorkSpace.Interface;
using ReoGridWorkSpace.Utility;
using System.Data;
using System.IO;

namespace ReoGridWorkSpace.Models
{
  public class ExcelScoreSheetCreator : IScoreSheetCreator
  {

    private readonly string _excelFilePath = Path.Combine("ExcelSampleData", "01_サンプル成績.xlsx");

    public DataTable CreateDataSource()
    {
      try
      {
        // Excelファイルまでのパスを取得
        var path = PathUtility.CombineWithAppPath(_excelFilePath);
        // ExcelをIEnumerable<T>で読み込み
        var rows = MiniExcel.Query<ScoreEntity>(path);

        // 列定義
        var dt = new DataTable();
        dt.Columns.Add("出席番号", typeof(int));
        dt.Columns.Add("氏名", typeof(string));
        dt.Columns.Add("国語", typeof(int));
        dt.Columns.Add("数学", typeof(int));
        dt.Columns.Add("英語", typeof(int));
        dt.Columns.Add("化学", typeof(int));
        dt.Columns.Add("物理", typeof(int));
        dt.Columns.Add("日本史", typeof(int));
        dt.Columns.Add("世界史", typeof(int));
        dt.Columns.Add("保健", typeof(int));
        dt.Columns.Add("倫理", typeof(int));

        foreach (var item in rows)
        {
          DataRow row = dt.NewRow();
          row["出席番号"] = item.Id;
          row["氏名"] = item.Name;
          row["国語"] = item.Japanese;
          row["数学"] = item.Math;
          row["英語"] = item.English;
          row["化学"] = item.Chemistry;
          row["物理"] = item.Physics;
          row["日本史"] = item.JapanHistory;
          row["世界史"] = item.WorldHistory;
          row["保健"] = item.Health;
          row["倫理"] = item.Ethics;
          dt.Rows.Add(row);
        }

        return dt;
      }
      catch
      {
        throw;
      }
    }



  }
}

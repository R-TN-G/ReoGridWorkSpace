using CommunityToolkit.Mvvm.ComponentModel;
using ReoGridWorkSpace.Event;
using ReoGridWorkSpace.Interface;
using ReoGridWorkSpace.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unvell.ReoGrid;
using unvell.ReoGrid.DataFormat;
using unvell.ReoGrid.Graphics;

namespace ReoGridWorkSpace.ViewModels
{
  public partial class ReoGridSheetScoreViewModel : ObservableObject, IReoGridSheetViewModel
  {

    [ObservableProperty]
    private DataTable? _reoGridTable;
    
    
    partial void OnReoGridTableChanged(DataTable? dt)
    {

    }

    public event EventHandler<CellChangedEventArgs>? CellDataChanged;

    public void RaiseCellDataChanged(CellChangedEventArgs args)
    {
      CellDataChanged?.Invoke(this, args);
    }

    private IScoreSheetCreator _scoreSheetCreator;

    public ReoGridSheetScoreViewModel(IScoreSheetCreator scoreSheetCreator)
    {
      // シート内容作成クラス取得
      _scoreSheetCreator = scoreSheetCreator;
      ReoGridTable = new DataTable();
      // ReoGridの中身作成
      ReoGridTable = _scoreSheetCreator.CreateDataSource();
    }

    /// <summary>
    /// 成績表reoGridの詳細カスタマイズ
    /// </summary>
    public Action<Worksheet, DataTable>? ConfigureSheet => (sheet, dt) =>
    {
      try
      {
        // 以下は各ViewModel側で設定することにする
        int rowCount = dt.Rows.Count;

        // 計算に使う列のリスト
        Dictionary<int, string> calcColumnDic = new Dictionary<int, string>();
        // 日付型の列リスト
        Dictionary<int, string> dateColumnDic = new Dictionary<int, string>();

        // ReoGridの列設定変更
        foreach (DataColumn col in dt.Columns)
        {
          if (string.IsNullOrWhiteSpace(col?.ColumnName))
            continue;

          // ドキュメントにあるExtensionColumnHeaderがSheetから取れないっぽかったので、
          // 以下(ColumnHeader)で代用
          var targetCellHeader = sheet.GetColumnHeader(col.Ordinal);

          // DataTableの列名をReoGridに反映する
          targetCellHeader.Text = col.ColumnName;


          // DataTableのColumn数値からExcelのColumn(Aなど)に変換
          var colInfo = ReoGridUtility.NumberToDictionary(col.Ordinal);

          // ひとまず汎用化は考えず、型ごとに書式を決定する
          if (col.DataType == typeof(int))
          {
            // 出席番号列はIDなので計算値には使用しない
            if (col.ColumnName == "出席番号")
              continue;
            // 平均点計算式に用いる列を取得
            calcColumnDic.Add(colInfo.First().Key, colInfo.First().Value);
          }
          else if(col.DataType == typeof(DateTime))
          {
            dateColumnDic.Add(colInfo.First().Key, colInfo.First().Value);
          }

        }

        // 書式の設定

        foreach(var calcCol in calcColumnDic)
        {
          // 数値型は小数点以下1桁まで表示
          sheet.SetRangeDataFormat($"{calcCol.Value}1:{calcCol.Value}{rowCount}",
                                   CellDataFormatFlag.Number,
                                   new NumberDataFormatter.NumberFormatArgs
                                   {
                                     DecimalPlaces = 1
                                   });
        }
        
        foreach (var dateCol in dateColumnDic)
        {
          // 日付型は「yyyy年MM月dd日」まで表示
          sheet.SetRangeDataFormat($"{dateCol.Value}1:{dateCol.Value}{rowCount}",
                                   CellDataFormatFlag.DateTime,
                                   new DateTimeDataFormatter.DateTimeFormatArgs
                                   {
                                     CultureName = "jp",
                                     Format = "yyyy年MM月dd日"
                                   });
          //sheet.SetColumnsWidth(dateCol.Key, 1, 200);
          sheet.SetColumnsWidth(dateCol.Key, 1, 120);
        }

        // 平均点行作成
        int avarageRowIndex = dt.Rows.Count;
        foreach (var targetCol in calcColumnDic)
        {
          // 各列の最終行に平均点計算ロジックを組む
          sheet.SetCellFormula(avarageRowIndex, targetCol.Key, $"AVERAGE({targetCol.Value}1:{targetCol.Value}100)");
        }


        // 総合点列作成
        int goukeiColumnNo = dt.Columns.Count;
        sheet.GetColumnHeader(goukeiColumnNo).Text = "総合点";
        int index = 1;
        string goukeiColumnName = ReoGridUtility.NumberToAlphabet(goukeiColumnNo);
        // 各列の最終行に総合点計算ロジックを組む
        foreach (DataRow row in dt.Rows)
        {
          // 総合点列
          sheet[goukeiColumnName + index] = SetSumFormula(calcColumnDic.Values.ToList(), index);
          index++;
        }

        // 数式の再計算(反映)
        sheet.Recalculate();


        // 各セルの値が平均点を下回っていたら赤色に、
        // 　　　　　　　　　　上回っていたら青色にする（文字色）
        // v3.3.0には存在していない模様
        // なので実施するには、手動で設定する必要がある
        // rowIndexが0始まりなので-1
        var lastRow = sheet.UsedRange.Rows - 1; 
        for (int row = 0; row < sheet.UsedRange.Rows - 1; row++)
        {
          if(row == lastRow)
            continue;
          foreach(var calcCol in calcColumnDic)
          {
            var cellValue = Convert.ToInt32(dt.Rows[row][calcCol.Key]);
            var avarageValue = sheet.Cells[lastRow, calcCol.Key].GetData<int>();
            SolidColor color = new SolidColor();
            // 平均点以下なら赤色に
            if (cellValue <= avarageValue)
              color = SolidColor.LightCoral;
            else
              color = SolidColor.SeaGreen;
            sheet.SetRangeStyles(row, calcCol.Key, 1, 1, new WorksheetRangeStyle
            {
              Flag = PlainStyleFlag.TextColor,
              TextColor = color
            });
          }
        }


        // 数式の再計算(反映)
        sheet.Recalculate();

      }
      catch
      {
        throw;
      }
    };

    /// <summary>
    /// 総合点列作成
    /// </summary>
    /// <param name="sumFormula"></param>
    /// <param name="rowNumber"></param>
    /// <returns></returns>
    private string SetSumFormula(List<string> sumFormula, int? rowNumber = null)
    {
      StringBuilder formula = new StringBuilder();

      string a = "INDIRECT(\"";
      string b = "\" & ROW())";

      int loopCount = 0;
      foreach (var ColumnID in sumFormula)
      {
        if (loopCount == 0)
        {
          formula.Append("=(");
        }
        else
        {
          formula.Append("+");
        }
        loopCount++;

        // 数式前半部分使用
        formula.Append(a);

        // 対象セル
        formula.Append(ColumnID);

        // 数式後半部分使用
        formula.Append(b);

      }
      formula.Append("\")");

      return formula.ToString();
    }


    /// <summary>
    /// 平均点列作成
    /// </summary>
    /// <param name="avarageFormula"></param>
    /// <param name="rowNumber"></param>
    /// <returns></returns>
    private string SetAvarageFormula(string targetColumnName, int? avarageRowIndex = null)
    {
      StringBuilder formula = new StringBuilder();

      string a = "INDIRECT(\"";
      string b = "\" & ROW())";
      int startIndex = 1;

      for(int i = startIndex; i < avarageRowIndex; i++)
      {
        if (i == startIndex)
        {
          formula.Append("=(");
        }
        else
        {
          formula.Append("+");
        }

        // 数式前半部分使用
        formula.Append(a);

        // 対象セル
        formula.Append($"{targetColumnName}{i}");

        // 数式後半部分使用
        formula.Append(b);
      }

      formula.Append("\")");
      // 対象行数で割る
      formula.Append("/");
      formula.Append(avarageRowIndex - 1);


      return formula.ToString();

    }



    ///// <summary>
    ///// 平均点列作成
    ///// </summary>
    ///// <param name="avarageFormula"></param>
    ///// <param name="rowNumber"></param>
    ///// <returns></returns>
    //private string SetAvarageFormula(List<string> avarageFormula, int? rowNumber = null)
    //{
    //  StringBuilder formula = new StringBuilder();

    //  string a = "INDIRECT(\"";
    //  string b = "\" & ROW())";

    //  int loopCount = 0;
    //  foreach (var ColumnID in avarageFormula)
    //  {
    //    if (loopCount == 0)
    //    {
    //      formula.Append("=(");
    //    }
    //    else
    //    {
    //      formula.Append("+");
    //    }
    //    loopCount++;

    //    // 数式前半部分使用
    //    formula.Append(a);

    //    // 対象セル
    //    formula.Append(ColumnID);

    //    // 数式後半部分使用
    //    formula.Append(b);

    //  }
    //  formula.Append("\")");
    //  // 対象行数で割る
    //  formula.Append("/");
    //  formula.Append(avarageFormula.Count);


    //  return formula.ToString();
    //}


  }
}

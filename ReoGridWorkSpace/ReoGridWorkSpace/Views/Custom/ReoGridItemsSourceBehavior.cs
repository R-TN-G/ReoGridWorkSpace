using ReoGridWorkSpace.Interface;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using unvell.ReoGrid;
using unvell.ReoGrid.DataFormat;

namespace ReoGridWorkSpace.Views.Custom
{
  /// <summary>
  /// ReoGridにItemsSource的なプロパティがなさそうなので、共通用ビヘイビアで代用
  /// </summary>
  public static class ReoGridItemsSourceBehavior 
  {
    public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.RegisterAttached
     (
       "ItemsSource",
       typeof(IReoGridSheetViewModel),
       typeof(ReoGridItemsSourceBehavior),
       new PropertyMetadata(null, OnItemsSourceChanged)
     );

    private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is ReoGridControl grid && e.NewValue is IReoGridSheetViewModel vm)
      {
        vm.PropertyChanged += (s, args) =>
        {
          if (args.PropertyName == nameof(IReoGridSheetViewModel.ReoGridTable))
            BindDataTable(grid, vm.ReoGridTable);
        };
        BindDataTable(grid, vm.ReoGridTable);
      }
    }

    private static void BindDataTable(ReoGridControl grid, DataTable? dt)
    {
      if (dt == null) return;
      var sheet = grid.CurrentWorksheet;
      sheet.Reset();
      sheet["A1"] = dt;

      int rowCount = dt.Rows.Count;

      List<string> avarageFormula = new List<string>();

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


        // ひとまず汎用化は考えず、型ごとに書式を決定する
        // 数値型には「.x」までの小数点を適用
        if (col.DataType == typeof(int))
        {

          
          // 行番号に「0（1行目）」を指定してアドレス "C1" を取得
          string address = new CellPosition(0, col.Ordinal).ToAddress();

          // 正規表現で末尾の数字「1」を消去して "C" にする
          string colLetter = System.Text.RegularExpressions.Regex.Replace(address, @"\d", "");

          avarageFormula.Add(colLetter);

        }
      }

      // 小数点以下2桁まで表示
      sheet.SetRangeDataFormat("C1:L100", 
                               CellDataFormatFlag.Number , 
                               new NumberDataFormatter.NumberFormatArgs
      {
        DecimalPlaces = 1
      });



      int colCount = dt.Columns.Count;
      sheet.GetColumnHeader(colCount).Text = "平均点";

      int index = 1;
      // 平均点列
      foreach (DataRow row in dt.Rows)
      {
        sheet["L" + index] = SetAvarageFormula(avarageFormula, index);

        //sheet["L" + index] = "=(INDIRECT(\"C\" & ROW()) + INDIRECT(\"D\" & ROW()) + INDIRECT(\"E\" & ROW()) + INDIRECT(\"F\" & ROW()) + INDIRECT(\"G\" & ROW()) + INDIRECT(\"H\" & ROW()) + INDIRECT(\"I\" & ROW()) + INDIRECT(\"J\" & ROW()) + INDIRECT(\"K\" & ROW())\")/9";
        index++;
      }
    }

    public static void SetItemsSource(DependencyObject obj, IEnumerable value)
    {
      obj.SetValue(ItemsSourceProperty, value);
    }

    public static IEnumerable GetValue(DependencyObject obj)
    {
      return (IEnumerable)obj.GetValue(ItemsSourceProperty);
    }

    private static string SetAvarageFormula(List<string> avarageFormula, int? rowNumber = null)
    {
      StringBuilder formula = new StringBuilder();

      string a = "INDIRECT(\"";
      string b = "\" & ROW())";

      int loopCount = 0;
      foreach (var ColumnID in avarageFormula)
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
      // 対象行数で割る
      formula.Append("/");
      formula.Append(avarageFormula.Count);
     

      return formula.ToString();
    }

    private static string NumberToAlphabet(int colNo)
    {
      // 行番号に「0（1行目）」を指定してアドレス "C1" を取得
      string address = new CellPosition(0, colNo).ToAddress();

      // 正規表現で末尾の数字「1」を消去して "C" にする
      string colLetter = System.Text.RegularExpressions.Regex.Replace(address, @"\d", "");

      return colLetter;
    }

  }

  /// <summary>
  /// DataGrid→ReoGrid用のマッピング
  /// </summary>
  public static class DataTabeReoGridMap
  {
    public static string MapToReoGrid(int columnNumber)
    {
      try
      {


        return "";
      }
      catch
      {
        throw;
      }
    }
  }
}

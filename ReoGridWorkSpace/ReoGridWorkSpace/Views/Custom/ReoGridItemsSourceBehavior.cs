using ReoGridWorkSpace.Event;
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
        // データソース変更→画面への変更通知
        vm.PropertyChanged += (s, args) =>
        {
          if (args.PropertyName == nameof(IReoGridSheetViewModel.ReoGridTable))
            BindDataTable(grid, vm);
        };

        BindDataTable(grid, vm);

        // セル入力→ViewModelへの変更通知
        grid.CurrentWorksheet.CellDataChanged += (s, args) =>
        {
          if (args.Cell.Row == 0) return;

          var dt = vm.ReoGridTable;
          if (dt != null)
          {
            var rowIndex = args.Cell.Row - 1;
            var colIndex = args.Cell.Column;

            while (dt.Columns.Count <= colIndex)
              dt.Columns.Add($"列{dt.Columns.Count}");

            while (dt.Rows.Count <= rowIndex)
              dt.Rows.Add(dt.NewRow());

            dt.Rows[rowIndex][colIndex] = args.Cell.Data ?? DBNull.Value;
          }

          // VMへの変更通知
          vm.RaiseCellDataChanged(new CellChangedEventArgs
          {
            Row = args.Cell.Row,
            Col = args.Cell.Column,
            NewValue = args.Cell.Data
          });
        };
      }
    }

    private static void BindDataTable(ReoGridControl grid, IReoGridSheetViewModel vm)
    {
      var dt = vm.ReoGridTable;

      if (dt == null) return;
      var sheet = grid.CurrentWorksheet;
      sheet.Reset();
      sheet["A1"] = dt;

      // その他カスタマイズ
      vm?.ConfigureSheet?.Invoke(sheet, dt);
    }



    public static void SetItemsSource(DependencyObject obj, IEnumerable value)
    {
      obj.SetValue(ItemsSourceProperty, value);
    }

    public static IReoGridSheetViewModel GetItemsSource(DependencyObject obj)
    {
      return (IReoGridSheetViewModel)obj.GetValue(ItemsSourceProperty);
    }



  }
}

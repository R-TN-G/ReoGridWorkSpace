using ReoGridWorkSpace.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using unvell.ReoGrid;

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
    }

    public static void SetItemsSource(DependencyObject obj, IEnumerable value)
    {
      obj.SetValue(ItemsSourceProperty, value);
    }

    public static IEnumerable GetValue(DependencyObject obj)
    {
      return (IEnumerable)obj.GetValue(ItemsSourceProperty);
    }
  }
}

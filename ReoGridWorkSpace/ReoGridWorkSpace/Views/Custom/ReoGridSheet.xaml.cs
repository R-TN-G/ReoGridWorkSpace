using ReoGridWorkSpace.Interface;
using ReoGridWorkSpace.ViewModels;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using unvell.ReoGrid;

namespace ReoGridWorkSpace.Views.Custom
{
  /// <summary>
  /// ReoGridSheet.xaml の相互作用ロジック
  /// </summary>
  public partial class ReoGridSheet : UserControl
  {
    public ReoGridSheet()
    {
      InitializeComponent();
    }

    //private void Sheet_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    //{
    //  try
    //  {
    //    if(e.NewValue is IReoGridSheetViewModel vm)
    //    {
    //      vm.PropertyChanged += (s, e) =>
    //      {
    //        if(e.PropertyName == nameof(IReoGridSheetViewModel.ReoGridTable))
    //        {
    //          BindDataTable(vm.ReoGridTable);
    //        }
    //      };

    //      // 初回表示用
    //      BindDataTable(vm.ReoGridTable);
    //    }
    //  }
    //  catch
    //  {
    //    throw;
    //  }
    //}

    //private void BindDataTable(DataTable? dt)
    //{
    //  if (dt == null)
    //    return;

    //  var sheet = Sheet.CurrentWorksheet;
    //  sheet.Reset();

    //  // データテーブルをReoGridにインポート
    //  sheet["A1"] = dt;
    //}

  }
}

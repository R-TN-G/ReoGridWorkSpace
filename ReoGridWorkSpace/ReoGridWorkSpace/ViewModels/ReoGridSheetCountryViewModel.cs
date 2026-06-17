using CommunityToolkit.Mvvm.ComponentModel;
using ReoGridWorkSpace.Event;
using ReoGridWorkSpace.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReoGridWorkSpace.ViewModels
{
  public partial class ReoGridSheetCountryViewModel : ObservableObject, IReoGridSheetViewModel
  {
    [ObservableProperty]
    private DataTable? _reoGridTable;

    public event EventHandler<CellChangedEventArgs>? CellDataChanged;

    private ICountrySheetCreator _countrySheetCreator;

    public void RaiseCellDataChanged(CellChangedEventArgs args)
    {
      CellDataChanged?.Invoke(this, args);
    }

    public ReoGridSheetCountryViewModel(ICountrySheetCreator countrySheetCreator)
    {
      // シート内容作成クラス取得
      _countrySheetCreator = countrySheetCreator;
      ReoGridTable = new DataTable();
      // ReoGridの中身作成
      ReoGridTable = _countrySheetCreator.CreateDataSource();
    }
  }
}

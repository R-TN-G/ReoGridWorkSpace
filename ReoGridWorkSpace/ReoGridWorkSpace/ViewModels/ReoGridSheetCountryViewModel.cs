using CommunityToolkit.Mvvm.ComponentModel;
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

    private ICountrySheetCreator _countrySheetCreator;

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

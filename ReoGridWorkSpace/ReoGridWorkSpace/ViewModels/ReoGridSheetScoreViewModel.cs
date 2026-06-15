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
  public partial class ReoGridSheetScoreViewModel : ObservableObject, IReoGridSheetViewModel
  {

    [ObservableProperty]
    private DataTable? _reoGridTable;

    private IScoreSheetCreator _scoreSheetCreator;

    public ReoGridSheetScoreViewModel(IScoreSheetCreator scoreSheetCreator)
    {
      // シート内容作成クラス取得
      _scoreSheetCreator = scoreSheetCreator;
      ReoGridTable = new DataTable();
      // ReoGridの中身作成
      ReoGridTable = _scoreSheetCreator.CreateDataSource();
    }


  }
}

using CommunityToolkit.Mvvm.Input;
using ReoGridWorkSpace.Interface;
using ReoGridWorkSpace.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ReoGridWorkSpace.ViewModels
{
  public partial class AnchorableWindowViewModel : ViewModelBase, IDisposable
  {
    // スコア表示用Grid ViewModel
    public ReoGridSheetScoreViewModel ScoreViewModel { get; set; }
    // 国名表示用Grid ViewModel
    public ReoGridSheetCountryViewModel CountryViewModel { get; set; }

    /// <summary>
    /// 閉じる
    /// </summary>
    public Action CloseAction { get; set; }



    public AnchorableWindowViewModel(IMessageService messageService,
                                     INavigationService navigationService,
                                     ReoGridSheetScoreViewModel scoreViewModel,
                                     ReoGridSheetCountryViewModel countryViewModel)
                                     : base(messageService, navigationService)
    {
      // 各reoGridの中身がある子ViewModelを定義
      ScoreViewModel = scoreViewModel;
      CountryViewModel = countryViewModel;
    }


    [RelayCommand]
    private void Close(object obj)
    {
      try
      {
        CloseAction?.Invoke();
      }
      catch (Exception ex)
      {
        base.Logger.Error(ex);
      }
    }


    /// <summary>
    /// ViewModelBaseの引数ありDisposeを上書きし、終了時の自動Dispose処理を設定する
    /// <see cref="NavigationService"/>
    /// </summary>
    /// <param name="disposing">1回目のDispose = True, 2回目以降 = False</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {

      }
      base.Dispose(disposing);
    }

  }
}

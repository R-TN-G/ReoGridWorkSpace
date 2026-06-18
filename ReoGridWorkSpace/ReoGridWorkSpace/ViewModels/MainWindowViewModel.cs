using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using ReoGridWorkSpace.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Data;
using System.Drawing;
using AvalonDock;
using AvalonDock.Layout.Serialization;
using System.IO;

namespace ReoGridWorkSpace.ViewModels
{
  public partial class MainWindowViewModel : ViewModelBase, IDisposable
  {

    // スコア表示用Grid ViewModel
    public ReoGridSheetScoreViewModel ScoreViewModel { get; set; }
    // 国名表示用Grid ViewModel
    public ReoGridSheetCountryViewModel CountryViewModel { get; set; }
    // AvalonDock状態保存ファイル
    private string xmlPath = System.IO.Path.Combine("DockingLayouts", "MainWindowDockingLayout.xml");

    /// <summary>
    /// 閉じる
    /// </summary>
    public Action CloseAction { get; set; }




    public MainWindowViewModel(IMessageService messageService,
                               INavigationService navigationService,
                               ReoGridSheetScoreViewModel scoreViewModel,
                               ReoGridSheetCountryViewModel countryViewModel)
                               : base(messageService, navigationService)
    {
      // 各reoGridの中身がある子ViewModelを定義
      ScoreViewModel = scoreViewModel;
      CountryViewModel = countryViewModel;

      #region 子ViewModelに移行
      //// シート内容作成クラス取得
      //_scoreSheetCreator = scoreSheetCreator;
      //ScoreTable = new DataTable();
      //// ReoGridの中身作成
      //ScoreTable = _scoreSheetCreator.CreateDataSource();
      #endregion
    }



    [RelayCommand]
    private void Close(object obj)
    {
      try
      {
        CloseAction?.Invoke();
      }
      catch(Exception ex) 
      {
        base.Logger.Error(ex);
      }
    }

    /// <summary>
    /// アンカラブルDockのテスト画面表示
    /// </summary>
    /// <param name="obj"></param>
    [RelayCommand]
    private void ShowAnchorableWindow(object obj)
    {
      try
      {
        RelayShowWindow<Views.AnchorableWindow>();
      }
      catch (Exception ex)
      {
        base.Logger.Error(ex);
      }
    }

    /// <summary>
    /// Bind変更通知のテスト
    /// </summary>
    [RelayCommand]
    private void Test(object obj)
    {
      try
      {
        var bindCheck = ScoreViewModel.ReoGridTable;
      }
      catch (Exception ex)
      {
        base.Logger.Error(ex);
      }
    }

    /// <summary>
    /// AvalonDockの状態保存
    /// </summary>
    [RelayCommand]
    private void SaveDockingXml(object obj)
    {
      try
      {
        if (obj is DockingManager dockingManager)
        {
          XmlLayoutSerializer serializer = new XmlLayoutSerializer(dockingManager);
          using (var stream = new StreamWriter(Utility.PathUtility.CombineWithAppPath(xmlPath)))
          {
            serializer.Serialize(stream);
          }
        }
      }
      catch (Exception ex)
      {
        base.Logger.Error(ex);
      }
    }

    /// <summary>
    /// AvalonDockの状態ロード
    /// </summary>
    [RelayCommand]
    private void LoadDockingXml(object obj)
    {
      try
      {
        if(obj is DockingManager dockingManager)
        {
          XmlLayoutSerializer serializer = new XmlLayoutSerializer(dockingManager);
          using (var stream = new StreamReader(Utility.PathUtility.CombineWithAppPath(xmlPath)))
          {
            serializer.Deserialize(stream);
          }
        }
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

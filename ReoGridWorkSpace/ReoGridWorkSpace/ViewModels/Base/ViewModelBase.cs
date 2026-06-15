using CommunityToolkit.Mvvm.ComponentModel;
using ReoGridWorkSpace.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace ReoGridWorkSpace.ViewModels
{
  public class ViewModelBase : ObservableObject, IDisposable //Models.BindableBase
  {

    /// <summary>
    /// ロガー
    /// </summary>
    protected log4net.ILog Logger = Utility.LogManager.Logger;

    /// <summary>
    /// 画面遷移を担当するインターフェース
    /// </summary>
    protected INavigationService Navigation { get; }

    /// <summary>
    /// メッセージダイアログを担当するインターフェース
    /// </summary>
    protected IMessageService Message { get; }

    public ViewModelBase(IMessageService messageService, INavigationService navigationService)
    {
      Message = messageService;
      Navigation = navigationService;
    }

    /// <summary>
    /// ウィンドウ表示
    /// </summary>
    /// <param name="obj"></param>
    protected virtual void RelayShowWindow<TWindow>() where TWindow : Window
    {
      try
      {
        this.Navigation.ShowWindow<TWindow>();
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
        this.Message.Error("ウィンドウ：" + nameof(TWindow), ex);
      }
    }
    /// <summary>
    /// ダイアログ 
    /// </summary>
    /// <param name="obj">ウィンドウ名を、xamlのCommandParameterで渡す。
    /// [ウィンドウ名]_[パラメータ]とすると、ISettableDataContextインタ
    /// ーフェイスを介してコードビハインドに渡される
    /// </param>
    protected virtual bool? RelayShowDialog<TWindow>() where TWindow : Window
    {
      try
      {
        return Navigation.ShowDialog<TWindow>();
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
        this.Message.Error("ウィンドウ：" + nameof(TWindow), ex);
        return null;
      }
    }


    #region Dispose関係

    private bool _disposed = false;

    /// <summary>
    /// <see cref="IDisposable"/>の実装メソッド。<br/>
    /// 内部で<see cref="Dispose(bool)"/>を呼び出し、マネージドリソースを解放する。<br/>
    /// <see cref="NavigationService.ApplyLifecycle"/>にて、
    /// Windowクローズ時に自動で呼び出されるよう設定されている。
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// リソースの解放。派生クラスでoverrideして解放処理を記述する。
    /// </summary>
    /// <param name="disposing">1回目のDispose = True, 2回目以降 = False</param>
    protected virtual void Dispose(bool disposing)
    {
      if (_disposed) return;
      if (disposing)
      {
        // 派生クラスでoverrideして解放処理を書く
      }
      _disposed = true;
    }

    #endregion


  }
}

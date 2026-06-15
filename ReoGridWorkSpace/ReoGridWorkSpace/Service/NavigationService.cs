using ReoGridWorkSpace.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace ReoGridWorkSpace.Service
{
  /// <summary>
  /// DIコンテナを利用して画面遷移を行うサービスクラス。
  /// <see cref="INavigationService"/>の実装。
  /// </summary>
  public class NavigationService : INavigationService
  {
    /// <summary>
    /// WindowのインスタンスをDIコンテナから解決するためのサービスプロバイダー
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="serviceProvider">DIコンテナのサービスプロバイダー</param>
    public NavigationService(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 指定したWindowをモードレスで表示する。
    /// Windowのインスタンスと紐づくViewModelはDIコンテナから解決される。
    /// </summary>
    /// <typeparam name="TWindow">表示するWindowの型</typeparam>
    public void ShowWindow<TWindow>() where TWindow : Window
    {
      var window = _serviceProvider.GetRequiredService<TWindow>();

      ApplyLifecycle(window, () => window.Show());
    }

    /// <summary>
    /// 指定したWindowをモーダルダイアログとして表示する。
    /// Windowのインスタンスと紐づくViewModelはDIコンテナから解決される。
    /// </summary>
    /// <typeparam name="TWindow">表示するWindowの型</typeparam>
    /// <returns>
    /// ダイアログの結果。
    /// OKまたは確定操作でtrue、キャンセルでfalse、
    /// それ以外はnullを返す。
    /// </returns>
    public bool? ShowDialog<TWindow>() where TWindow : Window
    {
      // WindowをDIコンテナから取得
      var window = _serviceProvider.GetRequiredService<TWindow>();
      bool? result = null;
      ApplyLifecycle(window, () => result = window.ShowDialog());

      return result;
    }


    /// <summary>
    /// Window表示前後の処理・Disposeをまとめて適用する
    /// </summary>
    private void ApplyLifecycle(Window window, Action showAction)
    {

      if (window.DataContext is IWindowLifecycle lifecycle)
      {
        lifecycle.OnBeforeShow();
      }

      if (window.DataContext is IDisposable disposable)
      {
        window.Closed += (s, e) => disposable.Dispose();
      }

      showAction();

      if (window.DataContext is IWindowLifecycle lc)
      {
        lc.OnAfterShow();
      }
    }

  }
}

using ReoGridWorkSpace.Interface;
using ReoGridWorkSpace.ViewModels;
using ReoGridWorkSpace.Views;
using Microsoft.Extensions.DependencyInjection;
using ReoGridWorkSpace.Models;

namespace ReoGridWorkSpace.Service
{
  public static class ServiceRegistration
  {
    /// <summary>
    /// 依存性定義
    /// </summary>
    public static void AddAppServices(this IServiceCollection services)
    {

      // メッセージダイアログ表示機能（Singleton）
      services.AddSingleton<IMessageService, MessageService>();
      // 画面遷移機能（Singleton）
      services.AddTransient<INavigationService, NavigationService>();

      // ViewModel（Transient）
      services.AddTransient<MainWindowViewModel>();
      services.AddTransient<ReoGridSheetScoreViewModel>();
      services.AddTransient<ReoGridSheetCountryViewModel>();

      // Window（ViewModelとの結びつきに必要）
      services.AddTransient<MainWindow>();


      // Model
      // (テストコード使用)
      //services.AddTransient<IScoreSheetCreator, CodeScoreSheetCreator>();
      services.AddTransient<ICountrySheetCreator, CodeCountrySheetCreator>();
      // (Excel使用)
      services.AddTransient<IScoreSheetCreator, ExcelScoreSheetCreator>();

    }
  }
}

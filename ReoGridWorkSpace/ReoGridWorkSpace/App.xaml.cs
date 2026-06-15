using Microsoft.Extensions.DependencyInjection;
using ReoGridWorkSpace.Service;
using ReoGridWorkSpace.Utility;
using ReoGridWorkSpace.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ReoGridWorkSpace
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    public static IServiceProvider Services { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
      var services = new ServiceCollection();

      // 依存性の設定
      services.AddAppServices();

      Services = services.BuildServiceProvider();

      LogManager.Logger.Info("システム起動");

      // 画面起動
      var mainWindow = Services.GetRequiredService<MainWindow>();
      mainWindow.Show();
    }
  }

}

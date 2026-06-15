using System.IO;

namespace ReoGridWorkSpace.Utility
{
  /// <summary>
  /// log4netを使用したロギング機能を一元管理します。
  /// </summary>
  public static class LogManager
  {
    private readonly static log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private static bool _isLogInit = false;
    private static readonly object _lockObj = new object();

    // ログ設定ファイル名
    private const string LogConfigFileName = "ReoGridWorkSpace_Log.config";

    /// <summary>
    /// ログインスタンスを取得します。初回呼び出し時に自動的に初期化されます。
    /// </summary>
    public static log4net.ILog Logger
    {
      get
      {
        EnsureInitialized();
        return _logger;
      }
    }

    /// <summary>
    /// ログシステムが初期化されていることを保証します。
    /// </summary>
    public static bool EnsureInitialized()
    {
      if (_isLogInit) return true;

      lock (_lockObj)
      {
        if (!_isLogInit)
        {
          // コンフィグまでの絶対パスを取得
          var logConfPath = PathUtility.CombineWithAppPath(LogConfigFileName);

          if (File.Exists(logConfPath))
          {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(logConfPath));
          }
          else
          {
            // 設定ファイルがない場合のフォールバック（デバッグ用）
            log4net.Config.BasicConfigurator.Configure();
          }

          _isLogInit = true;
        }
      }

      return true;
    }
  }
}

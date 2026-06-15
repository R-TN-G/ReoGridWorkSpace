using System;
using System.IO;

namespace ReoGridWorkSpace.Utility
{
  public static class PathUtility
  {

    /// <summary>
    /// アプリケーションのexeがあるディレクトリの絶対パスを取得
    /// </summary>
    public static string ApplicationStartupPath => AppContext.BaseDirectory;

    /// <summary>
    /// 実行フォルダを基準とした相対パスから、絶対パスを結合して取得
    /// </summary>
    public static string CombineWithAppPath(string relativePath)
        => Path.Combine(ApplicationStartupPath, relativePath);

    
  }
}
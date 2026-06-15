using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReoGridWorkSpace.Interface
{
  /// <summary>
  /// Window表示前後の処理を定義するインターフェース
  /// 各ViewModelで継承し、必要な時だけ実装
  /// </summary>
  public interface IWindowLifecycle
  {
    /// <summary>
    /// Window表示前に呼ばれる
    /// </summary>
    void OnBeforeShow();

    /// <summary>
    /// Window表示後に呼ばれる
    /// </summary>
    void OnAfterShow(bool? dialogResult = null);
  }
}

using ReoGridWorkSpace.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReoGridWorkSpace.Interface
{
  /// <summary>
  /// 画面遷移を担当するインターフェース
  /// </summary>
  public interface INavigationService
  {

    /* 普通に画面を呼び出す用途 */

    void ShowWindow<TWindow>() where TWindow : Window;
    bool? ShowDialog<TWindow>() where TWindow : Window;

    /* ここまで */


  }
}

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
  /// ViewModelBaseで使用する、メッセージダイアログの表示を担当するインターフェース。
  /// </summary>
  public interface IMessageService
  {
    void Error(string message, Exception ex);
    void Warning(string message);
    void Information(string message);
  }




}

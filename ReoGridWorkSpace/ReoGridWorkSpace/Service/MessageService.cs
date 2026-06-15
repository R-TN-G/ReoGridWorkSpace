using ReoGridWorkSpace.Interface;
using ReoGridWorkSpace.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReoGridWorkSpace.Service
{
  /// <summary>
  /// メッセージダイアログの表示を担当する機能
  /// <see cref="IMessageService"/>の実装。
  /// </summary>
  public class MessageService : IMessageService
  {
    public void Error(string message, Exception ex)
    {
      MessageBox.Show($"{message}\n{ex.Message}", "Error",
          MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public void Warning(string message)
    {
      MessageBox.Show(message, "Warning",
          MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    public void Information(string message)
    {
      MessageBox.Show(message, "Information",
          MessageBoxButton.OK, MessageBoxImage.Information);
    }
  }
}

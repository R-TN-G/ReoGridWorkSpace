using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReoGridWorkSpace.Event
{
  /// <summary>
  /// セル変更イベント引数
  /// </summary>
  public class CellChangedEventArgs : EventArgs
  {
    public int Row { get; init; }
    public int Col { get; init; }
    public object? NewValue { get; init; }
  }
}

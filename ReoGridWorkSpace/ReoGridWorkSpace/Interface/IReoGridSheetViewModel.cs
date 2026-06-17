using ReoGridWorkSpace.Event;
using System.ComponentModel;
using System.Data;
using unvell.ReoGrid;

namespace ReoGridWorkSpace.Interface
{
  public interface IReoGridSheetViewModel
  {
    /// <summary>
    /// ReoGrid表示用DataTable
    /// </summary>
    DataTable? ReoGridTable { get; }

    /// <summary>
    /// データソース変更時、画面反映用
    /// </summary>
    event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// セル編集時にSheetからVMへ通知
    /// </summary>
    event EventHandler<CellChangedEventArgs>? CellDataChanged;

    /// <summary>
    /// ReoGridの詳細カスタマイズ（数式など）
    /// </summary>
    Action<Worksheet, DataTable>? ConfigureSheet => null;

    /// <summary>
    /// CellDataChangedの補助
    /// </summary>
    void RaiseCellDataChanged(CellChangedEventArgs args);
  }
}

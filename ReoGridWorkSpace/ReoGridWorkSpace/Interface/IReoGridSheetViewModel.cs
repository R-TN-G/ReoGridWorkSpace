using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
  }
}

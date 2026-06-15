using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReoGridWorkSpace.Interface
{
  /// <summary>
  /// ReoGridのデータソース作成用インターフェース
  /// </summary>
  public interface ICountrySheetCreator
  {
    /// <summary>
    /// 画面表示する国名シート作成
    /// </summary>
    DataTable CreateDataSource();

  }
}

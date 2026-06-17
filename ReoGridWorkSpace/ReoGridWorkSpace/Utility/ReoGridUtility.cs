using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unvell.ReoGrid;

namespace ReoGridWorkSpace.Utility
{
  public static class ReoGridUtility
  {
    public static string NumberToAlphabet(int colNo)
    {
      // 行番号に「0（1行目）」を指定してアドレス "C1" を取得
      string address = new CellPosition(0, colNo).ToAddress();

      // 正規表現で末尾の数字「1」を消去して "C" にする
      string colLetter = System.Text.RegularExpressions.Regex.Replace(address, @"\d", "");

      return colLetter;
    }

    /// <summary>
    /// 
    /// </summary>
    public static Dictionary<int, string> NumberToDictionary(int colNo)
    {
      // 行番号に「0（1行目）」を指定してアドレス "C1" を取得
      string address = new CellPosition(0, colNo).ToAddress();

      // 正規表現で末尾の数字「1」を消去して "C" にする
      string colLetter = System.Text.RegularExpressions.Regex.Replace(address, @"\d", "");

      return new Dictionary<int, string>()
      {
        { colNo, colLetter }
      };
    }


  }
}

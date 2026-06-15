using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReoGridWorkSpace.Entity
{
  public class CountryEntity
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Value1 { get; set; }
    public int Value2 { get; set; }
    public int Value3 { get; set; }
    public int Value4 { get; set; }
    public int Value5 { get; set; }
    public int Value6 { get; set; }
    public int Value7 { get; set; }
    public int Value8 { get; set; }
    public int Value9 { get; set; }

    // コンストラクタ
    public CountryEntity(int id, string name, int value1, int value2, int value3, int value4, int value5, int value6, int value7, int value8, int value9)
    {
      Id = id;
      Name = name;
      Value1 = value1;
      Value2 = value2;
      Value3 = value3;
      Value4 = value4;
      Value5 = value5;
      Value6 = value6;
      Value7 = value7;
      Value8 = value8;
      Value9 = value9;
    }
  }
}

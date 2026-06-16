using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniExcelLibs.Attributes;

namespace ReoGridWorkSpace.Entity
{
  public class ScoreEntity
  {
    [ExcelColumnIndex(0)]
    public int Id { get; set; }
    [ExcelColumnIndex(1)]
    public string Name { get; set; } = string.Empty;
    [ExcelColumnIndex(2)]
    public int Japanese { get; set; }
    [ExcelColumnIndex(3)]
    public int Math { get; set; }
    [ExcelColumnIndex(4)]
    public int English { get; set; }
    [ExcelColumnIndex(5)]
    public int Chemistry { get; set; }
    [ExcelColumnIndex(6)]
    public int Physics { get; set; }
    [ExcelColumnIndex(7)]
    public int JapanHistory { get; set; }
    [ExcelColumnIndex(8)]
    public int WorldHistory { get; set; }
    [ExcelColumnIndex(9)]
    public int Health { get; set; }
    [ExcelColumnIndex(10)]
    public int Ethics { get; set; }

    public ScoreEntity()
    {

    }

    // コンストラクタ
    public ScoreEntity(int id, string name, int jpn, int math, int eng, int chem, int phys, int jHist, int wHist, int health, int ethics)
    {
      Id = id;
      Name = name;
      Japanese = jpn;
      Math = math;
      English = eng;
      Chemistry = chem;
      Physics = phys;
      JapanHistory = jHist;
      WorldHistory = wHist;
      Health = health;
      Ethics = ethics;
    }

  }
}

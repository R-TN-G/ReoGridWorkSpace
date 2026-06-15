using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReoGridWorkSpace.Entity
{
  public class ScoreEntity
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Japanese { get; set; }
    public int Math { get; set; }
    public int English { get; set; }
    public int Chemistry { get; set; }
    public int Physics { get; set; }
    public int JapanHistory { get; set; }
    public int WorldHistory { get; set; }
    public int Health { get; set; }
    public int Ethics { get; set; }

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

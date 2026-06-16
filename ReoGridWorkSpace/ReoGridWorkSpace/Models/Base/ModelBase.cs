using CommunityToolkit.Mvvm.ComponentModel;

namespace ReoGridWorkSpace.Models
{
  public class ModelBase : ObservableObject
  {
    protected log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

  }
}

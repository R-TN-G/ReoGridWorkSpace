using ReoGridWorkSpace.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ReoGridWorkSpace.Views
{
  /// <summary>
  /// AnchorableWindow.xaml の相互作用ロジック
  /// </summary>
  public partial class AnchorableWindow : Window
  {
    public AnchorableWindow()
    {
      InitializeComponent();
    }

    public AnchorableWindow(AnchorableWindowViewModel vm)
    {
      InitializeComponent();
      DataContext = vm;

      vm.CloseAction = () =>
      {
        this.Close();
      };
    }

  }
}

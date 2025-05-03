using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BinaryGrid.Model;

namespace BinaryGrid;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        //获取系统是以Left-handed（true）还是Right-handed（false）
        var ifLeft = SystemParameters.MenuDropAlignment;
        if (ifLeft)
        {
            // change to false
            var t = typeof(SystemParameters);
            var field = t.GetField("_menuDropAlignment", BindingFlags.NonPublic | BindingFlags.Static);
            field.SetValue(null, false);
            ifLeft = SystemParameters.MenuDropAlignment;
        }
        DataContext = new MainViewModel();
    }

    //private void Button_Click(object sender, RoutedEventArgs e)
    //{

    //    if (eight.Background == Brushes.Red)
    //    {
    //        eight.Background = Brushes.Green;
    //     }
    //    else
    //    {
    //        eight.Background = Brushes.Red;
    //    }
    //}
}

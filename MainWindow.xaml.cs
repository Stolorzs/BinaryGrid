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

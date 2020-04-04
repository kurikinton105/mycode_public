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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NavigationTest
{
    /// <summary>
    /// Page1.xaml の相互作用ロジック
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            //InitializeComponent();

        }
        private void btnNavigateWithUri_Click(object sender, RoutedEventArgs e)
        {
            //var nextPage = new Page2();
            //NavigationService.Navigate(nextPage);
        }

        private void BtnNavigateWithObject_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Page2.xaml", UriKind.Relative));
            
        }
        private void LoginButton(object sender, RoutedEventArgs e)
        {
            //.Background = Brushes.LightBlue
        }
        
    }
}

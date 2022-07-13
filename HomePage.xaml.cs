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

namespace ApplicationRestaurant
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        private bool _showLunch;
        public HomePage()
        {
            InitializeComponent();
        }

        private void click_sniadanie(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void LunchAction(object sender, RoutedEventArgs e)
        {
            this.toggleStackPanel(LunchPanel);
        }

        private void DrinkAction(object sender, RoutedEventArgs e)
        {
            this.toggleStackPanel(DrinkPanel);
        }

        private void toggleStackPanel(StackPanel element)
        {
            LunchPanel.Visibility = Visibility.Hidden;
            DrinkPanel.Visibility = Visibility.Hidden;

            element.Visibility = Visibility.Visible;
            
        }
        
    }
}

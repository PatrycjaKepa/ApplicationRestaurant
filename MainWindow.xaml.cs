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
using ApplicationRestaurant.Repository;
using ApplicationRestaurant.Models;

namespace ApplicationRestaurant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserRepository userRepository;
        public MainWindow()
        {
            this.userRepository = new UserRepository();
            InitializeComponent();
        }

        private void LoginAction(object sender, RoutedEventArgs e)
        {
            TextBox login = (TextBox)this.FindName("login");
            TextBox password = (TextBox)this.FindName("Password");
            List<Users> users = userRepository.getAll();

            Application.Current.MainWindow = new HomePage();
            Application.Current.MainWindow.Show();
            this.Close();
        }
    }
}

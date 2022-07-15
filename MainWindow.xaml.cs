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
using ApplicationRestaurant.ValueObject;

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

        private void LoginAction(object sender, RoutedEventArgs e) // metoda do logowania użytkownika
        {
            TextBox login = (TextBox)this.FindName("login");
            TextBox password = (TextBox)this.FindName("password");
            var user = userRepository.getByName(login.Text);
            if (user == null)   // instrukcja sprawdzająca poprawność logowania
            {
                MessageBox.Show("Nie znaleziono użytkownika");
                return;
            }
            if (user.Password.Trim() != password.Text.Trim())
            {
                MessageBox.Show("Hasło jest nie poprawne");
                return;
            }

            Application.Current.MainWindow = new Start(new UserVO(user.Id, user.UserName, user.Role, user.Password)); // po sprawdzeniu użytkownika przechodzimy do okna tworzenia zamówień 
            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void GoToRegisterAction(object sender, RoutedEventArgs e) // metoda dzięki której po przycisnięciu opcji zarejestruj przechodzimy do okna rejestracji
        {
            Application.Current.MainWindow = new Register();
            Application.Current.MainWindow.Show();
            this.Close();
        }
    }
}

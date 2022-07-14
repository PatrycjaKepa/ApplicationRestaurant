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
using ApplicationRestaurant.Repository;
using ApplicationRestaurant.Models;

namespace ApplicationRestaurant
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {

        private UserRepository userRepository;

        public Register()
        {
            this.userRepository = new UserRepository();
            InitializeComponent();
        }

        private void RegisterAction(object sender, RoutedEventArgs e)
        {
            TextBox login = (TextBox)this.FindName("login");
            TextBox password = (TextBox)this.FindName("password");

            if(this.checkIfUserExist(login.Text))
            {
                MessageBox.Show("Użytkownik już istnieje!");
                return;
            }

            bool isCreated = userRepository.createUser(login.Text, password.Text);
            if (!isCreated)
            {
                MessageBox.Show("Nie można zarejestrować użytkownika");
                return;
            }
            MessageBox.Show("Użytkownik został poprawnie założony");

            this.returnToMainWindow();
        }


        private void ReturnToLoginAction(object sender, RoutedEventArgs e)
        {
            this.returnToMainWindow();
        }

        private Boolean checkIfUserExist(string name)
        {
            return Convert.ToBoolean(userRepository.getByName(name));
        }

        private void returnToMainWindow()
        {
            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();
            this.Close();
        }
    }
}

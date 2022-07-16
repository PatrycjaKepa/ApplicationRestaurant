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
using ApplicationRestaurant.ValueObject;
using ApplicationRestaurant.Repository;
using ApplicationRestaurant.Models;

namespace ApplicationRestaurant
{
    /// <summary>
    /// klasa która pozwala utworzyć nowe zamówienie 
    /// </summary>
    public partial class Start : Window
    {
        private UserVO user;
        private OrdersRepository ordersRepository;
        public Start(UserVO user)
        {
            this.ordersRepository = new OrdersRepository();
            this.user = user;
            InitializeComponent();
            this.bindItems();
        }

        private void bindItems()
        {
            Grid grid = (Grid)this.FindName("ordersContainer");
            grid.Children.Clear();
            List<Orders> orders = this.ordersRepository.getAll();
            int row = 0;
            grid.RowDefinitions.Clear();

            
            foreach (Orders order in orders)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(100);
                grid.RowDefinitions.Add(rowDefinition);
                Button button = new Button();

                button.Content = order.Id; // przycisk łączy się z baza sprawdzając id zamówienia
                button.Name = "order_" + order.Id; // dodajemy numer zamówienia
                button.SetValue(Grid.RowProperty, row);
                button.SetValue(Grid.ColumnProperty, 0);
                button.Click += EditOrderAction; // edycja zamówienia poprzek klikniecie przycisku
                button.Background = Brushes.White;

                Button DeleteButton = new Button(); // przycisk do usuwania pozycji 
                DeleteButton.SetValue(Grid.RowProperty, row);
                DeleteButton.SetValue(Grid.ColumnProperty, 2);
                DeleteButton.Content = "-"; // klikniecie - spowoduje usunięcie pozycji 
                DeleteButton.Name = "delete_order_" + order.Id;
                DeleteButton.Click += DeleteOrderAction;
                DeleteButton.Background = Brushes.White;

                grid.Children.Add(button);
                grid.Children.Add(DeleteButton);

                row++;
            }
        }

        private void AddOrderAction(object sender, RoutedEventArgs e) // metoda dzięki której tworzymy zamówienie
        {
            bool isCreated = ordersRepository.createOrder("TODO");
            if (!isCreated)
            {
                MessageBox.Show("Nie można utworzyć zamówienia");
                return;
            }
            MessageBox.Show("Zamówienie utworzono");
            this.bindItems();
        }

        private void DeleteOrderAction(object sender, RoutedEventArgs e) // metoda do usuwania zamówienia
        {
            string name = ((Button)sender).Name;
            name = name.Remove(0, 13);
            bool isDeleted = ordersRepository.removeById(Convert.ToInt32(name));
            if (isDeleted == false)
            {
                MessageBox.Show("coś poszło nie tak");
                return;
            }
            MessageBox.Show("Zamówienie usunięte");
            this.bindItems();
        }

        private void EditOrderAction(object sender, RoutedEventArgs e) // metoda do edycji zamówienia
        {
            string name = ((Button)sender).Name;
            name = name.Remove(0, 6);
            Application.Current.MainWindow = new HomePage(new OrderVO(Convert.ToInt32(name)), this.user);
            Application.Current.MainWindow.Show();
            this.Close();
        }
        private void LogoutAction(object sender, RoutedEventArgs e) // metoda do wylogowywania się 
        {
            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();
            this.Close();
        }
    }
}

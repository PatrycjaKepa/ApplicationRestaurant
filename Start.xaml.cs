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
    /// Interaction logic for Window1.xaml
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

            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(100);
            foreach (Orders order in orders)
            {
                grid.RowDefinitions.Add(rowDefinition);
                Button button = new Button();

                button.Content = order.Id;
                button.Name = "order_" + order.Id;
                button.SetValue(Grid.RowProperty, row);
                button.SetValue(Grid.ColumnProperty, 0);
                button.Click += EditOrderAction;

                Button AddButton = new Button();
                AddButton.SetValue(Grid.RowProperty, row);
                AddButton.SetValue(Grid.ColumnProperty, 1);
                AddButton.Click += AddOrderAction;
                AddButton.Content = "+";

                Button DeleteButton = new Button();
                DeleteButton.SetValue(Grid.RowProperty, row);
                DeleteButton.SetValue(Grid.ColumnProperty, 2);
                DeleteButton.Content = "-";
                DeleteButton.Name = "delete_order_" + order.Id;
                DeleteButton.Click += DeleteOrderAction;

                grid.Children.Add(button);
                grid.Children.Add(AddButton);
                grid.Children.Add(DeleteButton);

                row++;
            }
        }

        private void AddOrderAction(object sender, RoutedEventArgs e)
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

        private void DeleteOrderAction(object sender, RoutedEventArgs e)
        {
            string name = ((Button)sender).Name;
            name = name.Remove(0, 13);
            bool isDeleted = ordersRepository.removeById(Convert.ToInt32(name));
            MessageBox.Show("Zamówienie usunięte");
            this.bindItems();
        }

        private void EditOrderAction(object sender, RoutedEventArgs e)
        {
            string name = ((Button)sender).Name;
            name = name.Remove(0, 6);
            Application.Current.MainWindow = new HomePage(new OrderVO(Convert.ToInt32(name)), this.user);
            Application.Current.MainWindow.Show();
            this.Close();
        }
    }
}

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

namespace ApplicationRestaurant
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        private ProductsRepository productsRepository;
        private OrderVO orderVO;
        private UserVO userVO;
        public HomePage(OrderVO orderVO, UserVO userVO)
        {
            this.orderVO = orderVO;
            this.userVO = userVO;
            this.productsRepository = new ProductsRepository();
            InitializeComponent();
        }

        private void LogoutAction(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void BreakfastAction(object sender, RoutedEventArgs e)
        {
            this.toggleGrid(BreakfastPanel);
            var elements = productsRepository.getProductWithCategoriesByCategoryName("Śniadanie");
        }

        private void LunchAction(object sender, RoutedEventArgs e)
        {
            this.toggleGrid(LunchPanel);
            var elements = productsRepository.getProductWithCategoriesByCategoryName("Lunch");
        }

        private void DrinkAction(object sender, RoutedEventArgs e)
        {
            this.toggleGrid(DrinkPanel);
            var elements = productsRepository.getProductWithCategoriesByCategoryName("Napoje");
        }

        private void toggleGrid(Grid element)
        {
            BreakfastPanel.Visibility = Visibility.Hidden;
            LunchPanel.Visibility = Visibility.Hidden;
            DrinkPanel.Visibility = Visibility.Hidden;

            element.Visibility = Visibility.Visible;
            
        }
        
        private void showProductsInGrid()
        {
            //todo
        }
    }
}

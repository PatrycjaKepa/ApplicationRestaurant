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
using System.Windows.Media;

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

        private void LogoutAction(object sender, RoutedEventArgs e) // metoda do wylogowywania się 
        {
            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void BreakfastAction(object sender, RoutedEventArgs e) // metoda pokazująca opcje z zakładki śniadania
        {
            var elements = productsRepository.getProductWithCategoriesByCategoryName("Śniadanie");
            this.showProductsInGrid(elements, FoodPanel);
        }

        private void LunchAction(object sender, RoutedEventArgs e) // metoda pokazująca opcje z zakładki lunch
        {
            var elements = productsRepository.getProductWithCategoriesByCategoryName("Lunch");
            this.showProductsInGrid(elements, FoodPanel);
        }

        private void DrinkAction(object sender, RoutedEventArgs e) // metoda pokazująca opcje z zakładki napoje
        {
            var elements = productsRepository.getProductWithCategoriesByCategoryName("Napoje");
            this.showProductsInGrid(elements, FoodPanel);
        }

   
        
        private void showProductsInGrid(List<ProductsWithCategoriesVO> products, Grid grid)
        {
            int row = 0;
            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            foreach (ProductsWithCategoriesVO product in products)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(50);
                grid.RowDefinitions.Add(rowDefinition);

                Button button = new Button();

                button.Content = product.Name;
                button.SetValue(Grid.RowProperty, row);
                button.SetValue(Grid.ColumnProperty, 0);
                button.Background = Brushes.White;

                Button AddButton = new Button();
                AddButton.SetValue(Grid.RowProperty, row);
                AddButton.SetValue(Grid.ColumnProperty, 1);
                AddButton.Background = Brushes.White;
                AddButton.Content = "Dodaj";

                grid.Children.Add(button);
                grid.Children.Add(AddButton);


                row++;
            }
        }
    }
}

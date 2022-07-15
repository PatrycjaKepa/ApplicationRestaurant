﻿using System;
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
        private OrderLinesRepository orderLinesRepository;
        private OrderVO orderVO;
        private UserVO userVO;
        public HomePage(OrderVO orderVO, UserVO userVO)
        {
            this.orderVO = orderVO;
            this.userVO = userVO;
            this.productsRepository = new ProductsRepository();
            this.orderLinesRepository = new OrderLinesRepository();
            InitializeComponent();
        }

        private void BackAction(object sender, RoutedEventArgs e) // metoda do wylogowywania się 
        {
            Application.Current.MainWindow = new Start(this.userVO);
            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void BreakfastAction(object sender, RoutedEventArgs e) // metoda pokazująca opcje z zakładki śniadania
        {
            SummaryPanelWrapper.Visibility = Visibility.Hidden;
            FoodPanel.Visibility = Visibility.Visible;
            var elements = productsRepository.getProductWithCategoriesByCategoryName("Śniadanie");
            this.showProductsInGrid(elements, FoodPanel);
        }

        private void LunchAction(object sender, RoutedEventArgs e) // metoda pokazująca opcje z zakładki lunch
        {
            SummaryPanelWrapper.Visibility = Visibility.Hidden;
            FoodPanel.Visibility = Visibility.Visible;
            var elements = productsRepository.getProductWithCategoriesByCategoryName("Lunch");
            this.showProductsInGrid(elements, FoodPanel);
        }

        private void DrinkAction(object sender, RoutedEventArgs e) // metoda pokazująca opcje z zakładki napoje
        {
            SummaryPanelWrapper.Visibility = Visibility.Hidden;
            FoodPanel.Visibility = Visibility.Visible;
            var elements = productsRepository.getProductWithCategoriesByCategoryName("Napoje");
            this.showProductsInGrid(elements, FoodPanel);
        }

        private void SummaryAction(object sender, RoutedEventArgs e)
        {
            SummaryPanelWrapper.Visibility = Visibility.Visible;
            FoodPanel.Visibility = Visibility.Hidden;
            this.showSummary();
        }

        private void showSummary()
        {
            var orderLines = orderLinesRepository.getByOrderId(orderVO.Id);
            int row = 0;
            decimal total = 0;
            SummaryPanel.Children.Clear();
            SummaryPanel.RowDefinitions.Clear();
            foreach (var line in orderLines)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(50);
                SummaryPanel.RowDefinitions.Add(rowDefinition);

                Button button = new Button();

                button.Content = line.Name;
                button.SetValue(Grid.RowProperty, row);
                button.SetValue(Grid.ColumnProperty, 0);
                button.Background = Brushes.White;

                Button buttonPrice = new Button();

                buttonPrice.Content = line.Value;
                buttonPrice.SetValue(Grid.RowProperty, row);
                buttonPrice.SetValue(Grid.ColumnProperty, 1);
                buttonPrice.Background = Brushes.White;

                Button buttonQuantity = new Button();

                buttonQuantity.Content = line.Quantity;
                buttonQuantity.SetValue(Grid.RowProperty, row);
                buttonQuantity.SetValue(Grid.ColumnProperty, 2);
                buttonQuantity.Background = Brushes.White;

                Button buttonRemove = new Button();

                buttonRemove.Content = "usuń";
                buttonRemove.SetValue(Grid.RowProperty, row);
                buttonRemove.SetValue(Grid.ColumnProperty, 3);
                buttonRemove.Background = Brushes.White;
                buttonRemove.Click += (object sender, RoutedEventArgs e) => { DeleteOrderLineAction(sender, e, line.Id); };

                SummaryPanel.Children.Add(button);
                SummaryPanel.Children.Add(buttonPrice);
                SummaryPanel.Children.Add(buttonQuantity);
                SummaryPanel.Children.Add(buttonRemove);

                total += line.Quantity * line.Value;
                row++;
            }
            RowDefinition rowDefinitionSummary = new RowDefinition();
            rowDefinitionSummary.Height = new GridLength(50);
            SummaryPanel.RowDefinitions.Add(rowDefinitionSummary);

            Button buttonSummary = new Button();

            buttonSummary.Content = "total:";
            buttonSummary.SetValue(Grid.RowProperty, row);
            buttonSummary.SetValue(Grid.ColumnSpanProperty, 2);
            buttonSummary.SetValue(Grid.ColumnProperty, 0);
            buttonSummary.Background = Brushes.White;

            Button buttonQuantitySummary = new Button();

            buttonQuantitySummary.Content = total;
            buttonQuantitySummary.SetValue(Grid.RowProperty, row);
            buttonQuantitySummary.SetValue(Grid.ColumnSpanProperty, 2);
            buttonQuantitySummary.SetValue(Grid.ColumnProperty, 2);
            buttonQuantitySummary.Background = Brushes.White;

            SummaryPanel.Children.Add(buttonSummary);
            SummaryPanel.Children.Add(buttonQuantitySummary);
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
                AddButton.Click += (object sender, RoutedEventArgs e) => { AddOrderLineAction(sender, e, product.Id); };

                grid.Children.Add(button);
                grid.Children.Add(AddButton);


                row++;
            }

        }
        private void AddOrderLineAction(object sender, RoutedEventArgs e, int productId)
        {
            bool isCreated = orderLinesRepository.addOrUpdate(orderVO.Id, productId);
            if (!isCreated)
            {
                MessageBox.Show("Nie można dodać produktu");
                return;
            }
            MessageBox.Show("Zamówienie zaktualizowano");
        }

        private void DeleteOrderLineAction(object sender, RoutedEventArgs e, int orderLineId)
        {
            bool isDeleted = orderLinesRepository.removeById(orderLineId);
            if (isDeleted == false)
            {
                MessageBox.Show("coś poszło nie tak");
                return;
            }
            MessageBox.Show("Linia usunięta");
            this.showSummary();
        }
    }
}

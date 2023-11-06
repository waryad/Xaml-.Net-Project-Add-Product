using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Final_DeepinderKaurWarya
{
    public partial class AddNewProductWindow : Window
    {
        public AddNewProductWindow()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void LoadCategories()
        {
            using (var context = new NorthwindEntities())
            {
                var categories = context.Categories.ToList();
                CmbNewProductCategory.ItemsSource = categories;
            }
        }


        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            // Check if product name or category is not entered
            if (string.IsNullOrWhiteSpace(TxtNewProductName.Text) || CmbNewProductCategory.SelectedValue == null)
            {
                MessageBox.Show("Please enter the required information (Product Name and Category) before adding a product.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            // Check if price is empty or cannot be parsed as a decimal
            if (string.IsNullOrWhiteSpace(TxtPrice.Text) || !decimal.TryParse(TxtPrice.Text, out decimal parsedPrice))
            {
                MessageBox.Show("Please enter a valid price before adding a product.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var context = new NorthwindEntities())
            {
                Product newProduct = new Product
                {
                    ProductName = TxtNewProductName.Text,
                    UnitPrice = parsedPrice,  // Use the parsed price here
                    CategoryID = (int)CmbNewProductCategory.SelectedValue
                };

                context.Products.Add(newProduct);
                context.SaveChanges();
            }

            MessageBox.Show("Product added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
            this.Close();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
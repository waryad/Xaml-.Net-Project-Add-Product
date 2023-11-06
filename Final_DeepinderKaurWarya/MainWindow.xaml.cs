using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Final_DeepinderKaurWarya
{
    public partial class MainWindow : Window
    {
        public List<Category> Categories { get; set; }
        public List<Product> DisplayedProducts { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategories();
            
        }

        private void LoadCategories()
        {

            using (var context = new NorthwindEntities())
            {
                Categories = context.Categories.ToList();
                CmbCategories.ItemsSource = Categories; // Add this line
                CmbCategories.DisplayMemberPath = "CategoryName";
                CmbCategories.SelectedValuePath = "CategoryID";
            }
        }
        private void LoadProducts()
        {
            using (var context = new NorthwindEntities())
            {
                DisplayedProducts = context.Products.ToList();
                dgProducts.ItemsSource = DisplayedProducts;
            }
        }

        private void LoadProductsByCategory(int categoryId)
        {
            using (var context = new NorthwindEntities())
            {
                DisplayedProducts = context.Products
                    .Where(p => p.CategoryID == categoryId)
                    .ToList();
                dgProducts.ItemsSource = DisplayedProducts;
            }
        }

        private void BtnGetAllProducts_Click(object sender, RoutedEventArgs e)
        {
            LoadProducts();
        }

        private void BtnClearData_Click(object sender, RoutedEventArgs e)
        {
            CmbCategories.SelectedIndex = -1;
            TxtProductName.Clear();
            dgProducts.ItemsSource = null;
            DisplayedProducts?.Clear();  // Add this line
        }

        private void BtnSearchProduct_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new NorthwindEntities())
            {
                string productName = TxtProductName.Text.Trim().ToLower();
                var products = context.Products
                    .Where(p => p.ProductName.ToLower().Contains(productName))
                    .ToList();
                dgProducts.ItemsSource = products;
            }
        }

        private void CmbCategories_SelectionChanged(object sender, SelectionChangedEventArgs e) // Add this method
        {
            if (CmbCategories.SelectedItem is Category selectedCategory)
            {
                LoadProductsByCategory(selectedCategory.CategoryID);
            }
        }

        private void BtnAddNewProduct_Click(object sender, RoutedEventArgs e)
        {
            AddNewProductWindow addNewProductWindow = new AddNewProductWindow();
            addNewProductWindow.ShowDialog();
            // Reload products after the Add New Product window is closed
            LoadProducts();
        }
    }
}